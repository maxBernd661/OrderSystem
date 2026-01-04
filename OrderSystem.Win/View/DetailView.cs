using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core.Entities;
using OrderSystem.Win.Controls;
using OrderSystem.Win.Forms;
using OrderSystem.Win.Services;

namespace OrderSystem.Win.View
{
    public class DetailView<TEntity> : ViewBase, IDetailView where TEntity : PersistentEntityBase
    {
        public void LoadData(object? entity, IServiceProvider sp)
        {
            Template.LoadData(entity, sp);
        }

        public PersistentEntityBase ReadData()
        {
            return (PersistentEntityBase)Template.ReadData();
        }

        public DetailViewDummy Template { get; }

        public override ViewKind Kind
        {
            get { return ViewKind.DetailView; }
        }

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

            parent.SuspendLayout();
            parent.Controls.Remove(dummy);
            parent.Controls.Add(listView);
            parent.Controls.SetChildIndex(listView, index);
            parent.ResumeLayout(true);

            dummy.Dispose();
        }
    }

    public interface IDataControl
    {
        void LoadData(object? entity);

        object GetData();

        event EventHandler<EventArgs> Changed;
    }

    public interface IDataControl<TEntity> : IDataControl
    {
        void LoadData(TEntity? entity);

        new TEntity GetData();
    }

    public interface IComplexDataControl : IDataControl
    {
        void LoadData(object? entity, IServiceProvider serviceProvider);
    }

    public interface IComplexDataControl<TEntity> : IComplexDataControl, IDataControl<TEntity>
    {
        void LoadData(TEntity? entity, IServiceProvider serviceProvider);
    }

    public interface IDetailView
    {
        public void LoadData(object? entity, IServiceProvider sp);

        public PersistentEntityBase ReadData();

        public DetailViewDummy Template { get; }

        public event EventHandler<EventArgs> Changed;
    }
}