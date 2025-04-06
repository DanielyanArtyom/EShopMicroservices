using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderItemConfiguration: IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .HasConversion(customerId => customerId.Value, dbId => OrderItemId.Of(dbId));

        builder
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(p => p.ProductId);

        builder
            .Property(o => o.Quantity)
            .IsRequired();
        
        builder
            .Property(o => o.Price)
            .IsRequired();
    }
}