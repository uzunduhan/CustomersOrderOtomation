using Microsoft.EntityFrameworkCore;
using CustomersOrderOtomation.Data.Models;
using System.Reflection;

namespace CustomersOrderOtomation.Data.DBOperations
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ShopList> ShopLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductCategory>()
              .HasOne(c => c.Product)
              .WithMany(cp => cp.Product_Categories)
              .HasForeignKey(ci => ci.ProductId);

            modelBuilder.Entity<ProductCategory>()
              .HasOne(c => c.Category)
              .WithMany(cp => cp.Product_Categories)
              .HasForeignKey(ci => ci.CategoryId);

        }
    }
}