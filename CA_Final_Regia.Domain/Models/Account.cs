namespace CA_Final_Regia.Domain.Models
{
    internal class Account
    {
        public Guid AccountId { get; set; }
        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public string Role { get; set; }
    }
}
