using System;
using System.Collections.Generic;

namespace AccountLedgerAPI.Data.Entities
{
    public partial class Transaction
    {
        public Guid TransactionId { get; set; }
        public Guid ApplicationUserAccountId { get; set; }
        public Guid TransactionTypeId { get; set; }
        public string TransactionName { get; set; } = null!;
        public decimal TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public long TransactionTicks { get; set; }

        public virtual ApplicationUserAccount ApplicationUserAccount { get; set; } = null!;
        public virtual TransactionType TransactionType { get; set; } = null!;
    }
}
