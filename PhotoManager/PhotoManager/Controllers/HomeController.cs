using PhotoManager.DAL.Repository;
using System.Web.Mvc;

namespace PhotoManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var repository = new PhotoManagerRepository();
            return View(repository.GetPhotos());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}