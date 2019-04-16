using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhotoManager.BLL.Repositories;
using PhotoManager.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace PhotoManager.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPhotoRepository _repository;

        public object ImageFormat { get; private set; }

        public HomeController(IMapper mapper, IPhotoRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet("[action]")]
        public IEnumerable<PhotoViewModel> GetDemoPhotos()
        {
            var photos = _repository.GetAll(0, 15).ToList();
            return photos.Select(photo => _mapper.Map<PhotoViewModel>(photo));
        }
    }
}