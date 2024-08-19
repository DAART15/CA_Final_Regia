using System.ComponentModel.DataAnnotations;
namespace CA_Final_Regia.Domain.Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [MaxLength(13)]
        [MinLength(13)]
        public long PersonalId {  get; set; }
        [MaxLength(15)]
        [MinLength(5)]
        public string PhoneNumber { get; set; }
        public string Mail {get; set; }
    }
}
