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
        private readonly string _mediaServerPath;

        public object ImageFormat { get; private set; }

        public PhotosController(IMapper mapper, IPhotoRepository repository, IConfiguration configuration)
        {
            _mapper = mapper;
            _repository = repository;
            _mediaServerPath = configuration.GetSection("MediaServerPath").Value;
        }

        [HttpGet("[action]")]
        public IEnumerable<PhotoViewModel> GetPhotosByAlbumId(string albumId, int pageIndex, int pageSize)
        {
            var photos = _repository.GetByAlbumId(pageIndex * pageSize, pageSize, albumId).ToList();
            var photoViewModels = photos.Select(photo => _mapper.Map<PhotoViewModel>(photo));
            return photoViewModels;
        }

        [HttpGet("[action]")]
        public PhotoViewModel GetPhotoById(string photoId)
        {
            var photo = _repository.Get(photoId);
            return _mapper.Map<PhotoViewModel>(photo);
        }               

        [HttpPost("[action]")]
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

                        await System.IO.File.WriteAllBytesAsync($"{"//mdemchenko/MediaServer/images"}/{photo.ServerName}.jpg", imageToReplace);
                        await System.IO.File.WriteAllBytesAsync($"{"//mdemchenko/MediaServer/thumbs"}/{photo.ServerName}.jpg", GetThumb(imageToReplace));
                    }

                    if (photo.PhotoId == null)
                    {
                        _repository.Create(_mapper.Map<Photo>(photo), photo.AlbumId);
                    }
                    else
                    {
                        _repository.Update(photo.PhotoId, _mapper.Map<Photo>(photo));
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest("Error processing photo: " + ex.Message);
                }
            }
            return Ok("Updated successfully");
        }

        [HttpPost("[action]")]
        [Authorize]
        [DisableRequestSizeLimit]
        public ActionResult RemovePhotos([FromBody]IEnumerable<PhotoViewModel> photos)
        {
            foreach (var photo in photos)
            {
                try
                {
                    _repository.Remove(_mapper.Map<Photo>(photo), photo.AlbumId);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error processing photos: " + ex.Message);
                }
            }
            return Ok();
        }        

        [HttpGet("[action]")]
        public IEnumerable<PhotoViewModel> GetAllPhotos(int pageIndex, int pageSize)
        {
            var photos = _repository.GetAll(pageIndex * pageSize, pageSize).ToList();
            return photos.Select(photo => _mapper.Map<PhotoViewModel>(photo));
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
        #endregion
    }
}