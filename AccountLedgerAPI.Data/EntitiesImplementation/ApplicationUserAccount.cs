

namespace AccountLedgerAPI.Data.Entities
{
    public partial class ApplicationUserAccount
    {
        public virtual PersonalDetail PersonalDetail
        {
            get
            {
                return ApplicationUser.PersonalDetail;
            }
        }
    }
}
