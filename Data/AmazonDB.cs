using ECommerce.Models;
using Microsoft.AspNetCore.Identity;

//using Microsoft.AspNetCore.Identity;
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
   
            base.OnModelCreating(builder);

            #region Add Master Admin 
            var hasher = new PasswordHasher<ApplicationUser>();

            var adminEmail = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SiteSettings")["AdminEmail"];
            var adminPassword = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SiteSettings")["AdminPassword"];

            IdentityRole admin = new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" };
            ;
            builder.Entity<IdentityRole>().HasData(admin, new IdentityRole { Name = "Client", NormalizedName = "CLIENT" });
            ApplicationUser MasterAdmin = new ApplicationUser
            {
                Id = "80c8b6b1-e2b6-45e8-b044-8f2178a90111", // primary key
                UserName = "admin",
                NormalizedUserName = adminEmail.ToUpper(),
                PasswordHash = hasher.HashPassword(null, adminPassword),
                Email = adminEmail,
                NormalizedEmail = adminEmail.ToUpper(),
                CartId = 1
            };
            builder.Entity<Cart>().HasData(
            new Cart() { CartId = 1, ApplicationUserId = MasterAdmin.Id });
            builder.Entity<ApplicationUser>().HasData(MasterAdmin);
            builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = MasterAdmin.Id, RoleId = admin.Id });
            #endregion
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ProductInCart> ProductInCart { get; set; }
        public DbSet<WishProduct> Wish { get; set; }
    }
}
