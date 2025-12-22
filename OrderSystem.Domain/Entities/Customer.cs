namespace OrderSystem.Domain.Entities
{
    /// <summary>
    /// Kunde
    /// </summary>
    public class Customer : PersistentEntityBase
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }
    }
}