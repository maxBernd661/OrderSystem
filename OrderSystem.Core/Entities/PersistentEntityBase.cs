namespace OrderSystem.Core.Entities
{
    /// <summary>
    /// Persistente Basisklasse
    /// </summary>
    public abstract class PersistentEntityBase
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public void Delete()
        {
            IsDeleted = true;
        }
    }

    public abstract class BaseDTO
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}