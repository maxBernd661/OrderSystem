namespace OrderSystem.Core.Entities
{
    /// <summary>
    /// Artikel
    /// </summary>
    public class Product : PersistentEntityBase
    {
        [Identifier]
        [Required]
        public string Name { get; set; }

        [ColumnName("Price per Unit")]
        [ClampDecimalValue(0.01f, 9999f)]
        public decimal UnitPrice { get; set; }

        [ClampValue(0.01f, 9999f)]
        public float Weight { get; set; }

        [ColumnName("Is Available")]
        public bool IsAvailable { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public sealed class ProductQueryProfile : IQueryProfile<Product>
    {
        public IQueryable<Product> Apply(IQueryable<Product> query)
        {
            return query;
        }
    }
}