using AutoMapper;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
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
        public IHttpActionResult GetAllPhotos()
        {
            var photos = Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoViewModel>>(_unitOfWork.Photos.GetAllPhotos());
            return Ok(photos);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var photo = Mapper.Map<Photo, PhotoViewModel>(_unitOfWork.Photos.GetPhotoById(id));
            return Ok(photo);
        }

        [HttpGet]
        [Route("{id}/album/{albumId}")]
        public IHttpActionResult Edit(int id, int albumId = 0)
        {
            var photo = Mapper.Map<Photo, PhotoViewModel>(_unitOfWork.Photos.GetPhotoById(id));
            photo.AlbumId = albumId;
            return Ok(photo);
        }

        [HttpGet]
        [Route("album/{albumId}")]
        public IHttpActionResult Add(int albumId)
        {
            var photoViewModel = Mapper.Map<Photo, PhotoViewModel>(new Photo());
            photoViewModel.AlbumId = albumId;
            return Ok(photoViewModel);
        }

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

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Add()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath(Common.Constants.Root);
            var provider = new MultipartFormDataStreamProvider(root);
            await Request.Content.ReadAsMultipartAsync(provider);

            try
            {
                var photoViewModel = JsonConvert.DeserializeObject<PhotoViewModel>(provider.FormData.GetValues("ViewModel")?.FirstOrDefault());
                photoViewModel.Image = File.ReadAllBytes(provider.FileData.FirstOrDefault().LocalFileName);

                var cameraSettings = Mapper.Map<PhotoViewModel, CameraSettings>(photoViewModel);
                var cameraSettingsId = _unitOfWork.Photos.AddCameraSettings(cameraSettings);
                var imageId = _unitOfWork.Photos.AddImage(photoViewModel.Image);
                var ownerId = HttpContext.Current.User.Identity.GetUserId();

                var photo = new Photo
                {
                    OwnerId = ownerId,
                    CameraSettingsId = cameraSettingsId,
                    ImageId = imageId,
                    Name = photoViewModel.Name,
                    CreationDate = photoViewModel.CreationDate,
                    Place = photoViewModel.Place
                };

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
        [Route("search/{filter}")]
        public IHttpActionResult Search(string filter)
        {
            var photos = string.IsNullOrEmpty(filter) ? _unitOfWork.Photos.GetAllPhotos() : _unitOfWork.Photos.GetPhotosByKeyWord(filter);
            var photoViewModels = Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoViewModel>>(photos.ToList());
            if (photoViewModels == null)
            {
                return NotFound();
            }
            return Ok(photoViewModels);
        }

        [HttpDelete]
        [Route("album/{albumId}")]
        public IHttpActionResult Delete(int albumId, int[] id)
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

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}