using AutoMapper;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using PhotoManager.Common;
using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Models;
using PhotoManager.ViewModels.PhotoManagerViewModels;
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
            var photos = _unitOfWork.Photos.GetAllPhotos()?.Skip(scrollViewModel.PageIndex * scrollViewModel.PageSize)
                .Take(scrollViewModel.PageSize).ToList();
            var photoViewModels = Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoViewModel>>(photos);
            if (photoViewModels == null)
            {
                return NotFound();
            }

            return Ok(photoViewModels);
        }

        [HttpGet]
        [Route("{id}/{size}")]
        public IHttpActionResult GetByIdAndSize(int id, int size)
        {
            var imageSize = (Constants.ImageSize)size;

            var sourcePhoto = _unitOfWork.Photos.GetPhotoById(id);
            if (imageSize == Constants.ImageSize.Medium)
            {
                return Ok(Mapper.Map<Photo, PhotoMediumViewModel>(sourcePhoto));
            }
            return Ok(Mapper.Map<Photo, PhotoOriginalViewModel>(sourcePhoto));
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/album/{albumId}")]
        public IHttpActionResult Edit(int id, int albumId)
        {
            var photo = Mapper.Map<Photo, PhotoMediumViewModel>(_unitOfWork.Photos.GetPhotoById(id));
            photo.AlbumId = albumId;
            return Ok(photo);
        }

        [Authorize]
        [HttpGet]
        [Route("album/{albumId}")]
        public IHttpActionResult Add(int albumId)
        {
            var photoViewModel = new PhotoMediumViewModel()
            {
                AlbumId = albumId,
                ImageUrl = "/api/image/0"
            };
            photoViewModel.AlbumId = albumId;
            return Ok(photoViewModel);
        }

        [Authorize]
        [HttpPut]
        [Route("")]
        public IHttpActionResult Edit(PhotoViewModel photoViewModel)
        {
            var cameraSettings = Mapper.Map<PhotoViewModel, CameraSettings>(photoViewModel);

            var photo = new Photo
            {
                Id = (int)photoViewModel.Id,
                Name = photoViewModel.Name,
                CreationDate = photoViewModel.CreationDate,
                Place = photoViewModel.Place
            };

            _unitOfWork.Photos.EditCameraSettings(cameraSettings);
            _unitOfWork.Photos.EditPhoto(photo);
            _unitOfWork.Save();

            return Ok(photoViewModel);
        }

        [Authorize]
        [HttpPost]
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
                var photoViewModel = JsonConvert.DeserializeObject<PhotoViewModel>(provider.FormData.GetValues("ViewModel")?.FirstOrDefault());
                var imageOriginal = File.ReadAllBytes(provider.FileData.FirstOrDefault().LocalFileName);

                var cameraSettings = Mapper.Map<PhotoViewModel, CameraSettings>(photoViewModel);
                var cameraSettingsId = _unitOfWork.Photos.AddCameraSettings(cameraSettings);

                var ownerId = HttpContext.Current.User.Identity.GetUserId();
                var images = GetImagesInDifferentShapes(imageOriginal);

                var photo = new Photo
                {
                    OwnerId = ownerId,
                    CameraSettingsId = cameraSettingsId,
                    Name = photoViewModel.Name,
                    CreationDate = photoViewModel.CreationDate,
                    Place = photoViewModel.Place
                };
                photo.Images = new List<Image>();
                photo.Images.AddRange(images);
                _unitOfWork.Photos.AddPhoto(photoViewModel.AlbumId, photo);
                _unitOfWork.Save();

                return Ok(photoViewModel);
            }
            catch
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
            var photos = _unitOfWork.Photos.GetPhotosByKeyWord(filter)?.Skip(scrollViewModel.PageIndex * scrollViewModel.PageSize).Take(scrollViewModel.PageSize).ToList();

            var photoViewModels = Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoViewModel>>(photos);
            if (photoViewModels == null)
            {
                return NotFound();
            }
            return Ok(photoViewModels);
        }

        [HttpGet]
        [Route("advancedSearchModel")]
        public IHttpActionResult AdvancedSearch()
        {
            var photoViewModel = Mapper.Map<Photo, PhotoViewModel>(new Photo());
            if (photoViewModel == null)
            {
                return NotFound();
            }
            return Ok(photoViewModel);
        }

        [HttpGet]
        [Route("advancedSearch")]
        public IHttpActionResult AdvancedSearch([FromUri]AdvancedSearchViewModel advancedSearchViewModel)
        {
            var photoViewModel = advancedSearchViewModel.PhotoViewModel;

            var cameraSettings = Mapper.Map<PhotoViewModel, CameraSettings>(photoViewModel);

            var photo = new Photo
            {
                Name = photoViewModel.Name,
                CreationDate = photoViewModel.CreationDate,
                Place = photoViewModel.Place,
                CameraSettings = cameraSettings
            };

            var skipCount = advancedSearchViewModel.ScrollViewModel.PageIndex *
                            advancedSearchViewModel.ScrollViewModel.PageSize;

            var takeCount = advancedSearchViewModel.ScrollViewModel.PageSize;
            var photos = _unitOfWork.Photos.GetPhotosBySearchModel(photo).Skip(skipCount).Take(takeCount);
            var photoViewModels = Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoViewModel>>(photos.ToList());
            if (photoViewModels == null)
            {
                return NotFound();
            }
            return Ok(photoViewModels);
        }

        [Authorize]
        [HttpDelete]
        [Route("album/{albumId}")]
        public IHttpActionResult Delete(int? albumId, int[] id)
        {
            _unitOfWork.Photos.DeletePhotos(id);
            _unitOfWork.Save();
            var photoViewModels = Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoViewModel>>(_unitOfWork.Photos.GetPhotosByAlbumId(albumId));
            if (photoViewModels == null)
            {
                return NotFound();
            }
            return Ok(photoViewModels);
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
    }
}