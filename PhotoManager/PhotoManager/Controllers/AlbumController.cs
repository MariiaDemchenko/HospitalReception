using Microsoft.AspNet.Identity;
using PhotoManager.DAL.ProjectionModels;
using System.Web.Http;
using System.Web.Mvc;

namespace PhotoManager.Controllers
{
    [System.Web.Mvc.RoutePrefix("albums")]
    public class AlbumController : Controller
    {
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("{name}")]
        public ActionResult Index(string name)
        {
            return View(new AlbumSearchModel { Name = name });
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("album")]
        public ActionResult Index(AlbumSearchModel album)
        {
            return View(album);
        }

        [System.Web.Http.Authorize]
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("manage")]
        public ActionResult Manage()
        {
            return View();
        }

        [System.Web.Http.Authorize]
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("edit/album")]
        public ActionResult Edit([FromUri] AlbumIndexModel album)
        {
            return View(album);
        }

        [System.Web.Http.Authorize]
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("add")]
        public ActionResult Add()
        {
            return View(new AlbumIndexModel { OwnerId = User.Identity.GetUserId() });
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
            TempData["ErrorMessage"] = "Error getting album";
            return RedirectToAction("Error");
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("error/edit")]
        public ActionResult ErroEditing()
        {
            TempData["ErrorMessage"] = "Error editing album";
            return RedirectToAction("Error");
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("error/add")]
        public ActionResult ErroAdding()
        {
            TempData["ErrorMessage"] = "Error adding album";
            return RedirectToAction("Error");
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("error/delete")]
        public ActionResult ErroDeleting()
        {
            TempData["ErrorMessage"] = "Error deleting album";
            return RedirectToAction("Error");
        }
    }
}