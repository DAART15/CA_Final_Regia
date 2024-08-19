namespace CA_Final_Regia.Domain.Models
{
    public class Picture
    {
        public byte[] FileData { get; set; }
        public Person Person { get; set; }
        public Person PersonalId { get; set; }
    }
}
