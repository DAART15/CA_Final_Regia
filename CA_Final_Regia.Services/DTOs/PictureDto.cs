using CA_Final_Regia.Services.ActionFilters;
using Microsoft.AspNetCore.Http;
namespace CA_Final_Regia.Services.DTOs
{
    public class PictureDto
    {
        [AllowedExtension([".jpg"])]
        public IFormFile Image { get; set; }
    }
}