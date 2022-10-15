using AccountLedgerAPI.BLL.DataContract;
using AccountLedgerAPI.Data.Entities;

namespace AccountLedgerAPI.BLL.BLLClasses
{
    public class ApplicationUserBLL
    {
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
    }
}
