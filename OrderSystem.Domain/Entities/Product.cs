namespace OrderSystem.Domain.Entities
{
    /// <summary>
    /// Artikel
    /// </summary>
    public class Product : PersistentEntityBase
    {
        public string Name { get; set; }

        public decimal UnitPrice { get; set; }
    }
}