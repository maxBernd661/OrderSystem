using OrderSystem.Core.Entities;
using OrderSystem.Win.Controls;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Forms
{
    public partial class PopupView : Form
    {
        private readonly IServiceProvider serviceProvider;
        private ViewHolder holder;

        public PersistentEntityBase? ReturnedItem { get; private set; }
        public Type? EntityType { get; private set; }

        public PopupView(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            labelValidation.TextChanged += LabelValidationOnTextChanged;
        }

        private async void LabelValidationOnTextChanged(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(labelValidation.Text))
            {
                return;
            }

            await Task.Delay(TimeSpan.FromSeconds(5));
            labelValidation.Text = string.Empty;
        }

        public bool ShowView(ViewHolder viewHolder)
        {
            holder = viewHolder;

            Text = viewHolder.Name;
            panel.Controls.Add(viewHolder);
            viewHolder.Dock = DockStyle.Fill;

            return ShowDialog() == DialogResult.OK;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (holder.View is not IDetailView dv)
            {
                return;
            }

            PersistentEntityBase curItem = dv.ReadData();
            Result validationResult = curItem.SoftValidate();
            if (!validationResult.IsSuccess)
            {
                labelValidation.Text = validationResult.Error;
                return;
            }

            ReturnedItem = curItem;
            EntityType = holder.View.EntityType;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}