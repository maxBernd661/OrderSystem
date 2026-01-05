using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.Win.Controls;
using OrderSystem.Win.Forms;
using OrderSystem.Win.View;
using OrderSystem.Win.ViewControllers;

namespace OrderSystem.Win.Services
{
    public sealed class ViewManager(IServiceProvider sp) : IDisposable
    {
        private readonly IServiceScopeFactory scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
        private readonly ViewFactory factory = sp.GetRequiredService<ViewFactory>();
        private readonly List<IManagedView> views = [];

        private ManagedView<TEntity> BuildManagedView<TEntity>(ViewKind kind) where TEntity : PersistentEntityBase
        {
            IServiceScope viewScope = scopeFactory.CreateScope();
            ViewBase? viewBase;
            if (kind is ViewKind.ListView)
            {
                viewBase = viewScope.ServiceProvider.GetRequiredService<ListView<TEntity>>();
            }
            else
            {
                viewBase = factory.CreateDetailView<TEntity>();
            }

            ViewHolder holder = new(string.Empty, viewBase);
            holder.Disposed += (_, _) => DisposeView(holder);
            List<IControllerBase> controllers = factory.MakeControllers<TEntity>(viewScope.ServiceProvider, viewBase);
            return new ManagedView<TEntity>()
            {
                Controllers = controllers,
                Kind = kind,
                Scope = viewScope,
                EntityType = typeof(TEntity),
                Holder = holder
            };
        }

        public ViewHolder AddListView<TEntity>() where TEntity : PersistentEntityBase
        {
            ManagedView<TEntity> managedView = BuildManagedView<TEntity>(ViewKind.ListView);
            managedView.Holder.Name = $"All {typeof(TEntity).Name}s";
            return AddView(managedView);
        }

        public async Task<ViewHolder> AddDetailView<TEntity>(Guid id) where TEntity : PersistentEntityBase
        {
            using IServiceScope scope = scopeFactory.CreateScope();
            OrderContext context = scope.ServiceProvider.GetRequiredService<OrderContext>();

            TEntity? existingItem = context.Set<TEntity>()
                                            .AsNoTracking()
                                            .FirstOrDefault(x => x.Id == id);

            return await AddDetailView(existingItem);
        }

        public async Task<ViewHolder> AddDetailView<TEntity>(TEntity? entity = null) where TEntity : PersistentEntityBase
        {
            ManagedView<TEntity> managedView = BuildManagedView<TEntity>(ViewKind.DetailView);
            managedView.Holder.ViewChanged += ViewChanged;
            managedView.Holder.Name = $"New {typeof(TEntity).Name}";

            if (entity != null)
            {
                PropertyInfo? identProp = factory.GetIdentifier(typeof(TEntity));
                if (identProp != null)
                {
                    object? identvalue = identProp.GetValue(entity);
                    managedView.Holder.Name = identvalue as string ?? string.Empty;
                }

                await ((IDetailView)managedView.Holder.View).LoadData(entity, managedView.Scope.ServiceProvider);
            }
            else
            {
                await ((IDetailView)managedView.Holder.View).LoadData(null, managedView.Scope.ServiceProvider);
            }

            return AddView(managedView);
        }

        public async Task AddAndShowDetailView<TEntity>() where TEntity : PersistentEntityBase
        {
            ViewHolder view = await AddDetailView<TEntity>();
            sp.GetRequiredService<MainForm>().ShowView(view);
        }

        /// <summary>
        /// Adds the Managed view or returns an existing view
        /// </summary>
        private ViewHolder AddView<TEntity>(ManagedView<TEntity> managedView) where TEntity : PersistentEntityBase
        {
            if (managedView.Kind == ViewKind.ListView)
            {
                IManagedView? openListView = views.FirstOrDefault(x => x.EntityType == typeof(TEntity));
                if (openListView != null)
                {
                    return openListView.Holder;
                }
            }

            if (managedView.Entity != null)
            {
                IManagedView? openDetailView = views.FirstOrDefault(x => x.Kind == ViewKind.DetailView && x.ManagedEntity?.Id == managedView.Entity.Id);
                if (openDetailView != null)
                {
                    return openDetailView.Holder;
                }
            }

            views.Add(managedView);

            return managedView.Holder;
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
                foreach (IControllerBase controller in view.Controllers)
                {
                    controller.Dispose();
                }

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

        public async Task<Result> DeleteAsync(ViewHolder holder)
        {
            IManagedView? managedView = views.FirstOrDefault(x => x.Holder == holder);
            if (managedView is null)
            {
                return Result.Fail("No managed View found");
            }

            IDataManipulationService service = GetDataService(managedView);
            return await service.DeleteAsync(managedView);
        }

        public async Task SaveAsync(ViewHolder holder)
        {
            IManagedView? managedView = views.FirstOrDefault(x => x.Holder == holder);
            if (managedView is null)
            {
                return;
            }

            IDataManipulationService service = GetDataService(managedView);
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
                if (managedView is { ManagedEntity: { } managedEntity } && managedEntity.CreatedAt != default)
                {
                    await dv.LoadData(managedEntity, managedView.Scope.ServiceProvider);
                }
                else
                {
                    object? entity = Activator.CreateInstance(holder.View.EntityType);
                    if (entity is PersistentEntityBase baseEntity)
                    {
                        await dv.LoadData(baseEntity, managedView.Scope.ServiceProvider);
                    }
                }
            }
        }

        private IDataManipulationService GetDataService(IManagedView managedView)
        {
            Type serviceType = typeof(DataManipulationService<>).MakeGenericType(managedView.EntityType);
            return (IDataManipulationService)managedView.Scope.ServiceProvider.GetRequiredService(serviceType);
        }
    }

    public interface IManagedView
    {
        IServiceScope Scope { get; set; }

        ViewHolder Holder { get; set; }

        ViewKind Kind { get; set; }

        Type EntityType { get; set; }

        PersistentEntityBase? ManagedEntity { get; set; }

        List<IControllerBase> Controllers { get; set; }
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

        public List<IControllerBase> Controllers { get; set; }
    }
}