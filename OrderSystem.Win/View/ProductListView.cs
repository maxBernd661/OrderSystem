using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core;
using OrderSystem.Core.Entities;

namespace OrderSystem.Win.View
{
    public partial class ProductListView : ListView
    {
        public ProductListView()
        {
            InitializeComponent();
            InitializeView<Product>(dataGrid, productDTOBindingSource);
        }

        [ActivatorUtilitiesConstructor]
        public ProductListView(OrderContext context) : base(context)
        {
            InitializeComponent();
            InitializeView<Product>(dataGrid, productDTOBindingSource);
        }

        public override async Task LoadData(Guid? id)
        {
            productDTOBindingSource.DataSource = new List<ProductDTO>();
            productDTOBindingSource.Clear();
            List<Product> products = await context.Products
                                                  .AsNoTracking()
                                                  .Where(x => !x.IsDeleted)
                                                  .ToListAsync();

            foreach (Product product in products)
            {
                ProductDTO dto = new()
                {
                    Id = product.Id,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt,
                    Name = product.Name,
                    UnitPrice = product.UnitPrice,
                    Weight = product.Weight,
                    IsAvailable = product.IsAvailable
                };
                productDTOBindingSource.Add(dto);
            }
        }

        public override Task SaveData()
        {
            return Task.CompletedTask;
        }
    }
}