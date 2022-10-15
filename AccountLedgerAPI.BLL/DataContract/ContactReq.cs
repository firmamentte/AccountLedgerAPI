namespace AccountLedgerAPI.BLL.DataContract
{
    public class ContactReq
    {
        public string MobileNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string StateOrProvince { get; set; } = string.Empty;
        public string CityOrTown { get; set; } = string.Empty;
        public string SuburbOrTownship { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
    }
}
