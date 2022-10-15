using Microsoft.Extensions.Configuration;
using static AccountLedgerAPI.Data.StaticClass;

namespace AccountLedgerAPI.BLL
{
    public class StaticClass
    {
        public static void InitializeApplicationSettings(IConfiguration configuration)
        {
            DatabaseHelper.ConnectionString ??= configuration["ConnectionStrings:DatabasePath"];
        }
    }
}