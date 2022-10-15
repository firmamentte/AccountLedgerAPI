using System;
using System.Collections.Generic;

namespace AccountLedgerAPI.Data.Entities
{
    public partial class ApplicationUserAccount
    {
        public ApplicationUserAccount()
        {
            Transactions = new HashSet<Transaction>();
        }

        public Guid ApplicationUserAccountId { get; set; }
        public Guid ApplicationUserId { get; set; }
        public string AccountNumber { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        public decimal AccountBalance { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
