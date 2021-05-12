using CodingMistakes.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingMistakes.DataAccessLayer.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product", "Production");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnName("ProductId");

            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
            builder.Property(e => e.Number).HasColumnName("ProductNumber").HasMaxLength(25).IsRequired();
        }
    }
}
