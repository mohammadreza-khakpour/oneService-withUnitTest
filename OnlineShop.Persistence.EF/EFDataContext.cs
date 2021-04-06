using Microsoft.EntityFrameworkCore;
using OnlineShop.Entities;
using System;
using System.Reflection;

namespace OnlineShop.Persistence.EF
{
    public class EFDataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=ShopDB;trusted_connection=true");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Good> Products { get; set; }
        public DbSet<GoodCategory> ProductCategories { get; set; }
    }
}
