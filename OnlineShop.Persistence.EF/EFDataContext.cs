using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnlineShop.Entities;
using System;
using System.Reflection;

namespace OnlineShop.Persistence.EF
{
    public class EFDataContext : DbContext
    {
        public EFDataContext(string connectionString)
            : this(new DbContextOptionsBuilder<EFDataContext>().UseSqlite(connectionString).Options)
        {
        }
        private EFDataContext(DbContextOptions<EFDataContext> options)
            : this((DbContextOptions)options)
        {
        }
        protected EFDataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public override ChangeTracker ChangeTracker
        {
            get
            {
                var tracker = base.ChangeTracker;
                tracker.LazyLoadingEnabled = false;
                tracker.AutoDetectChangesEnabled = true;
                tracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
                return tracker;
            }
        }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<GoodCategory> GoodCategories { get; set; }
    }
}
