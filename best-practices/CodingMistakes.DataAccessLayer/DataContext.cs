using System.Reflection;
using CodingMistakes.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodingMistakes.DataAccessLayer
{
    public class DataContext : DbContext
    {
        public DbSet<TransactionItem> Transactions { get; set; }

        public DbSet<Product> Products { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
