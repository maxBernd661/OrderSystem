using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core.Entities;
using OrderSystem.Win.Controls;
using OrderSystem.Win.Forms;
using OrderSystem.Win.Services;

namespace OrderSystem.Win.View
{
    /// <summary>
    /// Visual representation of a single entity
    /// </summary>
    /// <typeparam name="TEntity">An object inheriting <seealso cref="PersistentEntityBase"/>"</typeparam>
    public class DetailView<TEntity> : ViewBase, IDetailView where TEntity : PersistentEntityBase
    {
        /// <summary>
        /// The underlying <seealso cref="DetailViewDummy"/>
        /// </summary>
        public DetailViewDummy Template { get; }

        public override ViewKind Kind
        {
            get { return ViewKind.DetailView; }
        }

        /// <summary>
        /// Loads the provided entity into the view template and injects required lookup data into child controls.
        /// </summary>
        public async Task LoadData(object? entity, IServiceProvider sp, CancellationToken ct = default)
        {
            if (typeof(TEntity) == typeof(Order))
            {
                ICustomerLookupProvider provider = sp.GetRequiredService<ICustomerLookupProvider>();
                CustomerLookups lookups = await provider.GetLookups(ct);
                ApplyLookups(this, lookups);
            }
            else if (typeof(TEntity) == typeof(OrderItem))
            {
                IProductLookupProvider provider = sp.GetRequiredService<IProductLookupProvider>();
                ProductLookups lookups = await provider.GetLookups(ct);
                ApplyLookups(this, lookups);
            }

            Template.LoadData(entity);
        }

        private void ApplyLookups<TLookup>(Control root, TLookup lookup) where TLookup : IEntityLookup
        {
            Visit(root, lookup);
        }

        /// <summary>
        /// Feeds data provider via the given IEntityLookup
        /// </summary>
        private void Visit<TLookup>(Control control, TLookup lookup) where TLookup : IEntityLookup
        {
            if (control is IRequireLookup<TLookup> needsLookup)
            {
                needsLookup.SetLookup(lookup);
            }

            foreach (Control child in control.Controls)
            {
                Visit(child, lookup);
            }
        }

        public PersistentEntityBase ReadData()
        {
            return (PersistentEntityBase)Template.ReadData();
        }

        /// <summary>
        /// Creation is handled exclusively by <see cref="ViewFactory"/>.
        /// Do not call this constructor manually.
        /// </summary>
        public DetailView(IServiceProvider sp, DetailViewDummy template) : base(sp)
        {
            InitializeCore<TEntity>();
            Template = template;
            Template.Changed += (sender, args) => OnChanged();
            Dock = DockStyle.Fill;
            Control content = template.Root;
            content.Dock = DockStyle.Fill;
            Controls.Add(content);

            MaterializeOthers(this);
        }

        private void MaterializeOthers(Control root)
        {
            foreach (Control control in root.Controls)
            {
                if (control is ListViewDummy dummy)
                {
                    MaterializeListView(dummy);
                }

                MaterializeOthers(control);
            }
        }

        /// <summary>
        /// builds a proper <seealso cref="ListView{TEntity}"/> for the given <paramref name="dummy"/>
        /// </summary>
        /// <param name="dummy">A <seealso cref="ListViewDummy"/> nested inside the template</param>
        private void MaterializeListView(ListViewDummy dummy)
        {
            Control? parent = dummy.Parent;
            if (parent is null)
            {
                return;
            }

            int index = parent.Controls.GetChildIndex(dummy);
            Type entityType = ServiceProvider.GetRequiredService<ViewFactory>().GetTypeByName(dummy.EntityType);
            Type listType = typeof(ListView<>).MakeGenericType(entityType);

            Control listView = (Control)ActivatorUtilities.CreateInstance(ServiceProvider, listType);

            listView.Dock = dummy.Dock;
            listView.Margin = dummy.Margin;
            listView.Padding = dummy.Padding;
            listView.Size = dummy.Size;
            listView.Location = dummy.Location;
            listView.Anchor = dummy.Anchor;
            listView.MinimumSize = dummy.MinimumSize;
            listView.MaximumSize = dummy.MaximumSize;
            listView.Visible = dummy.Visible;
            listView.Enabled = dummy.Enabled;
            ((IListView)listView).SetIdent(dummy.Ident);

            parent.SuspendLayout();
            parent.Controls.Remove(dummy);
            parent.Controls.Add(listView);
            parent.Controls.SetChildIndex(listView, index);
            parent.ResumeLayout(true);

            dummy.Dispose();
        }
    }

    /// <summary>
    /// A control implementing this interface allows allows loading and saving data from entities inheriting <seealso cref="PersistentEntityBase"/>
    /// </summary>
    public interface IDataControl
    {
        void LoadData(object? entity);

        object GetData();

        event EventHandler<EventArgs> Changed;
    }

    /// <inheritdoc cref="IDataControl"/>
    public interface IDataControl<TEntity> : IDataControl
    {
        void LoadData(TEntity? entity);

        new TEntity GetData();
    }

    public interface IDetailView
    {
        public Task LoadData(object? entity, IServiceProvider sp, CancellationToken ct = default);

        public PersistentEntityBase ReadData();

        public DetailViewDummy Template { get; }

        public event EventHandler<EventArgs> Changed;
    }
}