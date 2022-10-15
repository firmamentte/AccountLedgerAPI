using System;
using System.Collections.Generic;

namespace AccountLedgerAPI.Data.Entities
{
    public partial class Transaction
    {
        public virtual decimal AccountBalance
        {
            get
            {
                return ApplicationUserAccount.AccountBalance;
            }
        }

        public virtual string TransactionTypeName
        {
            get
            {
                return TransactionType.TransactionTypeName;
            }
        }
    }
}
