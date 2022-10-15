using AccountLedgerAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountLedgerAPI.Data.DALClasses
{
    public class ApplicationUserDAL
    {
        public async Task<ApplicationUser> GetApplicationUserByCode(AccountLedgerContext dbContext, string applicationUserCode)
        {
            return await (from applicationUser in dbContext.ApplicationUsers
                          where applicationUser.ApplicationUserCode == applicationUserCode
                          select applicationUser).
                          FirstOrDefaultAsync();
        }
    }
}
