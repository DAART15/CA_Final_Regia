namespace CA_Final_Regia.DTOs
{
    public class PersonDto
    {
        public Guid AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PersonalId { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        public IFormFile Image { get; set; }
    }
}
