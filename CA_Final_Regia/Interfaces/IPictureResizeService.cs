using CA_Final_Regia.DTOs;
using System.Drawing;

namespace CA_Final_Regia.Interfaces
{
    public interface IPictureResizeService
    {
        Task<byte[]> ResizePictureAsync(PictureDto file);
    }
}
