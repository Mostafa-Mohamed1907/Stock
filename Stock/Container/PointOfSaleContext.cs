using Microsoft.EntityFrameworkCore;
using Stock.Models;
using Stock.SeedData;

namespace Stock.Container
{
    public class PointOfSaleContext:DbContext
    {
        public PointOfSaleContext() { }
        public PointOfSaleContext(DbContextOptions <PointOfSaleContext> options) : base(options) { }
        public virtual DbSet<Category> category { get; set; }
        public virtual DbSet<Product> products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategorySeedConfiguration());
            modelBuilder.ApplyConfiguration(new ProductSeedConfiguration());
        }
    }
}
