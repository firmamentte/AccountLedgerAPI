using System;
using System.Collections.Generic;

namespace AccountLedgerAPI.Data.Entities
{
    public partial class PersonalDetail
    {
        public PersonalDetail()
        {
            ApplicationUsers = new HashSet<ApplicationUser>();
        }

        public Guid PersonalDetailId { get; set; }
        public Guid ContactId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime Dob { get; set; }

        public virtual Contact Contact { get; set; } = null!;
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
