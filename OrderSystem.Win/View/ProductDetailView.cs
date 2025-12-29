using OrderSystem.Core;

namespace OrderSystem.Win.View
{
    public partial class ProductDetailView : DetailView
    {
        public ProductDetailView()
        {
            InitializeComponent();
        }

        public ProductDetailView(OrderContext context) : base(context)
        {
            InitializeComponent();
        }
    }
}