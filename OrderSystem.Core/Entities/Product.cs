namespace OrderSystem.Core.Entities
{
    /// <summary>
    /// Artikel
    /// </summary>
    public class Product : PersistentEntityBase
    {
        [Identifier]
        public string Name { get; set; }

        [ColumnName("Price per Unit")]
        public decimal UnitPrice { get; set; }

        public float Weight { get; set; }

        [ColumnName("Is Available")]
        public bool IsAvailable { get; set; }
    }
}