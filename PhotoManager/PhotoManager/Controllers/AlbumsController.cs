using AutoMapper;
using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Models;
using PhotoManager.ViewModels.PhotoManagerViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace PhotoManager.Controllers
{
    public class AlbumsController : ApiController
    {
        private readonly IAlbumRepository _repository;

        public AlbumsController(IAlbumRepository repository)
        {
            _repository = repository;
        }

        public IHttpActionResult GetAllAlbums()
        {
            var albums = Mapper.Map<IEnumerable<Album>, IEnumerable<AlbumViewModel>>(_repository.GetAllAlbums());
            if (albums == null)
            {
                return NotFound();
            }
            return Ok(albums);
        }

        public IHttpActionResult GetAlbumById(int id)
        {
            var album = Mapper.Map<Album, AlbumViewModel>(_repository.GetAlbumById(id));
            if (album == null)
            {
                return NotFound();
            }
            return Ok(album);
        }
    }
}