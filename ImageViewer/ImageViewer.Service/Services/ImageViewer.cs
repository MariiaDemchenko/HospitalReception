using Microsoft.ServiceModel.ImageViewer.Contracts.DataContracts;
using Microsoft.ServiceModel.ImageViewer.Contracts.MessageContracts;
using Microsoft.ServiceModel.ImageViewer.Contracts.ServiceContracts;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.ServiceModel;

namespace Microsoft.ServiceModel.ImageViewer.Services
{
    public class ImageViewer : IImageViewer
    {
        public FilesData GetAllImages()
        {
            var info = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "..\\..\\Images"));
            FileInfo[] files = info.GetFiles();

            var filesMetaData = new FilesData
            {
                ImageFiles = files.Select(f => new ImageInfo { Name = f.Name, Date = f.CreationTime, ImageSizeBytes = f.Length }).ToArray()
            };

            return filesMetaData;
        }

        public Stream DownloadImage(ImageInfo data)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, $"..\\..\\Images\\{data.Name}");
            try
            {
                var imageFile = File.OpenRead(filePath);

                if (data.Size == Constants.ImageSize.Original)
                {
                    return imageFile;
                }

                var orginalImage = Image.FromStream(imageFile);
                ImageFormat orginalImageFormat = orginalImage.RawFormat;
                int orginalImageWidth = orginalImage.Width;
                int orginalImageHeight = orginalImage.Height;

                int resizedImageHeight = Convert.ToInt32(250 * orginalImageHeight / orginalImageWidth);
                var bitmapResized = new Bitmap(orginalImage, 250, resizedImageHeight);
                var streamResized = new MemoryStream();
                bitmapResized.Save(streamResized, orginalImageFormat);
                streamResized.Position = 0;
                return streamResized;
            }
            catch (Exception ex)
            {
                var faultException = new ImageProcessingFault
                {
                    Action = "DownloadImage",
                    Message = ex.Message
                };

                throw new FaultException<ImageProcessingFault>(faultException, new FaultReason("Error uploading image"));
            }
        }

        public void UploadImage(ImageData request)
        {
            try
            {
                FileStream targetStream = null;
                Stream sourceStream = request.ImageStream;

                string filePath = Path.Combine($"{Environment.CurrentDirectory}\\..\\..\\Images",
                    request.ImageInfo.Name);

                using (targetStream = new FileStream(filePath, FileMode.Create,
                    FileAccess.Write, FileShare.None))
                {
                    const int bufferLength = Constants.BufferLength;
                    byte[] buffer = new byte[bufferLength];
                    int count;
                    while ((count = sourceStream.Read(buffer, 0, bufferLength)) > 0)
                    {
                        targetStream.Write(buffer, 0, count);
                    }

                    targetStream.Close();
                    sourceStream.Close();
                }
            }
            catch (Exception exception)
            {
                var faultException = new ImageProcessingFault
                {
                    Action = "UploadImage",
                    Message = exception.Message
                };

                throw new FaultException<ImageProcessingFault>(faultException, new FaultReason("Error uploading image"));
            }
        }
    }
}