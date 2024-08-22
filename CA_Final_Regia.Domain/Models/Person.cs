using System.ComponentModel.DataAnnotations;
namespace CA_Final_Regia.Domain.Models
{
    public class Person
    {
        [Key]
        public Guid AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [MaxLength(11)]
        [MinLength(11)]
        public long PersonalId {  get; set; }
        [MaxLength(15)]
        [MinLength(5)]
        public string PhoneNumber { get; set; }
        public string Mail {get; set; }
        public byte[] FileData { get; set; }
        public Account Account { get; set; }
        public Location? Location { get; set; }
    }
}
