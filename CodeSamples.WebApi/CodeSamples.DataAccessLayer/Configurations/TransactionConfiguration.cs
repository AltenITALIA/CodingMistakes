using CodeSamples.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeSamples.DataAccessLayer.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<TransactionItem>
    {
        public void Configure(EntityTypeBuilder<TransactionItem> builder)
        {
            builder.ToTable("TransactionHistory", "Production");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnName("TransactionID");

            builder.Property(e => e.Date).HasColumnName("TransactionDate");
            builder.Property(e => e.Cost).HasColumnName("ActualCost");

            builder.HasOne(e => e.Product).WithMany().HasForeignKey(e => e.ProductId);
        }
    }
}
