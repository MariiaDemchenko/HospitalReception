using PhotoManager.DAL.Contracts;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;

namespace PhotoManager.Controllers.Api
{
    [RoutePrefix("api/image")]
    public class ImageController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("{id}")]
        public HttpResponseMessage GetImage(int id)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            byte[] image = null;
            if (id == 0)
            {
                var rootPath = HostingEnvironment.MapPath("~/Content/Images/");
                var filePath = Path.Combine(rootPath, "emptyImage.jpg");
                if (File.Exists(filePath))
                {
                    image = File.ReadAllBytes(filePath);
                }
            }
            else
            {
                image = _unitOfWork.Photos.GetImageById(id)?.Bytes;
            }

            if (image == null)
            {
                return response;
            }

            var ms = new MemoryStream(image);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            return response;
        }
    }
}