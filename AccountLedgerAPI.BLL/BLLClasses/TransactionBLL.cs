

using AccountLedgerAPI.BLL.DataContract;
using AccountLedgerAPI.Data;
using AccountLedgerAPI.Data.DALClasses;
using AccountLedgerAPI.Data.Entities;

namespace AccountLedgerAPI.BLL.BLLClasses
{
    public class TransactionBLL
    {
        private readonly ApplicationUserAccountDAL ApplicationUserAccountDAL;
        private readonly TransactionDAL TransactionDAL;
        private readonly TransactionTypeDAL TransactionTypeDAL;

        public TransactionBLL()
        {
            ApplicationUserAccountDAL = new();
            TransactionDAL = new();
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
                DeletionDate = FirmamentUtilities.Utilities.DateHelper.DefaultDate
            };

            _applicationUserAccount.AccountBalance += createTransactionReq.TransactionAmount;

            await _dbContext.AddAsync(_transaction);
            await _dbContext.SaveChangesAsync();

            return FillTransactionResp(_transaction);
        }

        public async Task DeleteTransaction(DeleteTransactionReq deleteTransactionReq)
        {
            using AccountLedgerContext _dbContext = new();

            Transaction _transaction = await TransactionDAL.GetTransactionById(_dbContext, deleteTransactionReq.TransactionId);

            if (_transaction is null)
            {
                throw new Exception("Invalid Transaction Id");
            }

            _transaction.DeletionDate = DateTime.Now;

            if (_transaction.TransactionTypeName == FirmamentUtilities.Utilities.GetEnumDescription(AccountLedgerAPIEnum.TransactionType.credit))
            {
                _transaction.ApplicationUserAccount.AccountBalance += -_transaction.TransactionAmount;
            }
            else
            {
                _transaction.ApplicationUserAccount.AccountBalance += Math.Abs(_transaction.TransactionAmount);
            }

            await _dbContext.SaveChangesAsync();
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
