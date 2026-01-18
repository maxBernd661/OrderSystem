using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.Win.Controls;
using OrderSystem.Win.Forms;
using OrderSystem.Win.View;
using OrderSystem.Win.ViewControllers;
using System.Reflection;

namespace OrderSystem.Win.Services
{
    /// <summary>
    /// Coordinates the lifecycle, scoping and reuse of views within the application
    /// </summary>
    ///
    /// <remarks>
    ///
    /// <para>
    /// <seealso cref="ViewManager"/> is responsible for managing active views and their associated DI scopes, controllers and entities.
    /// </para>
    ///
    /// <para>
    /// Each view is wrapped in a managed container that tracks its scope, the instantiated view (and its holder), associated controllers and the currently loaded entity.
    /// </para>
    ///
    /// <para>
    /// Enforces at most one list view per entity type and at most one detail view per entity instance. <br/>
    /// Controllers and scopes are disposed together with their views.
    /// </para>
    ///
    /// <para>
    /// View creation is handled by <see cref="ViewFactory"/>, while lifetime and reuse are handled here
    /// </para>
    ///
    /// </remarks>
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

        /// <summary>
        /// Adds a list view for the given enttiy type or returns an existing one
        /// </summary>
        /// <typeparam name="TEntity">Entity type displayed in the list view</typeparam>
        /// <returns>A <see cref="ViewHolder"/> representing the list view</returns>
        public ViewHolder AddListView<TEntity>() where TEntity : PersistentEntityBase
        {
            ManagedView<TEntity> managedView = BuildManagedView<TEntity>(ViewKind.ListView);
            managedView.Holder.Name = $"All {typeof(TEntity).Name}s";
            return AddView(managedView);
        }

        /// <summary>
        /// Adds a detail view for an existing entity identified via its <paramref name="id"/> or returns an existing one
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to be displayed</typeparam>
        /// <returns>A <see cref="ViewHolder"/> representing the detail view</returns>
        public async Task<ViewHolder> AddDetailView<TEntity>(Guid id) where TEntity : PersistentEntityBase
        {
            using IServiceScope scope = scopeFactory.CreateScope();
            OrderContext context = scope.ServiceProvider.GetRequiredService<OrderContext>();

            TEntity? existingItem = context.Set<TEntity>()
                                            .AsNoTracking()
                                            .FirstOrDefault(x => x.Id == id);

            return await AddDetailView(existingItem);
        }

        /// <summary>
        /// Adds a detail view for a new or existing entity instance
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to be displayed</typeparam>
        /// <param name="entity">
        /// Optional entity instance. If <c>null</c>, a new entity is created.
        /// </param>
        /// <returns>A <see cref="ViewHolder"/> representing the detail view</returns>
        public async Task<ViewHolder> AddDetailView<TEntity>(TEntity? entity = null) where TEntity : PersistentEntityBase
        {
            ManagedView<TEntity> managedView = BuildManagedView<TEntity>(ViewKind.DetailView);
            managedView.Holder.ViewChanged += ViewChanged;
            managedView.Holder.Name = $"New {typeof(TEntity).Name}";

            if (entity == null)
            {
                entity = (TEntity)Activator.CreateInstance(typeof(TEntity))!;
            }
            else
            {
                PropertyInfo? identProp = factory.GetIdentifier(typeof(TEntity));
                if (identProp != null)
                {
                    object? identvalue = identProp.GetValue(entity);
                    managedView.Holder.Name = identvalue as string ?? string.Empty;
                }
            }

            managedView.ManagedEntity = entity;
            await ((IDetailView)managedView.Holder.View).LoadData(entity, managedView.Scope.ServiceProvider);

            return AddView(managedView);
        }

        public async Task AddAndShowDetailView<TEntity>() where TEntity : PersistentEntityBase
        {
            ViewHolder view = await AddDetailView<TEntity>();
            sp.GetRequiredService<MainForm>().ShowView(view);
        }

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

        /// <summary>
        /// Disposes the specified view, including its controllers and DI scope.
        /// </summary>
        /// <param name="holder">The view holder to dispose.</param>
        /// <returns>
        /// <c>true</c> if the view was found and disposed; otherwise <c>false</c>.
        /// </returns>
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

        /// <summary>
        /// Deletes the entity associated with the specified view.
        /// </summary>
        /// <param name="holder">The view holder to delete.</param>
        /// <returns>A result indicating success or failure.</returns>
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

        /// <summary>
        /// Saves the data of the specified view and reloads its state.
        /// </summary>
        /// <param name="holder">The view holder to save.</param>
        public async Task SaveAsync(ViewHolder holder)
        {
            IManagedView? managedView = views.FirstOrDefault(x => x.Holder == holder);
            if (managedView is null)
            {
                return;
            }

            IDataManipulationService service = GetDataService(managedView);
            PersistentEntityBase savedItem = await service.SaveAsync(managedView);
            managedView.ManagedEntity = savedItem;

            if (holder.View is IDetailView dv)
            {
                holder.Text = dv.ReadData().GetIdentifier();
            }

            await ReloadView(holder);
        }

        /// <summary>
        /// Reloads the data of the specified view
        /// </summary>
        /// <param name="holder">The view holder to reload</param>
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