
namespace AccountLedgerAPI.BLL.DataContract
{
    public class TransactionPaginationResp
    {
        public PaginationMeta Meta { get; set; } = new();
        public List<TransactionResp> Transactions { get; set; } = new();
    }
}
