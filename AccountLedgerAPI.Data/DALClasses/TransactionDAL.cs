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

        public async Task<List<Transaction>> GetTransactionsByCriteria(AccountLedgerContext dbContext, string? transactionTypeName, int skip, int take)
        {
            transactionTypeName ??= string.Empty;

            return await (from transaction in dbContext.Transactions
                          join transactionType in dbContext.TransactionTypes
                          on transaction.TransactionTypeId equals transactionType.TransactionTypeId
                          where transaction.DeletionDate == FirmamentUtilities.Utilities.DateHelper.DefaultDate &&
                                transactionType.TransactionTypeName.Contains(transactionTypeName)
                          select transaction).
                          OrderByDescending(transaction => transaction.TransactionTicks).
                          Skip(skip).
                          Take(take).
                          ToListAsync();
        }
    }
}
