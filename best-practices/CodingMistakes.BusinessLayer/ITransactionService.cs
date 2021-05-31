using System.Collections.Generic;
using System.Threading.Tasks;
using CodingMistakes.DataAccessLayer.Entities;

namespace CodingMistakes.BusinessLayer
{
    public interface ITransactionService
    {
        Task<TransactionItem> GetAsync(int id);

        Task<IEnumerable<TransactionItem>> GetByProductIdAsync(int productId);

        Task<IEnumerable<TransactionItem>> InMemoryGetByProductIdAsync(int productId);

        void LoadAll();
    }
}