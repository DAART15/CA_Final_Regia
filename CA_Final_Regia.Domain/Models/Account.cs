namespace CA_Final_Regia.Domain.Models
{
    public class Account
    {
        public Guid AccountId { get; set; } = Guid.NewGuid();
        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public string Role { get; set; }
        public Person? Person { get; set; }
    }
}
