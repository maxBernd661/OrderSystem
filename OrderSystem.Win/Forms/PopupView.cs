using OrderSystem.Win.Controls;

namespace OrderSystem.Win.Forms
{
    public partial class PopupView : Form
    {
        private readonly IServiceProvider serviceProvider;

        public PopupView(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
        }

        public void ShowView(ViewHolder holder)
        {
            Text = holder.Name;
            panel.Controls.Add(holder);
            holder.Dock = DockStyle.Fill;

            ShowDialog();
        }
    }
}