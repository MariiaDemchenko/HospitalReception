using AutoMapper;
using PhotoManager.Common;
using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Models;
using PhotoManager.DAL.ProjectionModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PhotoManager.DAL.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly IPhotoManagerDbContext _context;

        public AlbumRepository(IPhotoManagerDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ThumbnailModel> GetAllAlbums()
        {
            var albums = _context.Albums.Select(a =>
                new
                {
                    a.Id,
                    a.Name,
                    ImageUrl = "/api/Image/" + a.Photos.FirstOrDefault().Images.FirstOrDefault(i => i.Size == Constants.ImageSize.Thumbnail).Id
                }).ToList();

            return albums.Select(Mapper.Map<ThumbnailModel>).ToList();
        }

        public int GetUserAlbumsCount(string userId)
        {
            return _context.Albums.Count(a => a.OwnerId == userId);
        }

        public AlbumIndexModel GetAlbumById(int? id, bool allPhotosWithSelectedState)
        {
            AlbumIndexModel album;

            if (!allPhotosWithSelectedState)
            {
                var albums = _context.Albums.Select(a =>
                    new
                    {
                        a.Id,
                        a.OwnerId,
                        a.Name,
                        a.Description,
                        ImageUrl = string.Empty,
                        Photos = a.Photos.Select(p =>
                            new PhotoThumbnailModel
                            {
                                Id = p.Id,
                                Name = p.Name,
                                CreationDate = p.CreationDate,
                                ImageUrl = "/api/Image/" + p.Images.FirstOrDefault(i => i.Size == Constants.ImageSize.Thumbnail).Id
                            })
                    }).ToList();

                album = albums.Select(Mapper.Map<AlbumIndexModel>).FirstOrDefault(a => a.Id == id);
            }
            else
            {
                var albums = _context.Albums.Select(a =>
                    new
                    {
                        a.Id,
                        a.Name,
                        a.Description,
                        ImageUrl = string.Empty,
                        OwnerId = string.Empty,
                        Photos = _context.Photos.Select(p =>
                            new PhotoThumbnailModel
                            {
                                Id = p.Id,
                                Name = p.Name,
                                CreationDate = p.CreationDate,
                                Selected = a.Photos.Select(photo => photo.Id).Contains(p.Id),
                                ImageUrl = "/api/Image/" +
                                           p.Images.FirstOrDefault(i => i.Size == Constants.ImageSize.Thumbnail).Id
                            })
                    }).ToList();
                album = albums.Select(Mapper.Map<AlbumIndexModel>).FirstOrDefault(a => a.Id == id);
            }

            return album;
        }

        public Album EditAlbum(AlbumIndexModel album)
        {
            var albumToEdit = _context.Albums.FirstOrDefault(a => a.Id == album.Id);

            albumToEdit.Name = album.Name;
            albumToEdit.Description = album.Description;

            var newPhotosId = album.Photos?.Select(p => p.Id).ToList();
            var oldPhotosId = albumToEdit.Photos?.Select(p => p.Id).ToList();

            var state = EntityState.Unchanged;

            if (newPhotosId == null)
            {
                if (oldPhotosId.Count != 0)
                {
                    state = EntityState.Modified;
                }
            }
            else
            {
                if (oldPhotosId.Count == 0)
                {
                    albumToEdit.Photos = new List<Photo>();
                    state = EntityState.Modified;
                }
                else
                {
                    foreach (var id in newPhotosId)
                    {
                        if (!oldPhotosId.Contains(id))
                        {
                            state = EntityState.Modified;
                        }
                    }
                }
            }

            if (state == EntityState.Modified)
            {
                if (newPhotosId == null)
                {
                    albumToEdit.Photos.Clear();
                }
                else
                {
                    albumToEdit.Photos = _context.Photos.Where(p => newPhotosId.Contains(p.Id)).ToList();
                }
                _context.Entry(albumToEdit).State = EntityState.Modified;
            }

            return _context.Albums.FirstOrDefault(a => a.Id == album.Id);
        }

        public Album AddAlbum(AlbumIndexModel album)
        {
            var photosId = album.Photos?.Select(p => p.Id).ToList();
            var albumToAdd = new Album
            {
                Name = album.Name,
                OwnerId = album.OwnerId,
                Description = album.Description,
                Photos = photosId != null ? _context.Photos.Where(p => photosId.Contains(p.Id)).ToList() : null
            };
            _context.Albums.Add(albumToAdd);
            return _context.Albums.FirstOrDefault(a => a.Id == album.Id);
        }

        public IEnumerable<Album> DeleteAlbums(IEnumerable<int> albumsId)
        {
            _context.Albums.RemoveRange(_context.Albums.Where(a => albumsId.Contains(a.Id)));
            return _context.Albums.ToList();
        }
    }
}