using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhotoManager.BLL.Repositories;
using PhotoManager.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace PhotoManager.Controllers
{
    [Route("api/[controller]")]
    public class AlbumPhotosController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPhotoRepository _repository;

        public object ImageFormat { get; private set; }

        public AlbumPhotosController(IMapper mapper, IPhotoRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet("[action]/{albumId}")]
        public IEnumerable<PhotoViewModel> GetAlbumPhotos(string albumId, int pageIndex, int pageSize)
        {
            var photos = _repository.GetByAlbumId(pageIndex * pageSize, pageSize, albumId).ToList();
            var photoViewModels = photos.Select(photo => _mapper.Map<PhotoViewModel>(photo));
            return photoViewModels;
        }
    }
}