using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TH.RazorPages.Models;

namespace TH.RazorPages.Data
{
    public class DbContect : DbContext
    {
        public DbContect(DbContextOptions<DbContect> options): base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ImgProduct> ImgProducts { get; set; }
        public DbSet<Contact> contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)"); // Chỉ định kiểu dữ liệu cho cột Price trong SQL Server
        }
    }
}
