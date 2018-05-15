using Microsoft.ServiceModel.ImageViewer.Contracts.DataContracts;
using Microsoft.ServiceModel.ImageViewer.Contracts.MessageContracts;
using Microsoft.ServiceModel.ImageViewer.Contracts.ServiceContracts;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.ServiceModel;

namespace Microsoft.ServiceModel.ImageViewer
{
    public class ImageViewer : IImageViewer
    {
        public FilesData GetAllImages()
        {
            var info = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, Constants.ImageFolderPath));
            FileInfo[] files = info.GetFiles();

            var filesMetaData = new FilesData
            {
                ImageFiles = files.Select(f => new ImageInfo { Name = f.Name, Date = f.CreationTime, ImageSizeBytes = f.Length }).ToArray()
            };

            return filesMetaData;
        }

        public Stream DownloadImage(ImageInfo data)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, $"{Constants.ImageFolderPath}\\{data.Name}");
            try
            {
                if (data.Size == Constants.ImageSize.Original)
                {
                    return File.OpenRead(filePath);
                }

                var streamResized = new MemoryStream();

                using (var imageFile = File.OpenRead(filePath))
                using (var orginalImage = Image.FromStream(imageFile))
                {
                    ImageFormat orginalImageFormat = orginalImage.RawFormat;
                    int orginalImageWidth = orginalImage.Width;
                    int orginalImageHeight = orginalImage.Height;

                    int resizedImageHeight =
                        Convert.ToInt32(Constants.SmallPhotoSize * orginalImageHeight / orginalImageWidth);

                    using (var bitmapResized = new Bitmap(orginalImage, Constants.SmallPhotoSize, resizedImageHeight))
                    {
                        bitmapResized.Save(streamResized, orginalImageFormat);
                    }

                    streamResized.Position = 0;
                }

                return streamResized;
            }
            catch (FileNotFoundException)
            {
                var faultException = new ImageProcessingFault
                {
                    Action = "DownloadImage",
                    Message = "Requested image was not found"
                };
                throw new FaultException<ImageProcessingFault>(faultException, new FaultReason(faultException.Message));
            }
            catch (Exception exception)
            {
                var faultException = new ImageProcessingFault
                {
                    Action = "DownloadImage",
                    Message = exception.Message
                };

                throw new FaultException<ImageProcessingFault>(faultException, new FaultReason(exception.Message));
            }
        }

        public void UploadImage(ImageData imageData)
        {
            try
            {
                using (var imageStream = new MemoryStream())
                {
                    imageData.ImageStream.CopyTo(imageStream);
                    using (var image = new Bitmap(imageStream))
                    {
                        if (!image.RawFormat.Equals(ImageFormat.Jpeg))
                        {
                            var faultException = new ImageProcessingFault
                            {
                                Action = "UploadImage",
                                Message = "Wrong extension. Only .jpg extension is allowed"
                            };
                            throw new FaultException<ImageProcessingFault>(faultException, new FaultReason(faultException.Message));
                        }
                    }

                    imageStream.Position = 0;

                    using (Stream sourceStream = imageStream)
                    {
                        var filePath = Path.Combine($"{Environment.CurrentDirectory}\\{Constants.ImageFolderPath}",
                            imageData.ImageInfo.Name + ".jpg");
                        if (File.Exists(filePath))
                        {
                            var faultException = new ImageProcessingFault
                            {
                                Action = "UploadImage",
                                Message = "File with specified name already exists. File name must be unique"
                            };
                            throw new FaultException<ImageProcessingFault>(faultException,
                                new FaultReason(faultException.Message));
                        }

                        using (var targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write,
                            FileShare.None))
                        {
                            const int bufferLength = Constants.BufferLength;
                            byte[] buffer = new byte[bufferLength];
                            int count;
                            while ((count = sourceStream.Read(buffer, 0, bufferLength)) > 0)
                            {
                                targetStream.Write(buffer, 0, count);
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                var faultException = new ImageProcessingFault
                {
                    Action = "UploadImage",
                    Message = "Images folder was not found by the specified path"
                };
                throw new FaultException<ImageProcessingFault>(faultException, new FaultReason(faultException.Message));
            }
            catch (Exception exception)
            {
                if (exception is FaultException)
                {
                    throw;
                }

                var faultException = new ImageProcessingFault
                {
                    Action = "UploadImage",
                    Message = exception.Message
                };

                throw new FaultException<ImageProcessingFault>(faultException, new FaultReason(exception.Message));
            }
        }
    }
}