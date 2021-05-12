using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingMistakes.DataAccessLayer;
using CodingMistakes.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodingMistakes.BusinessLayer
{
    public class TransactionService : ITransactionService
    {
        private readonly DataContext dataContext;

        public TransactionService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<TransactionItem> GetAsync(int id)
        {
            // HACK: The following two lines of code produce the same SQL query. You can test them using a SQL Profiler.
            var transaction1 = await dataContext.Transactions.FirstOrDefaultAsync(t => t.Id == id);
            var transaction2 = await dataContext.Transactions.Where(t => t.Id == id).FirstOrDefaultAsync();

            return transaction1;
        }

        public async Task<IEnumerable<TransactionItem>> GetByProductIdAsync(int productId)
        {
            // HACK: The condition of the WHERE is translated in a SQL query, so it is efficiently excecuted on database side.
            // Only at the end we call ToListAsync() to materialize the (already filtered) list in memory.
            var transactions = await dataContext.Transactions.Include(t => t.Product).Where(t => t.ProductId == productId).ToListAsync();
            return transactions;
        }

        public async Task<IEnumerable<TransactionItem>> InMemoryGetByProductIdAsync(int productId)
        {
            // HACK: In this case, we first materialize all the table in memory and then we apply the WHERE condition.
            // This is extremely inefficient because we need to get all the database rows from the server.
            var transactions = (await dataContext.Transactions.Include(t => t.Product).ToListAsync()).Where(t => t.ProductId == productId);
            return transactions;
        }

        public async void LoadAll()
        {
            // HACK: This methods is incorrectly declared as async void, so is it impossible to await it.
            var transactions = await dataContext.Transactions.Include(t => t.Product).ToListAsync();
        }
    }
}
