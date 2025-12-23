using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderSystem.Domain.Entities;

namespace OrderSystem.Domain.EntityConfig
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> b)
        {
            b.ToTable(nameof(Customer));
            b.HasKey(x => x.Id);

            b.HasMany<Order>("orders")
             .WithOne()
             .HasForeignKey(x => x.CustomerId)
             .OnDelete(DeleteBehavior.Cascade);

            b.Navigation(nameof(Customer.Orders)).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}