using Microsoft.Extensions.Primitives;

namespace AccountLedgerAPI.Controllers.ControllerHelpers
{
    public class SharedHelper
    {
        public string GetHeaderAccessToken(HttpRequest request)
        {
            if (request.Headers.TryGetValue("AccessToken", out StringValues _username))
                return _username.FirstOrDefault();
            else
                return null;
        }
    }
}
