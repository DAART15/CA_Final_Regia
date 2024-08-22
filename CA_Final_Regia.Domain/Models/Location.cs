using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CA_Final_Regia.Domain.Models
{
    public class Location
    {
        [Key]
        [ForeignKey("Person")]
        public Guid AccountId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNr { get; set; }
        public string ApartmentNr { get; set; }
        public Person Person { get; set; }
    }
}
