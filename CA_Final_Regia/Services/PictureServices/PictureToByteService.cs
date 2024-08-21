using CA_Final_Regia.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
namespace CA_Final_Regia.Services.PictureServices
{
    public class PictureToByteService : IPictureToByteService
    {
        public async Task<byte[]> ConvertToByteArrayAsync(Image image)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using var memorystream = new MemoryStream();
                    image.Save(memorystream, ImageFormat.Jpeg);
                    return memorystream.ToArray();
                });
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
