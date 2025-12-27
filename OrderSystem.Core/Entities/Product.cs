namespace OrderSystem.Core.Entities
{
    /// <summary>
    /// Artikel
    /// </summary>
    public class Product : PersistentEntityBase
    {
        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public float Weight { get; set; }
    }

    public class ProductDTO : BaseDTO
    {
        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public float Weight { get; set; }
    }
}