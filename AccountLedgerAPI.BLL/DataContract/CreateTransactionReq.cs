
namespace AccountLedgerAPI.BLL.DataContract
{
    public class CreateTransactionReq
    {
        public string AccountNumber { get; set; } = string.Empty;
        public string TransactionName { get; set; } = string.Empty;
        public string TransactionTypeName { get; set; } = string.Empty;
        public decimal TransactionAmount { get; set; } = decimal.Zero;
        public DateTime TransactionDate { get; set; }
    }
}