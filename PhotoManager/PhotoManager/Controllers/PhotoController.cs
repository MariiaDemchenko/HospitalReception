using System.Web.Mvc;

namespace PhotoManager.Controllers
{
    [RoutePrefix("photos")]
    public class PhotoController : Controller
    {
        [HttpGet]
        [Route("{id}")]
        public ActionResult Index(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpGet]
        [Route("edit/{id}/album/{albumId}")]
        public ActionResult Edit(int id, int albumId = 0)
        {
            ViewBag.Id = id;
            ViewBag.AlbumId = albumId;
            return View();
        }

        [HttpGet]
        [Route("add/{id}")]
        public ActionResult Add(int id)
        {
            ViewBag.AlbumId = id;
            return View();
        }
    }
}