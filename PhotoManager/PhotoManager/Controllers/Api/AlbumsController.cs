using Microsoft.AspNet.Identity;
using PhotoManager.Common;
using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.ProjectionModels;
using PhotoManager.ViewModels.PhotoManagerViewModels;
using System.Web.Http;
using System.Web.Http.Results;

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

            var albums = _unitOfWork.Albums.GetAllAlbums();
            if (albums == null)
            {
                return NotFound();
            }
            return Ok(Extensions.TakePartial(albums, scrollViewModel.PageIndex, scrollViewModel.PageSize));
        }

        [HttpGet]
        [Route("album")]
        public IHttpActionResult GetAlbumByModel([FromUri]AlbumSearchModel model, [FromUri]ScrollViewModel scrollViewModel)
        {
            var album = _unitOfWork.Albums.GetAlbumByModel(model);
            if (album?.Photos == null)
            {
                return NotFound();
            }
            album.Photos = Extensions.TakePartial(album.Photos, scrollViewModel.PageIndex, scrollViewModel.PageSize);
            return Ok(album);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetAlbumById(int? id, [FromUri]ScrollViewModel scrollViewModel)
        {
            var album = _unitOfWork.Albums.GetAlbumById(id);
            if (album?.Photos == null)
            {
                return NotFound();
            }
            album.Photos = Extensions.TakePartial(album.Photos, scrollViewModel.PageIndex, scrollViewModel.PageSize);
            return Ok(album);
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult EditAlbum(AlbumIndexModel album)
        {
            _unitOfWork.Albums.EditAlbum(album);
            _unitOfWork.Save();
            return Ok(album);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddAlbum(AlbumIndexModel album)
        {
            var result = _unitOfWork.Albums.AddAlbum(album);
            if (result)
            {
                _unitOfWork.Save();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("add")]
        public IHttpActionResult AddAlbum()
        {
            return Ok(new AlbumIndexModel { OwnerId = User.Identity.GetUserId() });
        }

        [Authorize]
        [HttpDelete]
        [Route("")]
        public IHttpActionResult DeleteAlbum(int[] id)
        {
            _unitOfWork.Albums.DeleteAlbums(id);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IHttpActionResult EditAlbumPhotos(int id, [FromUri]ScrollViewModel scrollViewModel)
        {
            var album = _unitOfWork.Albums.GetAlbumById(id, true);
            album.Photos = Extensions.TakePartial(album.Photos, scrollViewModel.PageIndex, scrollViewModel.PageSize);
            return Ok(album);
        }

    }
}