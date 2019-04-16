using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PhotoManager.BLL.Repositories;
using PhotoManager.Models;
using PhotoManager.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoManager.Controllers
{
    [Route("api/[controller]")]
    public class PhotosController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPhotoRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly string _imagesPath;
        private readonly string _thumbsPath;

        public object ImageFormat { get; private set; }

        public PhotosController(IMapper mapper, IPhotoRepository repository, IConfiguration configuration)
        {
            _mapper = mapper;
            _repository = repository;            
            _configuration = configuration;
        }

        [HttpGet("[action]")]
        public IEnumerable<PhotoViewModel> GetAllPhotos(int pageIndex, int pageSize)
        {
            var photos = _repository.GetAll(pageIndex * pageSize, pageSize).ToList();
            return photos.Select(photo => _mapper.Map<PhotoViewModel>(photo));
        }

        [HttpPost("[action]")]
        [Authorize]
        [DisableRequestSizeLimit]
        public async Task<ActionResult> AddPhotos([FromBody]IEnumerable<PhotoViewModel> photos)
        {
            foreach (var photo in photos)
            {
                try
                {
                    var imageToReplace = FromDataURLToByteArray(photo.PhotoUrl);
                    if (photo.PhotoId == null)
                    {
                        photo.ServerName = Guid.NewGuid().ToString();
                        photo.Format = "jpg";
                    }

                    if (imageToReplace != null)
                    {
                        if (!IsJpgExtension(imageToReplace))
                        {
                            return BadRequest($"{photo.PhotoName}: Wrong image format. Only jpg allowed.");
                        }

                        await System.IO.File.WriteAllBytesAsync($"{GetImagesPath()}/{photo.ServerName}.jpg", imageToReplace);
                        await System.IO.File.WriteAllBytesAsync($"{GetThumbsPath()}/{photo.ServerName}.jpg", GetThumb(imageToReplace));
                    }

                    _repository.Create(_mapper.Map<Photo>(photo), photo.AlbumId);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error processing photo: " + ex.Message);
                }
            }
            return Ok("Updated successfully");
        }

        [HttpPut("[action]")]
        [Authorize]
        [DisableRequestSizeLimit]
        public async Task<ActionResult> EditPhotos([FromBody]IEnumerable<PhotoViewModel> photos)
        {
            foreach (var photo in photos)
            {
                try
                {
                    var imageToReplace = FromDataURLToByteArray(photo.PhotoUrl);
                    if (photo.PhotoId == null)
                    {
                        photo.ServerName = Guid.NewGuid().ToString();
                        photo.Format = "jpg";
                    }

                    if (imageToReplace != null)
                    {
                        if (!IsJpgExtension(imageToReplace))
                        {
                            return BadRequest($"{photo.PhotoName}: Wrong image format. Only jpg allowed.");
                        }

                        await System.IO.File.WriteAllBytesAsync($"{GetImagesPath()}/{photo.ServerName}.jpg", imageToReplace);
                        await System.IO.File.WriteAllBytesAsync($"{GetThumbsPath()}/{photo.ServerName}.jpg", GetThumb(imageToReplace));
                    }

                    _repository.Update(photo.PhotoId, _mapper.Map<Photo>(photo));
                }
                catch (Exception ex)
                {
                    return BadRequest("Error processing photo: " + ex.Message);
                }
            }
            return Ok("Updated successfully");
        }

        [HttpDelete("[action]")]
        [Authorize]
        public ActionResult RemovePhoto([FromBody]PhotoViewModel photo)
        {
            try
            {
                _repository.Remove(_mapper.Map<Photo>(photo), photo.AlbumId);
            }
            catch (Exception ex)
            {
                return BadRequest("Error processing photos: " + ex.Message);
            }
            return Ok();
        }

        #region utils

        private static byte[] ResizeImage(Stream streamToResize, int height)
        {
            byte[] resizedImage;
            using (var orginalImage = System.Drawing.Image.FromStream(streamToResize))
            {
                System.Drawing.Imaging.ImageFormat orginalImageFormat = orginalImage.RawFormat;
                int orginalImageWidth = orginalImage.Width;
                int orginalImageHeight = orginalImage.Height;

                int resizedImageWidth = Convert.ToInt32(height * orginalImageWidth / orginalImageHeight);
                using (var bitmapResized = new System.Drawing.Bitmap(orginalImage, resizedImageWidth, height))
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

        private byte[] GetThumb(byte[] imageOriginal)
        {
            byte[] imageObjectThumbnail;
            using (var stream = new MemoryStream(imageOriginal))
            {
                imageObjectThumbnail = ResizeImage(stream, 250);
            }
            return imageObjectThumbnail;
        }

        private bool IsJpgExtension(byte[] bytes)
        {
            using (MemoryStream imageMemStream = new MemoryStream(bytes))
            {
                using (System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageMemStream))
                {
                    return bitmap.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
        }

        private byte[] FromDataURLToByteArray(string dataUrl)
        {
            var data = dataUrl.Split(',');
            if (!(data.Count() > 1))
            {
                return null;
            }
            string base64 = data[1];
            return Convert.FromBase64String(base64);
        }

        private string GetImagesPath()
        {
            return $"{_configuration.GetSection("MediaServerSettings").GetSection("MediaServerPath").Value}/{_configuration.GetSection("MediaServerSettings").GetSection("ImagesCatalog").Value}";
        }

        private string GetThumbsPath()
        {
            return $"{_configuration.GetSection("MediaServerSettings").GetSection("MediaServerPath").Value}/{_configuration.GetSection("MediaServerSettings").GetSection("ThumbsCatalog").Value}";
        }
        #endregion
    }
}