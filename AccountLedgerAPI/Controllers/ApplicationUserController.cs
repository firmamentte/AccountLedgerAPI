using AccountLedgerAPI.BLL.BLLClasses;
using AccountLedgerAPI.BLL.DataContract;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AccountLedgerAPI.Controllers
{
    [ApiController]
    [Route("api/ApplicationUser")]
    public class ApplicationUserController : ControllerBase
    {
        private readonly ApplicationUserBLL ApplicationUserBLL;

        public ApplicationUserController()
        {
            ApplicationUserBLL = new();
        }

        [Route("V1/Register")]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] RegisterReq  registerReq)
        {
            #region RequestValidation

            ModelState.Clear();

            #endregion

            Response.Headers.Add("ApplicationUserCode", await ApplicationUserBLL.Register(registerReq));

            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}