using CA_Final_Regia.Services.DTOs;
namespace CA_Final_Regia.Services.Interfaces
{
    public interface IPictureResizeService
    {
        Task<byte[]> ResizePictureAsync(PictureDto file);
    }
}
