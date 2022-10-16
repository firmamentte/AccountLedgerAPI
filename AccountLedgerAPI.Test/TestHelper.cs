
namespace AccountLedgerAPI.Test
{
    public static class TestHelper
    {
        public static string DBConnection
        {
            get
            {
                return "Server=.\\SQLEXPRESS;Database=AccountLedger;Integrated Security=True;MultipleActiveResultSets=True";
            }
        }
    }
}
