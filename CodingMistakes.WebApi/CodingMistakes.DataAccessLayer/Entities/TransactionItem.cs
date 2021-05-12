using System;

namespace CodingMistakes.DataAccessLayer.Entities
{
    public class TransactionItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public DateTime Date { get; set; }

        public int Quantity { get; set; }

        public decimal Cost { get; set; }

        public virtual Product Product { get; set; }
    }
}
