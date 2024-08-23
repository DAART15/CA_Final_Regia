using CA_Final_Regia.DTOs;
using CA_Final_Regia.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
namespace CA_Final_Regia.Services.PictureServices
{
    public class PictureResizeService : IPictureResizeService 
    {
        public async Task<byte[]> ResizePictureAsync(PictureDto file)
        {
            try
            {
                using var streamForThumbnail = new MemoryStream();
                await file.Image.CopyToAsync(streamForThumbnail);
                streamForThumbnail.Position = 0;
                using Bitmap bitmap = new Bitmap(streamForThumbnail);
                Size thumbnailSize = GetThumbnailSize(bitmap);
                using var thumbnail = bitmap.GetThumbnailImage(thumbnailSize.Width, thumbnailSize.Height, null, IntPtr.Zero);
                using var streamForBytes = new MemoryStream();
                thumbnail.Save(streamForBytes,ImageFormat.Jpeg);
                return streamForBytes.ToArray();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        private static Size GetThumbnailSize(Bitmap original)
        {
            try
            {
                const int maxPixels = 200;
                int originalWidth = original.Width;
                int originalHeight = original.Height;
                double factor;
                if (originalWidth > originalHeight)
                {
                    factor = (double)maxPixels / originalWidth;
                }
                else
                {
                    factor = (double)maxPixels / originalHeight;
                }
                return new Size((int)(originalWidth * factor), (int)(originalHeight * factor));
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
