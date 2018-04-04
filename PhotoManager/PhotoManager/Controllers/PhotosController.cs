using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace PhotoManager.Controllers
{
    public class PhotosController : ApiController
    {
        private readonly IPhotoManagerRepository _repository;

        public PhotosController(IPhotoManagerRepository repository)
        {
            _repository = repository;
        }

        public IHttpActionResult GetPhotoById(int id)
        {
            Photo photo = _repository.GetPhotoById(id);
            if (photo == null)
            {
                return NotFound();
            }
            return Ok(photo);
        }

        public IHttpActionResult GetPhotosByKeyWord(string keyWord)
        {
            IEnumerable<Photo> photos = _repository.GetPhotoByKeyWord(keyWord);
            if (photos == null)
            {
                return NotFound();
            }
            return Ok(photos);
        }
    }
}