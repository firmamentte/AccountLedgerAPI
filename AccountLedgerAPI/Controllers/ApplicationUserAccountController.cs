using AccountLedgerAPI.BLL.BLLClasses;
using AccountLedgerAPI.BLL.DataContract;
using AccountLedgerAPI.Controllers.ControllerHelpers;
using AccountLedgerAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AccountLedgerAPI.Controllers
{
    [ApiController]
    [AuthenticateAccessToken]
    [Route("api/ApplicationUserAccount")]
    public class ApplicationUserAccountController : ControllerBase
    {
        private readonly SharedHelper SharedHelper;
        private readonly ApplicationUserAccountBLL ApplicationUserAccountBLL;

        public ApplicationUserAccountController()
        {
            ApplicationUserAccountBLL = new();
            SharedHelper = new();
        }

        [Route("V1/CreateApplicationUserAccount")]
        [HttpPost]
        public async Task<ActionResult> CreateApplicationUserAccount(CreateApplicationUserAccountReq createApplicationUserAccountReq)
        {
            #region RequestValidation

            ModelState.Clear();

            if (string.IsNullOrWhiteSpace(createApplicationUserAccountReq.AccountName))
            {
                ModelState.AddModelError("AccountName", "Account Name required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResp(ModelState));
            }

            #endregion

            return Ok(await ApplicationUserAccountBLL.CreateApplicationUserAccount(SharedHelper.GetHeaderAccessToken(Request), createApplicationUserAccountReq));
        }
    }
}
