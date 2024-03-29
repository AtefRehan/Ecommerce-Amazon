//using ECommerce.Models;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

//namespace ECommerce.Context
//{
//    public class AmazonDB :  IdentityDbContext<ApplicationUser>    //DbContext
//    {
//        public AmazonDB() : base()
//        {

//        }
//        public AmazonDB(DbContextOptions options) : base(options)
//        {
//        }
//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder); // Call the base method first

//            modelBuilder.Entity<OrderProduct>()
//                .HasOne(op => op.Order)
//                .WithMany(o => o.OrderProducts)
//                .HasForeignKey(op => op.OrderId)
//                .OnDelete(DeleteBehavior.Cascade);

//            modelBuilder.Entity<OrderProduct>()
//                .HasOne(op => op.Product)
//                .WithMany(p => p.OrderProducts)
//                .HasForeignKey(op => op.ProductId)
//                .OnDelete(DeleteBehavior.Restrict); // Set the delete behavior to Restrict here



//            modelBuilder.Entity<ProductInCart>()
//                .HasKey(p => p.Id);

//            modelBuilder.Entity<ProductInCart>()
//                .HasOne(p => p.Product)
//                .WithMany()
//                .HasForeignKey(p => p.ProductId)
//                .OnDelete(DeleteBehavior.Restrict);

//            // modelBuilder.Entity<ProductInCart>()
//            //     .HasOne(p => p.Cart)
//            //     .WithMany()
//            //     // .HasForeignKey(p => p.CartId);
//        }

//        public DbSet<Product> Products { get; set; }
//        // public DbSet<User> Users { get; set; }
//        public DbSet<Order> Orders { get; set; }
//        public DbSet<Cart> Carts { get; set; }
//        public DbSet<SubCategory> SubCategories { get; set; }
//        public DbSet<Category> Categories { get; set; }
//        public DbSet<Payment> payments { get; set; }
//        public DbSet<Supplier> Suppliers { get; set; }
//        public DbSet<ProductInCart> ProductInCart { get; set; }
//        // public DbSet<Role> Roles { get; set; }
//    }
//}
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
