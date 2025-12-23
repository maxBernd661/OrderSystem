using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderSystem.Domain.Entities;

namespace OrderSystem.Domain.EntityConfig
{
    public class OrderStatusHistoryConfig : IEntityTypeConfiguration<OrderStatusHistory>
    {
        public void Configure(EntityTypeBuilder<OrderStatusHistory> b)
        {
            b.ToTable(nameof(OrderStatusHistory));
            b.HasKey(x => x.Id);

            b.Property(x => x.ChangedTo)
             .HasConversion<string>()
             .IsRequired();

            b.Property(x => x.ChangedFrom)
             .HasConversion<string>();

            b.HasOne(x => x.Order)
             .WithMany("history")
             .HasForeignKey(x => x.OrderId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasIndex(x => x.OrderId);
        }
    }
}