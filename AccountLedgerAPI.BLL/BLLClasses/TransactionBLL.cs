

using AccountLedgerAPI.BLL.DataContract;
using AccountLedgerAPI.Data.DALClasses;
using AccountLedgerAPI.Data.Entities;

namespace AccountLedgerAPI.BLL.BLLClasses
{
    public class TransactionBLL
    {
        private readonly ApplicationUserAccountDAL ApplicationUserAccountDAL;
        private readonly TransactionTypeDAL TransactionTypeDAL;

        public TransactionBLL()
        {
            ApplicationUserAccountDAL = new();
            TransactionTypeDAL = new();
        }

        public async Task<TransactionResp> CreateTransaction(CreateTransactionReq createTransactionReq)
        {
            using AccountLedgerContext _dbContext = new();

            ApplicationUserAccount _applicationUserAccount =
            await ApplicationUserAccountDAL.GetApplicationUserAccountByAccountNumber(_dbContext, createTransactionReq.AccountNumber);

            if (_applicationUserAccount is null)
            {
                throw new Exception("Invalid Account Number");
            }

            Transaction _transaction = new()
            {
                ApplicationUserAccount = _applicationUserAccount,
                TransactionAmount = createTransactionReq.TransactionAmount,
                TransactionDate = createTransactionReq.TransactionDate.Date,
                TransactionTicks = createTransactionReq.TransactionDate.Ticks,
                TransactionName = createTransactionReq.TransactionName,
                TransactionType = await TransactionTypeDAL.GetTransactionTypeByTypeName(_dbContext, createTransactionReq.TransactionTypeName),
            };

            _applicationUserAccount.AccountBalance += createTransactionReq.TransactionAmount;

            await _dbContext.AddAsync(_transaction);
            await _dbContext.SaveChangesAsync();

            return FillTransactionResp(_transaction);
        }

        private TransactionResp FillTransactionResp(Transaction transaction)
        {
            return new TransactionResp()
            {
                AccountBalance = transaction.AccountBalance,
                TransactionDate = transaction.TransactionDate,
                TransactionAmount = transaction.TransactionAmount,
                TransactionName = transaction.TransactionName,
                TransactionType = transaction.TransactionTypeName,
                TransactionId = transaction.TransactionId
            };
        }


    }
}
