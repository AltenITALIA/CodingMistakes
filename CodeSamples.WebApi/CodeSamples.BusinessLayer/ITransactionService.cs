using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSamples.DataAccessLayer.Entities;

namespace CodeSamples.BusinessLayer
{
    public interface ITransactionService
    {
        Task<TransactionItem> GetAsync(int id);

        Task<IEnumerable<TransactionItem>> GetByProductIdAsync(int productId);

        Task<IEnumerable<TransactionItem>> InMemoryGetByProductIdAsync(int productId);
    }
}