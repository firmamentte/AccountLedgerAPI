using AccountLedgerAPI.BLL.DataContract;
using AccountLedgerAPI.Data.Entities;

namespace AccountLedgerAPI.BLL.BLLClasses
{
    public class SharedBLL
    {
        public TransactionResp FillTransactionResp(Transaction transaction)
        {
            return new TransactionResp()
            {
                AccountBalance = transaction.AccountBalance,
                TransactionDate = transaction.TransactionDate,
                TransactionAmount = transaction.TransactionAmount,
                TransactionName = transaction.TransactionName,
                TransactionType = transaction.TransactionTypeName,
                TransactionId = transaction.TransactionId
            };
        }

        public List<TransactionResp> FillTransactionResps(List<Transaction> transactions)
        {
            List<TransactionResp> _transactionResps = new();

            foreach (Transaction item in transactions)
            {
                _transactionResps.Add(FillTransactionResp(item));
            }

            return _transactionResps;
        }
    }
}
