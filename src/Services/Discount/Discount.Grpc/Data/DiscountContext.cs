using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountContext : DbContext
{
    public DbSet<CouponModel> Coupons { get; set; } = null!;
    
    public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<CouponModel>().HasData(
            new CouponModel
            {
                Id = 1,
                ProductName = "IPhone X",
                Description = "IPhone Discount",
                Amount = 150
            },
            new CouponModel
            {
                Id = 2,
                ProductName = "Samsung 10",
                Description = "Samsung Discount",
                Amount = 100
            },
            new CouponModel
            {
                Id = 3,
                ProductName = "Google Pixel",
                Description = "Google Pixel Discount",
                Amount = 50
            }
        );
        
        base.OnModelCreating(modelBuilder);
    }
}