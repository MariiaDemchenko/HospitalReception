using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoManager.BLL.Repositories;
using PhotoManager.Models;
using PhotoManager.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace PhotoManager.Controllers
{
    [Route("api/[controller]")]
    public class AlbumsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAlbumRepository _repository;

        public AlbumsController(IAlbumRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet("[action]")]
        public ActionResult<AlbumViewModel> GetAllAlbums(int pageIndex, int pageSize)
        {
            var albums = _repository.Get(pageIndex * pageSize, pageSize).ToList();
            var albumViewModels = albums.Select(album => _mapper.Map<AlbumViewModel>(album));
            return Ok(albumViewModels);
        }

        [HttpPost("[action]")]
        [DisableRequestSizeLimit]
        
        public ActionResult RemoveAlbums([FromBody]IEnumerable<AlbumViewModel> albums)
        {
            _repository.RemoveMany(albums.Select(a => a.AlbumId));
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public ActionResult<AlbumViewModel> EditAlbum([FromBody]AlbumViewModel album)
        {
            var albumIn = _mapper.Map<Album>(album);

            var error = _repository.ValidateAlbum(albumIn);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            if (albumIn.Id == null)
            {
                _repository.Create(albumIn);
            }
            else
            {
                _repository.Update(album.AlbumId, albumIn);
            }

            return Ok();
        }
    }
}