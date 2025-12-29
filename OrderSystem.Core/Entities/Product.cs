namespace OrderSystem.Core.Entities
{
    /// <summary>
    /// Artikel
    /// </summary>
    public class Product : PersistentEntityBase
    {
        public string Name { get; set; }

        [ColumnName("Price per Unit")]
        public decimal UnitPrice { get; set; }

        public float Weight { get; set; }

        [ColumnName("Is Available")]
        public bool IsAvailable { get; set; }
    }

    public class ProductDTO : BaseDTO
    {
        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public float Weight { get; set; }

        public bool IsAvailable { get; set; }
    }
}