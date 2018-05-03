using AutoMapper;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using PhotoManager.Common;
using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Models;
using PhotoManager.DAL.ProjectionModels;
using PhotoManager.Filters;
using PhotoManager.ViewModels.PhotoManagerViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Constants = PhotoManager.Common.Constants;
using Image = PhotoManager.DAL.Models.Image;

namespace PhotoManager.Controllers.Api
{
    [RoutePrefix("api/photos")]
    [ExceptionHandlingAttributeWebApi(Message = "Error processing photos")]
    public class PhotosController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public PhotosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllPhotos([FromUri] ScrollViewModel scrollViewModel)
        {
            var photos = _unitOfWork.Photos.GetAllPhotos();
            return Ok(Extensions.GetCollection(photos, scrollViewModel.PageIndex, scrollViewModel.PageSize));
        }

        [HttpGet]
        [Route("{id}/{size}")]
        public IHttpActionResult GetByIdAndSize(int id, int size)
        {
            var sourcePhoto = _unitOfWork.Photos.GetPhotoById(id, (Constants.ImageSize)size);

            return Ok(Mapper.Map<PhotoPropertiesModel>(sourcePhoto));
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/album")]
        public IHttpActionResult Edit(int id)
        {
            var photo = _unitOfWork.Photos.GetPhotoById(id, Constants.ImageSize.Medium);
            if (photo == null)
            {
                return NotFound();
            }
            return Ok(photo);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/album/{albumId}")]
        public IHttpActionResult Edit(int id, int albumId)
        {
            var photo = _unitOfWork.Photos.GetPhotoById(id, Constants.ImageSize.Medium, albumId);
            if (photo == null)
            {
                return NotFound();
            }
            return Ok(photo);
        }

        [Authorize]
        [HttpPut]
        [ValidateAntiForgeryToken]
        [Route("")]
        public IHttpActionResult Edit(PhotoEditModel photoViewModel)
        {
            if (!PhotoIsValid(photoViewModel))
            {
                return BadRequest();
            }
            _unitOfWork.Photos.EditPhoto(photoViewModel);
            _unitOfWork.Save();
            return Ok(photoViewModel.AlbumId);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("")]
        public async Task<IHttpActionResult> Add()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath(Constants.Root);
            var provider = new MultipartFormDataStreamProvider(root);
            await Request.Content.ReadAsMultipartAsync(provider);

            try
            {
                var photoViewModel = JsonConvert.DeserializeObject<PhotoAddModel>(provider.FormData.GetValues("ViewModel")?.FirstOrDefault());
                var imageOriginal = File.ReadAllBytes(provider.FileData.FirstOrDefault().LocalFileName);
                var images = GetImagesInDifferentShapes(imageOriginal).ToList();

                photoViewModel.OwnerId = HttpContext.Current.User.Identity.GetUserId();
                photoViewModel.Images = new List<Image>();
                photoViewModel.Images.AddRange(images);

                _unitOfWork.Photos.AddPhoto(photoViewModel);
                _unitOfWork.Save();
                return Ok(photoViewModel.AlbumId);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("search")]
        public IHttpActionResult Search([FromUri]ScrollViewModel scrollViewModel = null)
        {
            return Search(string.Empty, scrollViewModel);
        }

        [HttpGet]
        [Route("search/{filter}")]
        public IHttpActionResult Search(string filter, [FromUri]ScrollViewModel scrollViewModel = null)
        {
            var keyWord = !string.IsNullOrEmpty(filter) ? filter.Trim(' ') : string.Empty;
            var photos = Extensions.GetCollection(_unitOfWork.Photos.GetPhotosByKeyWord(keyWord).ToList(),
                scrollViewModel.PageIndex, scrollViewModel.PageSize);
            return Ok(photos);
        }

        [HttpGet]
        [Route("advancedSearchModel")]
        public IHttpActionResult AdvancedSearch()
        {
            var photoViewModel = new PhotoThumbnailModel();
            return Ok(photoViewModel);
        }

        [HttpGet]
        [Route("advancedSearch")]
        public IHttpActionResult AdvancedSearch([FromUri]AdvancedSearchViewModel advancedSearchViewModel)
        {
            var photoViewModel = advancedSearchViewModel?.PhotoViewModel;

            var photos = Extensions.GetCollection(_unitOfWork.Photos.GetPhotosBySearchModel(photoViewModel).ToList(),
                advancedSearchViewModel.ScrollViewModel.PageIndex, advancedSearchViewModel.ScrollViewModel.PageSize);
            return Ok(photos);
        }

        [Authorize]
        [HttpDelete]
        [Route("album")]
        public IHttpActionResult Delete(int[] id)
        {
            _unitOfWork.Photos.DeletePhotos(id);
            _unitOfWork.Save();

            return Ok(_unitOfWork.Photos.GetAllPhotos());
        }

        [Authorize]
        [HttpDelete]
        [Route("album/{albumId}")]
        public IHttpActionResult Delete(int? albumId, int[] id)
        {
            _unitOfWork.Photos.DeletePhotos(id);
            _unitOfWork.Save();
            var album = _unitOfWork.Albums.GetAlbumById(albumId, User.Identity.GetUserId());
            if (album.Photos == null)
            {
                return NotFound();
            }
            return Ok(album.Photos);
        }

        [HttpPost]
        [Route("like")]
        public IHttpActionResult AddLike([FromUri]Like like)
        {
            var userId = User.Identity.GetUserId();
            var likesModel = _unitOfWork.Photos.AddLike(userId, like.Id, like.AlbumId, like.IsPositive);
            _unitOfWork.Save();
            return Ok(likesModel);
        }

        private IEnumerable<Image> GetImagesInDifferentShapes(byte[] imageOriginal)
        {
            byte[] imageObjectMedium;
            byte[] imageObjectThumbnail;
            using (var stream = new MemoryStream(imageOriginal))
            {
                imageObjectThumbnail = Extensions.ResizeImage(stream, 250);
                imageObjectMedium = Extensions.ResizeImage(stream, 300);
            }

            return new List<Image>
            {
                new Image
                {
                    Bytes = imageObjectThumbnail,
                    Size = Constants.ImageSize.Thumbnail
                },
                new Image
                {
                    Bytes = imageObjectMedium,
                    Size = Constants.ImageSize.Medium
                },
                new Image
                {
                    Bytes = imageOriginal,
                    Size = Constants.ImageSize.Original
                }
            };
        }

        private bool PhotoIsValid(PhotoEditModel photo)
        {
            return !string.IsNullOrEmpty(photo.Name) &&
                    photo.Iso >= 1 && photo.Iso <= Constants.MaxIso &&
                    photo.LensFocalLength >= 1 && photo.LensFocalLength <= Constants.MaxLensFocalLength;
        }
    }
}