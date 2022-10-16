
using AccountLedgerAPI.BLL.BLLClasses;
using AccountLedgerAPI.BLL.DataContract;
using static AccountLedgerAPI.Data.StaticClass;

namespace AccountLedgerAPI.Test
{
    public class ApplicationUserBLLUnitTest
    {
        private readonly ApplicationUserBLL ApplicationUserBLL;

        public ApplicationUserBLLUnitTest()
        {
            DatabaseHelper.ConnectionString ??= TestHelper.DBConnection;
            ApplicationUserBLL = new();
        }

        [Fact]
        public async Task RegisterAndAuthenticateProcess()
        {
            string _applicationUserCode = await ApplicationUserBLL.Register(new RegisterReq()
            {
                FirstName = "Bona",
                LastName = "Shezi",
                DOB = new DateTime(1984, 04, 24),
                Contact = new ContactReq()
                {
                    AddressLine1 = "AddressLine1",
                    AddressLine2 = "AddressLine2",
                    CityOrTown = "CityOrTown",
                    EmailAddress = "htr@gmail.com",
                    MobileNumber = "0735886554",
                    PostalCode = "4001",
                    StateOrProvince = "Kwa-Zulu Natal",
                    SuburbOrTownship = "Westridge"
                }
            });

            AuthenticateResp _authenticateResp = await ApplicationUserBLL.Authenticate(_applicationUserCode);

            bool _result = await ApplicationUserBLL.IsAccessTokenValid(_authenticateResp.AccessToken);

            Assert.True(_result == true);
        }
    }
}