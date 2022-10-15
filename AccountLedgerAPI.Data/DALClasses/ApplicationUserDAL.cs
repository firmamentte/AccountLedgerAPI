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

        public async Task<ApplicationUser> GetApplicationUserByAccessToken(AccountLedgerContext dbContext, string accessToken)
        {
            return await (from applicationUser in dbContext.ApplicationUsers
                          where applicationUser.AccessToken == accessToken
                          select applicationUser).
                          FirstOrDefaultAsync();
        }
        public async Task<bool> IsAccessTokenValid(AccountLedgerContext dbContext, string accessToken)
        {
            return await (from applicationUser in dbContext.ApplicationUsers
                          where applicationUser.AccessToken == accessToken &&
                                applicationUser.AccessTokenExpiryDate >= DateTime.Now.Date
                          select applicationUser).
                          AnyAsync();
        }
    }
}
