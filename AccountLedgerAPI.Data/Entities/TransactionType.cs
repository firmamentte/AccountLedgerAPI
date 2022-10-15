using System;
using System.Collections.Generic;

namespace AccountLedgerAPI.Data.Entities
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            Transactions = new HashSet<Transaction>();
        }

        public Guid TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; } = null!;

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
