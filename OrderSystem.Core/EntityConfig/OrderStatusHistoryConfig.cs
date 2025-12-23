using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderSystem.Core.Entities;

namespace OrderSystem.Core.EntityConfig
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

            b.HasIndex(x => x.OrderId);
        }
    }
}