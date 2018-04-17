using AutoMapper;
using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Models;
using PhotoManager.ViewModels.PhotoManagerViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace PhotoManager.Controllers.Api
{
    [RoutePrefix("api/albums")]
    public class AlbumsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AlbumsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllAlbums()
        {
            var albums = Mapper.Map<IEnumerable<Album>, IEnumerable<AlbumCoverViewModel>>(_unitOfWork.Albums.GetAllAlbums());
            if (albums == null)
            {
                return NotFound();
            }
            return Ok(albums);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetAlbumById(int id)
        {
            var album = Mapper.Map<Album, AlbumViewModel>(_unitOfWork.Albums.GetAlbumById(id));
            if (album == null)
            {
                return NotFound();
            }
            return Ok(album);
        }
    }
}