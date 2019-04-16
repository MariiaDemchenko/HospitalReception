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
        private readonly IAlbumRepository _albumRepository;
        private readonly IPhotoRepository _photoRepository;

        public AlbumsController(IAlbumRepository albumRepository, IPhotoRepository photoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _albumRepository = albumRepository;
            _photoRepository = photoRepository;
        }

        [HttpGet("[action]")]
        public ActionResult<AlbumViewModel> GetAllAlbums(int pageIndex, int pageSize)
        {
            var albums = _albumRepository.Get(pageIndex * pageSize, pageSize).ToList();
            var albumViewModels = albums.Select(album => _mapper.Map<AlbumViewModel>(album));
            return Ok(albumViewModels);
        }

        [HttpPost("[action]")]
        [Authorize]
        public ActionResult<AlbumViewModel> AddAlbum([FromBody]AlbumViewModel album)
        {
            var albumIn = _mapper.Map<Album>(album);

            var error = ValidateAlbum(albumIn);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            if (albumIn.Photos?.Count > 0)
            {
                albumIn.Cover = GetCover(albumIn.Photos);
            }
            _albumRepository.Create(albumIn);
            return Ok();
        }

        [HttpPut("[action]")]
        [Authorize]
        public ActionResult<AlbumViewModel> EditAlbum([FromBody]AlbumViewModel album)
        {
            var albumIn = _mapper.Map<Album>(album);

            var error = ValidateAlbum(albumIn);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            if (albumIn.Photos?.Count > 0)
            {
                albumIn.Cover = GetCover(albumIn.Photos);
            }
            _albumRepository.Update(album.AlbumId, albumIn);
            return Ok();
        }

        [HttpDelete("[action]")]
        public ActionResult RemoveAlbum([FromBody]AlbumViewModel album)
        {
            _albumRepository.Remove(album.AlbumId);
            return Ok();
        }

        #region utils
        private string GetCover(List<string> photos)
        {
            var coverPhoto = _photoRepository.Get(photos.FirstOrDefault());
            return $"{coverPhoto?.ServerName}.{coverPhoto?.Format}";
        }

        private string ValidateAlbum(Album albumIn)
        {
            var validationError = string.Empty;
            if (_albumRepository.Get(0).Any(album => album.AlbumName == albumIn.AlbumName && album.Id != albumIn.Id))
            {
                validationError = "Album name must be unique";
            }
            return validationError;
        }
        #endregion
    }
}