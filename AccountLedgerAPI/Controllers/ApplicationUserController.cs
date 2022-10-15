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
        public async Task<ActionResult> Register([FromBody] RegisterReq registerReq)
        {
            #region RequestValidation

            ModelState.Clear();

            if (string.IsNullOrWhiteSpace(registerReq.FirstName))
            {
                ModelState.AddModelError("FirstName", "First Name required");
            }

            if (string.IsNullOrWhiteSpace(registerReq.LastName))
            {
                ModelState.AddModelError("LastName", "Last Name required");
            }

            if (registerReq.DOB == DateTime.MinValue)
            {
                ModelState.AddModelError("InvalidDOB", "Invalid DOB");
            }
            else
            {
                if (registerReq.DOB > DateTime.Now)
                {
                    ModelState.AddModelError("InvalidDOB", "DOB can not be greater than current date");
                }
            }

            if (string.IsNullOrWhiteSpace(registerReq.Contact.MobileNumber))
            {
                ModelState.AddModelError("MobileNumber", "Mobile Number required");
            }

            if (string.IsNullOrWhiteSpace(registerReq.Contact.EmailAddress))
            {
                ModelState.AddModelError("EmailAddress", "Email Address required");
            }
            else
            {
                if (!FirmamentUtilities.Utilities.EmailHelper.IsEmailAddress(registerReq.Contact.EmailAddress))
                {
                    ModelState.AddModelError("InvalidEmailAddress", "Invalid Email Address");
                }
            }

            if (string.IsNullOrWhiteSpace(registerReq.Contact.AddressLine1))
            {
                ModelState.AddModelError("AddressLine1", "Address Line 1 required");
            }

            if (string.IsNullOrWhiteSpace(registerReq.Contact.StateOrProvince))
            {
                ModelState.AddModelError("StateOrProvince", "State or Province required");
            }

            if (string.IsNullOrWhiteSpace(registerReq.Contact.CityOrTown))
            {
                ModelState.AddModelError("CityOrTown", "City or Town required");
            }

            if (string.IsNullOrWhiteSpace(registerReq.Contact.SuburbOrTownship))
            {
                ModelState.AddModelError("SuburbOrTownship", "Suburb or Township required");
            }

            if (string.IsNullOrWhiteSpace(registerReq.Contact.PostalCode))
            {
                ModelState.AddModelError("PostalCode", "Postal Code required");
            }
            else
            {
                try
                {
                    int.Parse(registerReq.Contact.PostalCode);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("InvalidPostalCode", "Postal Code must be an integer value");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResp(ModelState));
            }

            #endregion

            Response.Headers.Add("ApplicationUserCode", await ApplicationUserBLL.Register(registerReq));

            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}