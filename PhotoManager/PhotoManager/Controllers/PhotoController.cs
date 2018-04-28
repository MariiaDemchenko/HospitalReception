using PhotoManager.DAL.ProjectionModels;
using PhotoManager.ViewModels.PhotoManagerViewModels;
using System.Web.Http;
using System.Web.Mvc;

namespace PhotoManager.Controllers
{
    [System.Web.Mvc.RoutePrefix("photos")]
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
    }
}