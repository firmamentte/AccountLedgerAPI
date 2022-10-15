
using AccountLedgerAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountLedgerAPI.Data.DALClasses
{
    public class ApplicationUserAccountDAL
    {
        public async Task<ApplicationUserAccount> GetApplicationUserAccountByAccountNumber(AccountLedgerContext dbContext, string accountNumber)
        {
            return await (from applicationUserAccount in dbContext.ApplicationUserAccounts
                          where applicationUserAccount.AccountNumber == accountNumber
                          select applicationUserAccount).
                          FirstOrDefaultAsync();
        }

        public async Task<bool> IsAccountNumberExisting(AccountLedgerContext dbContext, string accountNumber)
        {
            return await (from applicationUserAccount in dbContext.ApplicationUserAccounts
                          where applicationUserAccount.AccountNumber == accountNumber
                          select applicationUserAccount).
                          AnyAsync();
        }

        public async Task<List<ApplicationUserAccount>> GetApplicationUserAccountsByCriteria(AccountLedgerContext dbContext, string? accountNumber, string? accountName)
        {
            accountNumber ??= string.Empty;
            accountName ??= string.Empty;

            return await (from applicationUserAccount in dbContext.ApplicationUserAccounts
                          where applicationUserAccount.AccountNumber.Contains(accountNumber) &&
                                applicationUserAccount.AccountName.Contains(accountName)
                          select applicationUserAccount).
                          ToListAsync();
        }
    }
}
