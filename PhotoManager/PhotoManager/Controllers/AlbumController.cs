using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PhotoManager.DAL.ProjectionModels;
using PhotoManager.ViewModels.PhotoManagerViewModels;

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

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("manage")]
        public ActionResult Manage()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("edit/album")]
        public ActionResult Edit([FromUri] AlbumIndexModel album)
        {
            return View(album);
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("add")]
        public ActionResult Add()
        {
            return View(new AlbumIndexModel { OwnerId = User.Identity.GetUserId() });
        }
    }
}