using AutoMapper;
using Microsoft.AspNet.Identity;
using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Models;
using PhotoManager.ViewModels.PhotoManagerViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PhotoManager.Controllers
{
    public class PhotosController : ApiController
    {
        private readonly IPhotoRepository _repository;

        public PhotosController(IPhotoRepository repository)
        {
            _repository = repository;
        }

        public IHttpActionResult GetAllPhotos()
        {
            IEnumerable<PhotoViewModel> photos = Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoViewModel>>(_repository.GetAllPhotos());
            return Ok(photos);
        }

        public IHttpActionResult GetPhotoById(int id)
        {
            PhotoViewModel photo = Mapper.Map<Photo, PhotoViewModel>(_repository.GetPhotoById(id));
            if (photo == null)
            {
                return NotFound();
            }
            return Ok(photo);
        }

        [HttpPost]
        [Route("api/photos/edit")]
        public IHttpActionResult Edit(PhotoViewModel photoViewModel)
        {
            CameraSettings cameraSettings = Mapper.Map<PhotoViewModel, CameraSettings>(photoViewModel);

            var photo = new Photo
            {
                Id = photoViewModel.Id,
                Name = photoViewModel.Name,
                CreationDate = photoViewModel.CreationDate,
                Place = photoViewModel.Place
            };

            _repository.EditCameraSettings(cameraSettings);
            _repository.EditPhoto(photo);

            return Ok();
        }

        [HttpPost]
        [Route("api/photos/add")]
        public IHttpActionResult Add(PhotoViewModel photoViewModel)
        {
            var cameraSettings = Mapper.Map<PhotoViewModel, CameraSettings>(photoViewModel);

            var cameraSettingsId = _repository.AddCameraSettings(cameraSettings);

            var imageId = _repository.AddImage(photoViewModel.Image);
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

            _repository.AddPhoto(photoViewModel.AlbumId, photo);

            return Ok();
        }

        [HttpPost]
        [Route("api/photos/search")]
        public IHttpActionResult Search(SearchModel model)
        {
            var photos = string.IsNullOrEmpty(model.KeyWord) ? _repository.GetAllPhotos() : _repository.GetPhotosByKeyWord(model.KeyWord);
            IEnumerable<PhotoViewModel> photoViewModels = Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoViewModel>>(photos.ToList());
            if (photoViewModels == null)
            {
                return NotFound();
            }
            return Ok(photoViewModels);
        }
    }
}