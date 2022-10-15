
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
    }
}
