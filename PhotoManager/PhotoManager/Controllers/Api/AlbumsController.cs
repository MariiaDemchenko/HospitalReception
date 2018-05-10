using Microsoft.AspNet.Identity;
using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.ProjectionModels;
using PhotoManager.Filters;
using PhotoManager.ViewModels.PhotoManagerViewModels;
using System.Web.Http;
using Constants = PhotoManager.Common.Constants;

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
            var albums = _unitOfWork.Albums.GetAllAlbums(scrollViewModel.PageIndex, scrollViewModel.PageSize);
            return Ok(albums);
        }

        [HttpGet]
        [Route("album")]
        public IHttpActionResult GetAlbumByModel([FromUri] AlbumSearchModel model,
            [FromUri] ScrollViewModel scrollViewModel)
        {
            var album = _unitOfWork.Albums.GetAlbumByModel(model, scrollViewModel.PageIndex, scrollViewModel.PageSize, User.Identity.GetUserId());

            return Ok(album);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetAlbumById(int? id, [FromUri]ScrollViewModel scrollViewModel)
        {
            var album = _unitOfWork.Albums.GetAlbumById(id, scrollViewModel.PageIndex, scrollViewModel.PageSize, User.Identity.GetUserId());

            return Ok(album);
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
            var id = User.Identity.GetUserId();

            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("User was not authorized");
            }

            var user = _unitOfWork.Users.GetUserById(User.Identity.GetUserId());
            var canAddAlbums = user.IsPayed && _unitOfWork.Albums.GetUserAlbumsCount(id) < int.MaxValue ||
                               _unitOfWork.Albums.GetUserAlbumsCount(id) < Constants.FreeAlbumsCount;

            if (!canAddAlbums)
            {
                return BadRequest("User can not add photos due to payment restriction");
            }
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
            var album = _unitOfWork.Albums.GetAlbumById(id, scrollViewModel.PageIndex, scrollViewModel.PageSize, User.Identity.GetUserId(), true);
            return Ok(album);
        }

        [Authorize]
        [HttpGet]
        [Route("edit/fromPage/{id}")]
        public IHttpActionResult GetAlbumPhotosFromPage(int id, [FromUri]ScrollViewModel scrollViewModel)
        {
            var album = _unitOfWork.Albums.GetSelectedAlbumPhotosFromPageIndex(id, scrollViewModel.PageIndex, scrollViewModel.PageSize, User.Identity.GetUserId());
            return Ok(album);
        }
    }
}