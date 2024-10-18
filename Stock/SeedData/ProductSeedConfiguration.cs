using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Models;

namespace Stock.SeedData
{
    public class ProductSeedConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { Id = 1, Name = "Riche", Description="V2", Price=100, CategoryId=1 },
                new Product { Id = 2, Name = "HP Laptop", Description = "Laptop", Price = 2000, CategoryId = 2 },
                new Product { Id = 3, Name = "Dell", Description = "Laptop", Price = 1500, CategoryId = 2 },
                new Product { Id = 4, Name = "Test", Description = "Test V2", Price = 1750, CategoryId = 1 }
                );
        }
    }
}
