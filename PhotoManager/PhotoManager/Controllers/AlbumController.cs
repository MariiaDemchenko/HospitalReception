using System.Web.Mvc;

namespace PhotoManager.Controllers
{
    [RoutePrefix("albums")]
    public class AlbumController : Controller
    {
        [HttpGet]
        [Route("{id}")]
        public ActionResult Index(int id = 0)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}