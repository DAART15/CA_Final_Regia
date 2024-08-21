using System.Drawing;

namespace CA_Final_Regia.Interfaces
{
    public interface IPictureToByteService
    {
        Task<byte[]> ConvertToByteArrayAsync(Image image);
    }
}
