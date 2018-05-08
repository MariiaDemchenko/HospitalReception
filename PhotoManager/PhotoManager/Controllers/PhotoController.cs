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
        [Route("{id}/album/{albumId?}")]
        public ActionResult Edit(int? albumId, int id = 0)
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
        [Route("add")]
        public ActionResult Add()
        {
            return RedirectToAction("Edit", new {id = 0});
        }

        [Authorize]
        [HttpGet]
        [Route("add/{id?}")]
        public ActionResult Add(int? id)
        {
            return RedirectToAction("Edit", new { albumId = id, id = 0 });
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        [Route("error")]
        public ActionResult ErrorIndex()
        {
            TempData["ErrorMessage"] = "Error getting photo";
            return RedirectToAction("Error");
        }

        [Route("error/edit")]
        public ActionResult ErrorEdit()
        {
            TempData["ErrorMessage"] = "Error editing photo";
            return RedirectToAction("Error");
        }

        [Route("error/add")]
        public ActionResult ErrorAdd()
        {
            TempData["ErrorMessage"] = "Error adding photo";
            return RedirectToAction("Error");
        }

        [Route("error/delete")]
        public ActionResult ErrorDelete()
        {
            TempData["ErrorMessage"] = "Error deleting photo";
            return RedirectToAction("Error");
        }
    }
}