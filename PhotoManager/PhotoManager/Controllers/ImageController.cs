using PhotoManager.DAL.Contracts;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PhotoManager.Controllers
{
    public class ImageController : ApiController
    {
        private readonly IPhotoRepository _repository;

        public ImageController(IPhotoRepository repository)
        {
            _repository = repository;
        }

        public HttpResponseMessage GetImage(int id)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            var image = _repository.GetImageById(id)?.Bytes;

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