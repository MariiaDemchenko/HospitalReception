using System.Web.Mvc;

namespace PhotoManager.Controllers
{
    [RoutePrefix("albums")]
    public class AlbumController : Controller
    {
        [HttpGet]
        [Route("{id}")]
        public ActionResult Index(int id)
        {
            return View(id);
        }

        [HttpGet]
        [Route("manage")]
        public ActionResult Manage()
        {
            return View();
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            return View(id);
        }

        [HttpGet]
        [Route("add")]
        public ActionResult Add()
        {
            return View();
        }
    }
}