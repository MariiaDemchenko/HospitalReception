using Microsoft.AspNet.Identity;
using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.ProjectionModels;
using System.Web.Mvc;

namespace PhotoManager.Controllers
{
    [RoutePrefix("albums")]
    public class AlbumController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AlbumController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("{name}")]
        public ActionResult Index(string name)
        {
            return View(new AlbumSearchModel { Name = name });
        }

        [HttpGet]
        [Route("album")]
        public ActionResult Index(AlbumSearchModel album)
        {
            return View(album);
        }

        [Authorize]
        [HttpGet]
        [Route("manage")]
        public ActionResult Manage()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        [Route("edit/{id?}")]
        public ActionResult Edit(int? id)
        {
            var album = id == null ?
                new AlbumIndexModel { OwnerId = User.Identity.GetUserId() } :
                _unitOfWork.Albums.GetAlbumById(id, 0, 0, User.Identity.GetUserId(), true);
            return View(album);
        }

        [Authorize]
        [HttpGet]
        [Route("add")]
        public ActionResult Add()
        {
            return RedirectToAction("Edit");
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
            TempData["ErrorMessage"] = "Error getting album";
            return RedirectToAction("Error");
        }

        [HttpGet]
        [Route("error/edit")]
        public ActionResult ErroEditing()
        {
            TempData["ErrorMessage"] = "Error editing album";
            return RedirectToAction("Error");
        }

        [HttpGet]
        [Route("error/add")]
        public ActionResult ErroAdding()
        {
            TempData["ErrorMessage"] = "Error adding album";
            return RedirectToAction("Error");
        }

        [HttpGet]
        [Route("error/delete")]
        public ActionResult ErroDeleting()
        {
            TempData["ErrorMessage"] = "Error deleting album";
            return RedirectToAction("Error");
        }
    }
}