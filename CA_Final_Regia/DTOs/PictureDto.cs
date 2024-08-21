using CA_Final_Regia.Properties.ActionFilters;
namespace CA_Final_Regia.DTOs
{
    public class PictureDto
    {
        [AllowedExtension([".jpg"])]
        public IFormFile Image { get; set; }
    }
}
