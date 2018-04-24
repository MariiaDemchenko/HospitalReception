using PhotoManager.Common;
using PhotoManager.DAL.Contracts;
using PhotoManager.ViewModels.PhotoManagerViewModels;
using System.Web.Http;

namespace PhotoManager.Controllers.Api
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("settings/{id}")]
        public IHttpActionResult GetUserSettings(string id)
        {
            if (id == null)
            {
                return Ok(new UserSettingsViewModel { IsAuthorized = false });
            }

            var user = _unitOfWork.Users.GetUserById(id);
            return Ok(new UserSettingsViewModel
            {
                IsAuthorized = User.Identity.IsAuthenticated,
                CanAddPhotos = user.IsPayed && _unitOfWork.Photos.GetUserPhotosCount(id) < int.MaxValue || _unitOfWork.Photos.GetUserPhotosCount(id) < Constants.FreePhotosCount,
                CanAddAlbums = user.IsPayed && _unitOfWork.Albums.GetUserAlbumsCount(id) < int.MaxValue || _unitOfWork.Albums.GetUserAlbumsCount(id) < Constants.FreeAlbumsCount
            });
        }
    }
}
