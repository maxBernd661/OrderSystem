using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderSystem.Core.Entities;

namespace OrderSystem.Core.EntityConfig
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> b)
        {
            b.ToTable(nameof(Customer));
            b.HasKey(x => x.Id);

            b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            b.Property(x => x.Email).HasMaxLength(200);

            b.Navigation(x => x.Orders).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}