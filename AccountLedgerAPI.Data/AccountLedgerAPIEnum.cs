using System.ComponentModel;

namespace AccountLedgerAPI.Data
{
    public class AccountLedgerAPIEnum
    {
        public enum TransactionType
        {
            [Description("credit")]
            credit,
            [Description("debit")]
            debit
        }
    }

}
