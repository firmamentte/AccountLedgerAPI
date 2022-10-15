
namespace AccountLedgerAPI.BLL.DataContract
{
    public class TransactionResp
    {
        public Guid TransactionId { get; set; }
        public decimal AccountBalance { get; set; } = decimal.Zero;
        public string TransactionName { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public decimal TransactionAmount { get; set; } = decimal.Zero;
        public DateTime TransactionDate { get; set; }
    }
}
