using System;
using System.Collections.Generic;

namespace AccountLedgerAPI.Data.Entities
{
    public partial class Contact
    {
        public Contact()
        {
            PersonalDetails = new HashSet<PersonalDetail>();
        }

        public Guid ContactId { get; set; }
        public string MobileNumber { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string AddressLine1 { get; set; } = null!;
        public string? AddressLine2 { get; set; }
        public string StateOrProvince { get; set; } = null!;
        public string CityOrTown { get; set; } = null!;
        public string SuburbOrTownship { get; set; } = null!;
        public string PostalCode { get; set; } = null!;

        public virtual ICollection<PersonalDetail> PersonalDetails { get; set; }
    }
}
