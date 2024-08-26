using CA_Final_Regia.Services.ActionFilters;
using Microsoft.AspNetCore.Http;
namespace CA_Final_Regia.Services.DTOs
{
    public class PersonPostDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PersonalId { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        [AllowedExtension([".jpg"])]
        public IFormFile Image { get; set; }
    }
}
