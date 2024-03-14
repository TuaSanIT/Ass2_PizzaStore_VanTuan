using Ass2_PizzaStore_VanTuan.Models;
using Microsoft.EntityFrameworkCore;

namespace Ass2_PizzaStore_VanTuan.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(en =>
            {
                en.HasKey(e => e.AccountID);
            });

            modelBuilder.Entity<Categories>(en =>
            {
                en.HasKey(e => e.CategoryID);
            });

            modelBuilder.Entity<Supplier>(en =>
            {
                en.HasKey(e => e.SupplierID);
            });

            modelBuilder.Entity<Customers>(en =>
            {
                en.HasKey(e => e.CustomerID);
            });

            modelBuilder.Entity<Order>(en =>
            {
                en.HasKey(p => p.OrderID);

                en.HasOne(c => c.Customer).WithMany(o => o.Orders).HasForeignKey(o => o.CustomerID);

            });

            modelBuilder.Entity<OrderDetail>().HasNoKey();

            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderID, od.ProductID });

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductID);

        }

    }
}
