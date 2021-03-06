using Microsoft.AspNet.Identity;
using PhotoManager.DAL.Contracts;
using PhotoManager.Filters;
using PhotoManager.ViewModels.PhotoManagerViewModels;
using System.Web.Http;
using Constants = PhotoManager.Common.Constants;

namespace PhotoManager.Controllers.Api
{
    [RoutePrefix("api/users")]
    [ExceptionHandlingAttributeWebApi(Message = "Error processing users")]
    public class UsersController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetUserId()
        {
            return Ok(User.Identity.GetUserId());
        }

        [HttpGet]
        [Route("settings")]
        public IHttpActionResult GetUserSettings()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Ok(new UserSettingsViewModel { IsAuthorized = false });
            }

            var id = User.Identity.GetUserId();

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