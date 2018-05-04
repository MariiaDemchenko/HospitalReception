using Microsoft.AspNet.Identity;
using PhotoManager.Common;
using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.ProjectionModels;
using PhotoManager.Filters;
using PhotoManager.ViewModels.PhotoManagerViewModels;
using System.Web.Http;

namespace PhotoManager.Controllers.Api
{
    [ExceptionHandlingAttributeWebApi(Message = "Error processing albums")]
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

            return Ok(Extensions.GetCollection(albums, scrollViewModel.PageIndex, scrollViewModel.PageSize));
        }

        [HttpGet]
        [Route("album")]
        public IHttpActionResult GetAlbumByModel([FromUri] AlbumSearchModel model,
            [FromUri] ScrollViewModel scrollViewModel)
        {
            var album = _unitOfWork.Albums.GetAlbumByModel(model, User.Identity.GetUserId());

            return Ok(new PhotoAlbumViewModel
            {
                Id = album.Id,
                Name = album.Name,
                Description = album.Description,
                OwnerId = album.OwnerId,
                Photos = Extensions.GetCollection(album.Photos, scrollViewModel.PageIndex, scrollViewModel.PageSize)
            });
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetAlbumById(int? id, [FromUri]ScrollViewModel scrollViewModel)
        {
            var album = _unitOfWork.Albums.GetAlbumById(id, User.Identity.GetUserId());

            return Ok(new PhotoAlbumViewModel
            {
                Id = album.Id,
                Name = album.Name,
                Description = album.Description,
                OwnerId = album.OwnerId,
                Photos = Extensions.GetCollection(album.Photos, scrollViewModel.PageIndex, scrollViewModel.PageSize)
            });
        }

        [Authorize]
        [HttpPut]
        [ValidateAntiForgeryToken]
        [Route("")]
        public IHttpActionResult EditAlbum(AlbumIndexModel album)
        {
            _unitOfWork.Albums.EditAlbum(album);
            _unitOfWork.Save();
            return Ok(album);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("")]
        public IHttpActionResult AddAlbum(AlbumIndexModel album)
        {
            if (string.IsNullOrEmpty(album.Name))
            {
                return BadRequest();
            }

            var existingAlbum = _unitOfWork.Albums.GetAlbumByModel(new AlbumSearchModel { Name = album.Name });
            if (existingAlbum != null)
            {
                return BadRequest();
            }

            var result = _unitOfWork.Albums.AddAlbum(album);
            if (result)
            {
                _unitOfWork.Save();
            }

            return Ok(result);
        }

        [Authorize]
        [HttpDelete]
        [ValidateAntiForgeryToken]
        [Route("")]
        public IHttpActionResult DeleteAlbum(int[] id)
        {
            _unitOfWork.Albums.DeleteAlbums(id);
            _unitOfWork.Save();
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("edit/{id}")]
        public IHttpActionResult EditAlbumPhotos(int id, [FromUri]ScrollViewModel scrollViewModel)
        {
            var album = _unitOfWork.Albums.GetAlbumById(id, User.Identity.GetUserId(), true);
            return Ok(new PhotoAlbumViewModel
            {
                Id = album.Id,
                Name = album.Name,
                Description = album.Description,
                OwnerId = album.OwnerId,
                Photos = Extensions.GetCollection(album.Photos, scrollViewModel.PageIndex, scrollViewModel.PageSize)
            });
        }
    }
}