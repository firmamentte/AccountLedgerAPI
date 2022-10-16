namespace AccountLedgerAPI.BLL.DataContract
{
    public class RegisterReq
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DOB { get; set; } = DateTime.MinValue;
        public ContactReq Contact { get; set; } = new();
    }
}