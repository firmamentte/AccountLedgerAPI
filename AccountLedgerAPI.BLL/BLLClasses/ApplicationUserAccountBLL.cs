﻿using AccountLedgerAPI.BLL.DataContract;
using AccountLedgerAPI.Data.DALClasses;
using AccountLedgerAPI.Data.Entities;
using System.Collections.Generic;

namespace AccountLedgerAPI.BLL.BLLClasses
{
    public class ApplicationUserAccountBLL
    {
        private readonly ApplicationUserDAL ApplicationUserDAL;
        private readonly ApplicationUserAccountDAL ApplicationUserAccountDAL;

        public ApplicationUserAccountBLL()
        {
            ApplicationUserDAL = new();
            ApplicationUserAccountDAL = new();
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
    }
}
