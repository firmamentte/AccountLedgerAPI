using AccountLedgerAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountLedgerAPI.Data.DALClasses
{
    public class TransactionDAL
    {
        public async Task<Transaction> GetTransactionById(AccountLedgerContext dbContext, Guid transactionId)
        {

            return await (from transaction in dbContext.Transactions
                          where transaction.TransactionId == transactionId
                          select transaction).
                          FirstOrDefaultAsync();
        }
    }
}
