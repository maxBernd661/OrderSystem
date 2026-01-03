using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.Win.Controls;
using OrderSystem.Win.Forms;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Services
{
    public sealed class ViewManager : IDisposable
    {
        public ViewManager(IServiceProvider sp)
        {
            this.sp = sp;
            scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
            factory = sp.GetRequiredService<ViewFactory>();
        }

        private readonly IServiceProvider sp;
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ViewFactory factory;
        private readonly List<IManagedView> views = [];

        public void AddListView<TEntity>() where TEntity : PersistentEntityBase
        {
            IServiceScope viewScope = scopeFactory.CreateScope();
            ListView<TEntity> listview = viewScope.ServiceProvider.GetRequiredService<ListView<TEntity>>();

            ViewHolder holder = new($"All {typeof(TEntity).Name}s", listview);
            ManagedView<TEntity> managedView = new()
            {
                EntityType = typeof(TEntity),
                Holder = holder,
                Kind = ViewKind.ListView,
                Scope = viewScope
            };

            AddView(managedView);
        }

        public void AddDetailView<TEntity>(Guid? id = null) where TEntity : PersistentEntityBase
        {
            IServiceScope viewScope = scopeFactory.CreateScope();
            OrderContext context = viewScope.ServiceProvider.GetRequiredService<OrderContext>();

            DetailView<TEntity> view = factory.CreateDetailView<TEntity>();

            TEntity? existingItem = null;
            string viewName = $"New {typeof(TEntity).Name}";
            if (id != null)
            {
                existingItem = context.Set<TEntity>()
                                      .AsNoTracking()
                                      .Single(x => x.Id == id);

                PropertyInfo? identProp = factory.GetIdentifier(typeof(TEntity));
                if (identProp != null)
                {
                    object? identvalue = identProp.GetValue(existingItem);
                    viewName = identvalue is string s ? s : string.Empty;
                }

                view.Template.LoadData(existingItem);
            }

            ViewHolder holder = new(viewName, view);
            holder.ViewChanged += ViewChanged;

            ManagedView<TEntity> managedView = new()
            {
                Holder = holder,
                EntityType = typeof(TEntity),
                Entity = existingItem,
                Kind = ViewKind.DetailView,
                Scope = viewScope
            };

            AddView(managedView);
        }

        private void AddView<TEntity>(ManagedView<TEntity> managedView) where TEntity : PersistentEntityBase
        {
            MainForm form = sp.GetRequiredService<MainForm>();

            //dont open the same unfiltered listview twice
            if (managedView.Kind == ViewKind.ListView)
            {
                IManagedView? openListView = views.FirstOrDefault(x => x.EntityType == typeof(TEntity));
                if (openListView != null)
                {
                    form.ShowView(openListView.Holder);
                    return;
                }
            }

            if (managedView.Entity != null)
            {
                IManagedView? openDetailView = views.FirstOrDefault(x => x.Kind == ViewKind.DetailView && x.ManagedEntity?.Id == managedView.Entity.Id);
                if (openDetailView != null)
                {
                    form.ShowView(openDetailView.Holder);
                    return;
                }
            }

            views.Add(managedView);
            form.ShowView(managedView.Holder);
        }

        public void Dispose()
        {
            foreach (IManagedView view in views)
            {
                view.Scope.Dispose();
            }
            views.Clear();
        }

        public bool DisposeView(ViewHolder holder)
        {
            IManagedView? view = views.FirstOrDefault(x => x.Holder == holder);
            if (view != null)
            {
                view.Holder.ViewChanged -= ViewChanged;
                view.Scope.Dispose();
                views.Remove(view);
                return true;
            }

            return false;
        }

        private void ViewChanged(object? sender, EventArgs e)
        {
            MainForm form = sp.GetRequiredService<MainForm>();
            form.ToggleButtons();
        }

        public async Task SaveAsync(ViewHolder holder)
        {
            IManagedView? managedView = views.FirstOrDefault(x => x.Holder == holder);
            if (managedView is null)
            {
                return;
            }

            Type serviceType = typeof(SavingService<>).MakeGenericType(managedView.EntityType);
            ISavingService service = (ISavingService)managedView.Scope.ServiceProvider.GetRequiredService(serviceType);
            PersistentEntityBase savedItem = await service.SaveAsync(managedView);

            if (holder.View is IDetailView dv)
            {
                managedView.ManagedEntity = savedItem;
                holder.Text = dv.ReadData().GetIdentifier();
            }

            await ReloadView(holder);
        }

        public async Task ReloadView(ViewHolder holder)
        {
            if (holder.View is IListView lv)
            {
                await lv.LoadSourceData();
            }
            else if (holder.View is IDetailView dv)
            {
                IManagedView? managedView = views.FirstOrDefault(x => x.Holder == holder);
                if (managedView is { ManagedEntity: { } managedEntity })
                {
                    dv.Template.LoadData(managedEntity);
                }
                else
                {
                    object? entity = Activator.CreateInstance(holder.View.EntityType);
                    if (entity is PersistentEntityBase baseEntity)
                    {
                        dv.Template.LoadData(baseEntity);
                    }
                }
            }
        }
    }

    public interface IManagedView
    {
        IServiceScope Scope { get; set; }

        ViewHolder Holder { get; set; }

        ViewKind Kind { get; set; }

        Type EntityType { get; set; }

        PersistentEntityBase? ManagedEntity { get; set; }
    }

    public class ManagedView<TEntity> : IManagedView where TEntity : PersistentEntityBase
    {
        public ViewHolder Holder { get; set; }

        public ViewKind Kind { get; set; }

        public Type EntityType { get; set; }

        public PersistentEntityBase? ManagedEntity
        {
            get { return Entity; }
            set { Entity = (TEntity)value; }
        }

        public TEntity? Entity { get; set; }

        public IServiceScope Scope { get; set; }
    }
}