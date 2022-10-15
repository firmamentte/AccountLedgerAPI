using AccountLedgerAPI.BLL.BLLClasses;
using AccountLedgerAPI.BLL.DataContract;
using AccountLedgerAPI.Controllers.ControllerHelpers;
using AccountLedgerAPI.Data;
using AccountLedgerAPI.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountLedgerAPI.Controllers
{
    [ApiController]
    [AuthenticateAccessToken]
    [Route("api/Transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionBLL TransactionBLL;

        public TransactionController()
        {
            TransactionBLL = new();
        }

        [Route("V1/CreateTransaction")]
        [HttpPost]
        public async Task<ActionResult> CreateTransaction([FromBody] CreateTransactionReq createTransactionReq)
        {
            #region RequestValidation

            ModelState.Clear();

            if (string.IsNullOrWhiteSpace(createTransactionReq.AccountNumber))
            {
                ModelState.AddModelError("AccountNumber", "Account Number required");
            }

            if (string.IsNullOrWhiteSpace(createTransactionReq.TransactionName))
            {
                ModelState.AddModelError("TransactionName", "Transaction Name required");
            }

            if (string.IsNullOrWhiteSpace(createTransactionReq.TransactionTypeName))
            {
                ModelState.AddModelError("TransactionTypeName", "Transaction Type Name required");
            }
            else
            {
                createTransactionReq.TransactionTypeName = createTransactionReq.TransactionTypeName.ToLower();

                if (createTransactionReq.TransactionTypeName != FirmamentUtilities.Utilities.GetEnumDescription(AccountLedgerAPIEnum.TransactionType.credit) &&
                    createTransactionReq.TransactionTypeName != FirmamentUtilities.Utilities.GetEnumDescription(AccountLedgerAPIEnum.TransactionType.debit))
                {
                    ModelState.AddModelError("TransactionTypeName", "Invalid Transaction Type Name");
                }
            }

            if (createTransactionReq.TransactionDate == DateTime.MinValue)
            {
                ModelState.AddModelError("TransactionDate", "Invalid Transaction Date");
            }

            if (createTransactionReq.TransactionAmount == decimal.Zero)
            {
                ModelState.AddModelError("TransactionAmount", "Transaction Amount cannot be zero");
            }
            else
            {
                if (createTransactionReq.TransactionTypeName == FirmamentUtilities.Utilities.GetEnumDescription(AccountLedgerAPIEnum.TransactionType.credit))
                {
                    if (createTransactionReq.TransactionAmount < 0)
                    {
                        ModelState.AddModelError("TransactionAmount", "Transaction Amount must be greater than zero");
                    }
                }

                if (createTransactionReq.TransactionTypeName == FirmamentUtilities.Utilities.GetEnumDescription(AccountLedgerAPIEnum.TransactionType.debit))
                {
                    if (createTransactionReq.TransactionAmount > 0)
                    {
                        ModelState.AddModelError("TransactionAmount", "Transaction Amount must be less than zero");
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResp(ModelState));
            }

            #endregion

            return Ok(await TransactionBLL.CreateTransaction(createTransactionReq));
        }
    }
}
