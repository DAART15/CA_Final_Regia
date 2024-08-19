namespace CA_Final_Regia.Domain.Models
{
    public class Location
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNr { get; set; }
        public int ApartmentNr { get; set; }
        public Person Person { get; set; }
        public Person PersonalId { get; set; }
    }
}
