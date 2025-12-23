using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderSystem.Domain.Entities;

namespace OrderSystem.Domain.EntiityConfig
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
             .WithMany()
             .HasForeignKey(x => x.CustomerId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasMany<OrderItem>("items")
             .WithOne()
             .HasForeignKey(x => x.OrderId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasMany<OrderStatusHistory>("history")
             .WithOne()
             .HasForeignKey(x => x.OrderId)
             .OnDelete(DeleteBehavior.Cascade);

            b.Navigation(nameof(Order.Items)).UsePropertyAccessMode(PropertyAccessMode.Field);
            b.Navigation(nameof(Order.History)).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}