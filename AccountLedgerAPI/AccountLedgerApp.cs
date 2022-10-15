using AccountLedgerAPI.BLL;

namespace AccountLedgerAPI
{
    public static class AccountLedgerApp
    {
        public static void InitializeApplicationSettings(this WebApplication app)
        {
            StaticClass.InitializeApplicationSettings(app.Configuration);
        }
    }
}
