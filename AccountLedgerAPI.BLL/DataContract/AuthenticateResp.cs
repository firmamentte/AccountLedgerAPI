
namespace AccountLedgerAPI.BLL.DataContract
{
    public class AuthenticateResp
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
    }
}
