using AccountLedgerAPI.BLL.BLLClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Net;

namespace AccountLedgerAPI.Filters
{
    public class AuthenticateAccessToken : TypeFilterAttribute
    {
        public AuthenticateAccessToken() : base(typeof(AuthenticateAccessTokenImplementation)) { }
        private class AuthenticateAccessTokenImplementation : IAsyncAuthorizationFilter
        {
            private ApplicationUserBLL ApplicationUserBLL { get; set; }

            public AuthenticateAccessTokenImplementation()
            {
                ApplicationUserBLL = new();
            }

            public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
            {
                if (!context.HttpContext.Request.Headers.TryGetValue("AccessToken", out StringValues _accessToken))
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Result = new JsonResult(new ApiErrorResp("Access Token required"));
                }
                else
                {
                    if (!await ApplicationUserBLL.IsAccessTokenValid(_accessToken.FirstOrDefault()))
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.Result = new JsonResult(new ApiErrorResp("Request Forbidden"));
                    }
                }
            }
        }
    }
}
