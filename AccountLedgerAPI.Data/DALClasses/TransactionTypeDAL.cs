using AccountLedgerAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountLedgerAPI.Data.DALClasses
{
    public class TransactionTypeDAL
    {
        public async Task<TransactionType> GetTransactionTypeByTypeName(AccountLedgerContext dbContext, string transactionTypeName)
        {

            return await (from transactionType in dbContext.TransactionTypes
                          where transactionType.TransactionTypeName == transactionTypeName
                          select transactionType).
                          FirstOrDefaultAsync();
        }
    }
}
