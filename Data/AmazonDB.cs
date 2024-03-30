using ECommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ECommerce.Data
{
    public class AmazonDB : IdentityDbContext<ApplicationUser>
    {
        public AmazonDB() : base()
        {

        }
        public AmazonDB(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<WishProduct>().HasKey(k => new { k.ProductId, k.UserId });
            builder.Entity<ProductInCart>()
            .HasKey(p => p.Id);
            //
            // builder.Entity<ProductInCart>()
            //     .HasOne(p => p.Product)
            //     .WithMany()
            //     .HasForeignKey(p => p.ProductId)
            //     .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ProductInCart> ProductInCart { get; set; }
        public DbSet<WishProduct> Wish { get; set; }
    }
}
