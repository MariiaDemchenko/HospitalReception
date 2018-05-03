using PhotoManager.DAL.ProjectionModels;
using PhotoManager.Filters;
using PhotoManager.ViewModels.PhotoManagerViewModels;
using System.Web.Http;
using System.Web.Mvc;

namespace PhotoManager.Controllers
{
    [System.Web.Mvc.RoutePrefix("photos")]
    [ExceptionHandlingAttributeMvc(Message = "Error processing photos")]
    public class PhotoController : Controller
    {
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("{id}")]
        public ActionResult Index(int id)
        {
            return View(new PhotoDisplayViewModel { Id = id, Size = (int)Common.Constants.ImageSize.Original });
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("properties/{id}")]
        public ActionResult Properties(int id)
        {
            var x = new PhotoDisplayViewModel { Id = id, Size = (int)Common.Constants.ImageSize.Original };

            return View(x);
        }

        [System.Web.Mvc.Authorize]
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("edit/photo")]
        public ActionResult Edit([FromUri]PhotoEditModel photo)
        {
            return View(photo);
        }

        [System.Web.Mvc.Authorize]
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("add")]
        public ActionResult Add()
        {
            var model = new PhotoAddModel
            {
                ImageUrl = "/api/image/"
            };
            return View(model);
        }

        [System.Web.Mvc.Authorize]
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("add/{id}")]
        public ActionResult Add(int id)
        {
            var model = new PhotoAddModel
            {
                AlbumId = id,
                ImageUrl = "/api/image/"
            };
            return View(model);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("error")]
        public ActionResult ErrorIndex()
        {
            TempData["ErrorMessage"] = "Error getting photo";
            return RedirectToAction("Error");
        }

        [System.Web.Mvc.Route("error/edit")]
        public ActionResult ErrorEdit()
        {
            TempData["ErrorMessage"] = "Error editing photo";
            return RedirectToAction("Error");
        }

        [System.Web.Mvc.Route("error/add")]
        public ActionResult ErrorAdd()
        {
            TempData["ErrorMessage"] = "Error adding photo";
            return RedirectToAction("Error");
        }

        [System.Web.Mvc.Route("error/delete")]
        public ActionResult ErrorDelete()
        {
            TempData["ErrorMessage"] = "Error deleting photo";
            return RedirectToAction("Error");
        }
    }
}