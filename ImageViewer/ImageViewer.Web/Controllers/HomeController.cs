using ImageClient.Filters;
using ImageClient.ImageServiceReference;
using System.Web.Mvc;

namespace ImageClient.Controllers
{
    public class HomeController : Controller
    {
        readonly ImageViewerClient _client;
        public HomeController()
        {
            _client = new ImageViewerClient();
        }

        [WcfError]
        public ActionResult Index()
        {
            var imageFiles = _client.GetAllImages();
            return View(imageFiles);
        }
    }
}