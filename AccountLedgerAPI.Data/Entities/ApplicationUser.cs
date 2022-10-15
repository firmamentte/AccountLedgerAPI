using System;
using System.Collections.Generic;

namespace AccountLedgerAPI.Data.Entities
{
    public partial class ApplicationUser
    {
        public ApplicationUser()
        {
            ApplicationUserAccounts = new HashSet<ApplicationUserAccount>();
        }

        public Guid ApplicationUserId { get; set; }
        public Guid PersonalDetailId { get; set; }
        public string ApplicationUserCode { get; set; } = null!;
        public string? AccessToken { get; set; }
        public DateTime? AccessTokenExpiryDate { get; set; }

        public virtual PersonalDetail PersonalDetail { get; set; } = null!;
        public virtual ICollection<ApplicationUserAccount> ApplicationUserAccounts { get; set; }
    }
}
