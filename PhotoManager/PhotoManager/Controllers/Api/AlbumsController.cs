using AutoMapper;
using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Models;
using PhotoManager.ViewModels.PhotoManagerViewModels;
using System.Collections.Generic;
using System.Linq;
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
        public IHttpActionResult GetAllAlbums([FromUri]ScrollViewModel scrollViewModel)
        {
            var albums = _unitOfWork.Albums.GetAllAlbums()?
                .Skip(scrollViewModel.PageIndex * scrollViewModel.PageSize)
                .Take(scrollViewModel.PageSize);
            if (albums == null)
            {
                return NotFound();
            }
            var albumViewModels = Mapper.Map<IEnumerable<Album>, IEnumerable<AlbumCoverViewModel>>(albums);
            
            return Ok(albumViewModels);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetAlbumById(int id, [FromUri]ScrollViewModel scrollViewModel)
        {
            var album = Mapper.Map<Album, AlbumViewModel>(_unitOfWork.Albums.GetAlbumById(id));
            if (album?.Photos == null)
            {
                return NotFound();
            }
            album.Photos = album.Photos.Skip(scrollViewModel.PageIndex * scrollViewModel.PageSize).Take(scrollViewModel.PageSize).ToList();
            return Ok(album);
        }
    }
}