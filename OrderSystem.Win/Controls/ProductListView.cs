namespace OrderSystem.Win.Controls
{
    public partial class ProductListView : UserControl
    {
        private readonly IServiceProvider serviceProvider;

        public ProductListView(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
        }
    }
}