using AccountLedgerAPI.BLL.DataContract;
using AccountLedgerAPI.Data.DALClasses;
using AccountLedgerAPI.Data.Entities;

namespace AccountLedgerAPI.BLL.BLLClasses
{
    public class ApplicationUserBLL
    {
        private readonly ApplicationUserDAL ApplicationUserDAL;

        public ApplicationUserBLL()
        {
            ApplicationUserDAL = new();
        }

        public async Task<string> Register(RegisterReq registerReq)
        {
            using AccountLedgerContext _dbContext = new();

            ApplicationUser _applicationUser = new()
            {
                ApplicationUserCode = FirmamentUtilities.Utilities.RandomPasswordHelper.Generate(6, 6),
                PersonalDetail = new()
                {
                    FirstName = registerReq.FirstName,
                    LastName = registerReq.LastName,
                    Dob = registerReq.DOB,
                    Contact = new()
                    {
                        MobileNumber = registerReq.Contact.MobileNumber,
                        EmailAddress = registerReq.Contact.EmailAddress,
                        AddressLine1 = registerReq.Contact.AddressLine1,
                        AddressLine2 = registerReq.Contact.AddressLine2,
                        CityOrTown = registerReq.Contact.CityOrTown,
                        StateOrProvince = registerReq.Contact.StateOrProvince,
                        SuburbOrTownship = registerReq.Contact.SuburbOrTownship,
                        PostalCode = registerReq.Contact.PostalCode
                    }
                }
            };

            await _dbContext.AddAsync(_applicationUser);
            await _dbContext.SaveChangesAsync();

            return _applicationUser.ApplicationUserCode;
        }

        public async Task<AuthenticateResp> Authenticate(string applicationUserCode)
        {
            using AccountLedgerContext _dbContext = new();

            ApplicationUser _applicationUser = await ApplicationUserDAL.GetApplicationUserByCode(_dbContext, applicationUserCode);

            if (_applicationUser is null)
            {
                throw new Exception("Invalid Application User Code");
            }

            _applicationUser.AccessToken = CreateAccessToken();
            _applicationUser.AccessTokenExpiryDate = DateTime.Now.AddMonths(2).Date;

            await _dbContext.SaveChangesAsync();

            return FillAuthenticateResp(_applicationUser.AccessToken, (DateTime)_applicationUser.AccessTokenExpiryDate);
        }

        private string CreateAccessToken()
        {
            return $"{Guid.NewGuid().ToString().Replace("-", "")}{Guid.NewGuid().ToString().Replace("-", "")}{Guid.NewGuid().ToString().Replace("-", "")}{Guid.NewGuid().ToString().Replace("-", "")}{Guid.NewGuid().ToString().Replace("-", "")}{Guid.NewGuid().ToString().Replace("-", "")}";
        }

        private AuthenticateResp FillAuthenticateResp(string accessToken, DateTime accessTokenExpiryDate)
        {
            return new AuthenticateResp()
            {
                AccessToken = accessToken,
                ExpiryDate = accessTokenExpiryDate
            };
        }
    }
}
