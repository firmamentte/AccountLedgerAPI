
namespace AccountLedgerAPI.BLL.DataContract
{
    public class PaginationMeta
    {
        public string OrderedBy { get; set; } = string.Empty;
        public int Taken { get; set; }
        public int NextSkip { get; set; }
    }
}
