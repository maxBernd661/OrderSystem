using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderSystem.Core.Entities;

namespace OrderSystem.Core.EntityConfig
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> b)
        {
            b.ToTable(nameof(Order));
            b.HasKey(x => x.Id);

            b.Property(x => x.Status)
             .HasConversion<string>()
             .IsRequired();

            b.HasOne(x => x.Customer)
             .WithMany(x => x.Orders)
             .HasForeignKey(x => x.CustomerId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasMany(x => x.Items)
             .WithOne(x => x.Order)
             .HasForeignKey(x => x.OrderId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.History)
             .WithOne(x => x.Order)
             .HasForeignKey(x => x.OrderId)
             .OnDelete(DeleteBehavior.Cascade);

            b.Navigation(x => x.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
            b.Navigation(x => x.History).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}