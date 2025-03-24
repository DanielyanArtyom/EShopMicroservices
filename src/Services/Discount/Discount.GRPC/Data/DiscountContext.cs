using Discount.GRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data;

public class DiscountContext: DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;
    
    public DiscountContext(DbContextOptions<DiscountContext> context) : base(context){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductName = "Iphone X", Description = "Iphone Description", Amount = 8 },
            new Coupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung Description", Amount = 5 }
        );
    }
}