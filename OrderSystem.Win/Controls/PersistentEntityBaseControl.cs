using OrderSystem.Core.Entities;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Controls
{
    public partial class PersistentEntityBaseControl : UserControl, IDataControl<PersistentEntityBase>
    {
        public PersistentEntityBaseControl()
        {
            InitializeComponent();
        }

        public void LoadData(object? entity)
        {
            LoadData((PersistentEntityBase)entity!);
        }

        public PersistentEntityBase GetData()
        {
            return savedItem ?? new Product();
        }

        public event EventHandler<EventArgs>? Changed;

        public void LoadData(PersistentEntityBase? entity)
        {
            if (entity == null)
            {
                textBoxId.Text = Guid.Empty.ToString();
                textBoxCreated.Text = DateTime.MinValue.ToString("dd.MM.yyyy : HH:mm");
                textBoxUpdated.Text = DateTime.MinValue.ToString("dd.MM.yyyy : HH:mm");
            }
            else
            {
                savedItem = entity;
                textBoxId.Text = entity.Id.ToString();
                textBoxCreated.Text = entity.CreatedAt.ToString("dd.MM.yyyy : HH:mm");
                textBoxUpdated.Text = entity.UpdatedAt.ToString("dd.MM.yyyy : HH:mm");
            }
        }

        private PersistentEntityBase? savedItem;

        object IDataControl.GetData()
        {
            return GetData();
        }
    }
}