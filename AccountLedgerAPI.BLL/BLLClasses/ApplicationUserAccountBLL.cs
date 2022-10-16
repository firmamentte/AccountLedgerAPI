using AccountLedgerAPI.BLL.DataContract;
using AccountLedgerAPI.Data;
using AccountLedgerAPI.Data.DALClasses;
using AccountLedgerAPI.Data.Entities;

namespace AccountLedgerAPI.BLL.BLLClasses
{
    public class ApplicationUserAccountBLL : SharedBLL
    {
        private readonly ApplicationUserDAL ApplicationUserDAL;
        private readonly ApplicationUserAccountDAL ApplicationUserAccountDAL;
        private readonly TransactionDAL TransactionDAL;

        public ApplicationUserAccountBLL()
        {
            ApplicationUserDAL = new();
            ApplicationUserAccountDAL = new();
            TransactionDAL = new();
        }

        public async Task<ApplicationUserAccountResp> CreateApplicationUserAccount(string accessToken, CreateApplicationUserAccountReq createApplicationUserAccountReq)
        {
            using AccountLedgerContext _dbContext = new();

            ApplicationUserAccount _applicationUserAccount = new()
            {
                AccountName = createApplicationUserAccountReq.AccountName,
                AccountNumber = await CreateAccountNumber(_dbContext),
                ApplicationUser = await ApplicationUserDAL.GetApplicationUserByAccessToken(_dbContext, accessToken),
                CreationDate = DateTime.Now.Date,
            };

            await _dbContext.AddAsync(_applicationUserAccount);
            await _dbContext.SaveChangesAsync();

            return FillApplicationUserAccountResp(_applicationUserAccount);
        }

        public async Task<List<ApplicationUserAccountResp>> GetApplicationUserAccountsByCriteria(string? accountNumber, string? accountName)
        {
            using AccountLedgerContext _dbContext = new();

            List<ApplicationUserAccountResp> applicationUserAccountResps = new();

            foreach (var item in await ApplicationUserAccountDAL.GetApplicationUserAccountsByCriteria(_dbContext, accountNumber, accountName))
            {
                applicationUserAccountResps.Add(FillApplicationUserAccountResp(item));
            }

            return applicationUserAccountResps;
        }

        public async Task<AccountStatementResp> GetAccountStatement(string accountNumber, DateTime fromDate, DateTime toDate)
        {
            using AccountLedgerContext _dbContext = new();

            ApplicationUserAccount _applicationUserAccount = await ApplicationUserAccountDAL.GetApplicationUserAccountByAccountNumber(_dbContext, accountNumber);

            if (_applicationUserAccount is null)
            {
                throw new Exception("Invalid Account Number");
            }

            return FillAccountStatementResp(
                 _applicationUserAccount,
                 await TransactionDAL.GetTransactionsByDateRange(_dbContext, accountNumber, fromDate, toDate),
                 fromDate,
                 toDate);
        }

        private async Task<string> CreateAccountNumber(AccountLedgerContext _dbContext)
        {
            Random _random = new();

            string _accountNumber = _random.Next(10000, 2000000000).ToString();

            while (await ApplicationUserAccountDAL.IsAccountNumberExisting(_dbContext, _accountNumber))
            {
                _accountNumber = _random.Next(10000, 2000000000).ToString();
            }

            return _accountNumber;
        }

        private ApplicationUserAccountResp FillApplicationUserAccountResp(ApplicationUserAccount applicationUserAccount)
        {
            return new ApplicationUserAccountResp()
            {
                AccountName = applicationUserAccount.AccountName,
                AccountNumber = applicationUserAccount.AccountNumber,
                AccountBalance = applicationUserAccount.AccountBalance,
                CreationDate = applicationUserAccount.CreationDate
            };
        }

        private AccountStatementResp FillAccountStatementResp(
        ApplicationUserAccount applicationUserAccount, List<Transaction> transactions, DateTime fromDate, DateTime toDate)
        {
            transactions ??= new();

            PersonalDetail _personalDetail = applicationUserAccount.PersonalDetail;

            return new AccountStatementResp()
            {
                AccountName = applicationUserAccount.AccountName,
                AccountNumber = applicationUserAccount.AccountNumber,
                Contact = FillContactResp(_personalDetail.Contact),
                FirstName = _personalDetail.FirstName,
                LastName = _personalDetail.LastName,
                Transactions = FillTransactionResps(transactions),
                AccountStatementBalance = CalculateAccountStatementBalance(transactions),
                FromDate = fromDate,
                ToDate = toDate,
            };
        }

        private ContactResp FillContactResp(Contact contact)
        {
            return new ContactResp()
            {
                AddressLine1 = contact.AddressLine1,
                AddressLine2 = contact.AddressLine2,
                CityOrTown = contact.CityOrTown,
                EmailAddress = contact.EmailAddress,
                MobileNumber = contact.MobileNumber,
                PostalCode = contact.PostalCode,
                StateOrProvince = contact.StateOrProvince,
                SuburbOrTownship = contact.SuburbOrTownship
            };
        }

        private decimal CalculateAccountStatementBalance(List<Transaction> transactions)
        {
            decimal _totalCredit = transactions.Where(transaction => transaction.TransactionTypeName == FirmamentUtilities.Utilities.GetEnumDescription(AccountLedgerAPIEnum.TransactionType.credit)).
                                                Sum(transaction => transaction.TransactionAmount),
                    _totalDebit = transactions.Where(transaction => transaction.TransactionTypeName == FirmamentUtilities.Utilities.GetEnumDescription(AccountLedgerAPIEnum.TransactionType.debit)).
                                                Sum(transaction => transaction.TransactionAmount);

            return _totalCredit + _totalDebit;
        }
    }
}
