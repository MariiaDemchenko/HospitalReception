using PhotoManager.Common;
using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.ProjectionModels;
using PhotoManager.Filters;
using PhotoManager.ViewModels.PhotoManagerViewModels;
using System.Web.Mvc;

namespace PhotoManager.Controllers
{
    [RoutePrefix("photos")]
    [ExceptionHandlingAttributeMvc(Message = "Error processing photos")]
    public class PhotoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PhotoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Index(int id)
        {
            return View(new PhotoDisplayViewModel { Id = id, Size = (int)Constants.ImageSize.Original });
        }

        [HttpGet]
        [Route("properties/{id}")]
        public ActionResult Properties(int id)
        {
            var x = new PhotoDisplayViewModel { Id = id, Size = (int)Constants.ImageSize.Original };

            return View(x);
        }

        [Authorize]
        [HttpGet]
        [Route("{id:int=0}/album/{albumId?}")]
        public ActionResult Edit(int id, int? albumId)
        {
            PhotoAddModel photo;
            if (albumId == null)
            {
                photo = id == 0
                   ? new PhotoAddModel()
                   : _unitOfWork.Photos.GetPhotoById(id, Constants.ImageSize.Medium);
            }
            else
            {
                photo = id == 0
                    ? new PhotoAddModel
                    {
                        AlbumId = (int)albumId
                    }
                    : _unitOfWork.Photos.GetPhotoById(id, Constants.ImageSize.Medium, (int)albumId);
            }

            return View(photo);
        }

        [Authorize]
        [HttpGet]
        [Route("add/{id?}")]
        public ActionResult Add(int? id)
        {
            return RedirectToAction("Edit", new { id = 0, albumId = id });
        }
    }
}