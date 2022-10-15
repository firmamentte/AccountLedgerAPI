
namespace AccountLedgerAPI.BLL.DataContract
{
    public class ApplicationUserAccountResp
    {
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public decimal AccountBalance { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
