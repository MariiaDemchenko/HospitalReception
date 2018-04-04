using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace PhotoManager.Controllers
{
    public class AlbumsController : ApiController
    {
        private readonly IPhotoManagerRepository _repository;

        public AlbumsController(IPhotoManagerRepository repository)
        {
            _repository = repository;
        }

        public IHttpActionResult GetAllAlbums()
        {
            IEnumerable<Album> albums = _repository.GetAllAlbums();
            if (albums == null)
            {
                return NotFound();
            }
            return Ok(albums);
        }

        public IHttpActionResult GetAlbumById(int id)
        {
            Album album = _repository.GetAlbumById(id);
            if (album == null)
            {
                return NotFound();
            }
            return Ok(album);
        }
    }
}