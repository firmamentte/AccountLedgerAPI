
namespace AccountLedgerAPI.BLL.DataContract
{
    public class AccountStatementResp
    {
        public string AccountName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ContactResp Contact { get; set; } = new();
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal AccountStatementBalance { get; set; }
        public List<TransactionResp> Transactions { get; set; } = new();
    }
}
