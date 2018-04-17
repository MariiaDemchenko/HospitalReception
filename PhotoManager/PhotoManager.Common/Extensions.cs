using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PhotoManager.Common
{
    public class Extensions
    {
        public static byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        public static byte[] ResizeImage(Stream streamToResize, int width)
        {
            byte[] resizedImage;
            using (var orginalImage = Image.FromStream(streamToResize))
            {
                ImageFormat orginalImageFormat = orginalImage.RawFormat;
                int orginalImageWidth = orginalImage.Width;
                int orginalImageHeight = orginalImage.Height;

                int resizedImageHeight = Convert.ToInt32(width * orginalImageHeight / orginalImageWidth);
                using (var bitmapResized = new Bitmap(orginalImage, width, resizedImageHeight))
                {
                    using (var streamResized = new MemoryStream())
                    {
                        bitmapResized.Save(streamResized, orginalImageFormat);
                        resizedImage = streamResized.ToArray();
                    }
                }
            }
            return resizedImage;
        }
    }
}
