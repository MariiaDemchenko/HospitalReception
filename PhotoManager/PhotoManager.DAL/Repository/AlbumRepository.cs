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

        public CollectionModel<ThumbnailModel> GetAllAlbums(int pageIndex, int pageSize)
        {
            var skipCount = pageIndex * pageSize;

            var collectionModel = new CollectionModel<ThumbnailModel>
            {
                TotalCount = _context.Albums.Count()
            };

            if (collectionModel.TotalCount != 0)
            {
                collectionModel.Items = _context.Albums.OrderBy(a => a.Id).Skip(skipCount).Take(pageSize).Select(a =>
                    new ThumbnailModel
                    {
                        Id = a.Id,
                        Name = a.Name,
                        ImageId = a.Photos.FirstOrDefault().Images.FirstOrDefault(i => i.Size == Constants.ImageSize.Thumbnail).Id
                    }).ToList();
            }

            return collectionModel;
        }

        public int GetUserAlbumsCount(string userId)
        {
            return _context.Albums.Count(a => a.OwnerId == userId);
        }

        public AlbumIndexModel GetAlbumByModel(AlbumSearchModel model, int pageIndex, int pageSize, string userId, bool allPhotosWithSelectedState = false)
        {
            if (model.Id != null)
            {
                return GetAlbumById(model.Id, pageIndex, pageSize, userId, allPhotosWithSelectedState);
            }
            return GetAlbumByName(model.Name, userId, pageIndex, pageSize, allPhotosWithSelectedState);
        }

        private AlbumIndexModel GetAlbumByName(string name, string userId, int pageIndex, int pageSize, bool allPhotosWithSelectedState)
        {
            if (pageSize == 0)
            {
                return _context.Albums.Select(
                    a => new AlbumIndexModel
                    {
                        Id = a.Id,
                        OwnerId = a.OwnerId,
                        Name = a.Name,
                        Description = a.Description
                    }).FirstOrDefault(a => a.Name == name);
            }
            var skipCount = pageIndex * pageSize;
            if (!allPhotosWithSelectedState)
            {
                return _context.Albums.Select(
                    a => new AlbumIndexModel
                    {
                        Id = a.Id,
                        OwnerId = a.OwnerId,
                        Name = a.Name,
                        Description = a.Description,
                        Photos = new CollectionModel<PhotoThumbnailModel>
                        {
                            Items = a.Photos.OrderBy(p => p.Id).Skip(skipCount).Take(pageSize).
                                Select(p =>
                                    new PhotoThumbnailModel
                                    {
                                        Id = p.Id,
                                        Name = p.Name,
                                        CreationDate = p.CreationDate,
                                        ImageId = p.Images.FirstOrDefault(i => i.Size == Constants.ImageSize.Thumbnail).Id,
                                        Likes = p.Likes.Count(l => l.IsPositive && l.AlbumId == a.Id),
                                        Liked = p.Likes.Any(l => l.IsPositive && l.UserId == userId && l.AlbumId == a.Id),
                                        Dislikes = p.Likes.Count(l => !l.IsPositive && l.AlbumId == a.Id),
                                        Disliked = p.Likes.Any(l => !l.IsPositive && l.UserId == userId && l.AlbumId == a.Id)
                                    }),
                            TotalCount = a.Photos.Count()
                        }
                    }).FirstOrDefault(a => a.Name == name);
            }
            return _context.Albums.Select(a =>
                new AlbumIndexModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    OwnerId = string.Empty,
                    Photos = new CollectionModel<PhotoThumbnailModel>
                    {
                        Items = _context.Photos.OrderBy(p => p.Id).Skip(skipCount).Take(pageSize).Select(p =>
                          new PhotoThumbnailModel
                          {
                              Id = p.Id,
                              Name = p.Name,
                              CreationDate = p.CreationDate,
                              Selected = a.Photos.Select(photo => photo.Id).Contains(p.Id),
                              Likes = p.Likes.Count(l => l.IsPositive && l.AlbumId == a.Id),
                              Liked = p.Likes.Any(l => l.IsPositive && l.UserId == userId && l.AlbumId == a.Id),
                              Dislikes = p.Likes.Count(l => !l.IsPositive && l.AlbumId == a.Id),
                              Disliked = p.Likes.Any(l => !l.IsPositive && l.UserId == userId && l.AlbumId == a.Id),
                              ImageId = p.Images.FirstOrDefault(i => i.Size == Constants.ImageSize.Thumbnail).Id
                          }).ToList(),
                        TotalCount = _context.Photos.Count()
                    }
                }).FirstOrDefault(a => a.Name == name);
        }

        public AlbumIndexModel GetSelectedAlbumPhotosFromPageIndex(int? id, int pageIndex, int pageSize, string userId)
        {
            var skipCount = pageIndex * pageSize;
            return _context.Albums.Select(a =>
                new AlbumIndexModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    OwnerId = string.Empty,
                    Photos = new CollectionModel<PhotoThumbnailModel>
                    {
                        Items = _context.Photos.OrderBy(p => p.Id).Skip(skipCount)
                            .Select(p =>
                            new PhotoThumbnailModel
                            {
                                Id = p.Id,
                                Selected = a.Photos.Select(photo => photo.Id).Contains(p.Id)
                            }).Where(p => p.Selected).ToList(),
                        TotalCount = _context.Photos.Count()
                    }
                }).FirstOrDefault(a => a.Id == id);
        }

        public AlbumIndexModel GetAlbumById(int? id, int pageIndex, int pageSize, string userId, bool allPhotosWithSelectedState)
        {
            var skipCount = pageSize * pageIndex;
            if (pageSize == 0)
            {
                return _context.Albums.Select(
                    a => new AlbumIndexModel
                    {
                        Id = a.Id,
                        OwnerId = a.OwnerId,
                        Name = a.Name,
                        Description = a.Description
                    }).FirstOrDefault(a => a.Id == id);
            }
            if (!allPhotosWithSelectedState)
            {
                return _context.Albums.Select(
                    a => new AlbumIndexModel
                    {
                        Id = a.Id,
                        OwnerId = a.OwnerId,
                        Name = a.Name,
                        Description = a.Description,
                        Photos = new CollectionModel<PhotoThumbnailModel>()
                        {
                            Items = a.Photos.OrderBy(p => p.Id).Skip(skipCount).Take(pageSize).Select(p =>
                                    new PhotoThumbnailModel
                                    {
                                        Id = p.Id,
                                        Name = p.Name,
                                        CreationDate = p.CreationDate,
                                        ImageId = p.Images.FirstOrDefault(i => i.Size == Constants.ImageSize.Thumbnail).Id,
                                        Likes = p.Likes.Count(l => l.IsPositive && l.AlbumId == a.Id),
                                        Liked = p.Likes.Any(l => l.IsPositive && l.UserId == userId && l.AlbumId == a.Id),
                                        Dislikes = p.Likes.Count(l => !l.IsPositive && l.AlbumId == a.Id),
                                        Disliked = p.Likes.Any(l => !l.IsPositive && l.UserId == userId && l.AlbumId == a.Id)
                                    }),
                            TotalCount = a.Photos.Count
                        }
                    }).FirstOrDefault(a => a.Id == id);
            }
            return _context.Albums.Select(a =>
                new AlbumIndexModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    OwnerId = string.Empty,
                    Photos = new CollectionModel<PhotoThumbnailModel>
                    {
                        Items = _context.Photos.OrderBy(p => p.Id).Skip(skipCount).Take(pageSize)
                            .Select(p =>
                            new PhotoThumbnailModel
                            {
                                Id = p.Id,
                                Name = p.Name,
                                CreationDate = p.CreationDate,
                                Selected = a.Photos.Select(photo => photo.Id).Contains(p.Id),
                                Likes = p.Likes.Count(l => l.IsPositive && l.AlbumId == a.Id),
                                Liked = p.Likes.Any(l => l.IsPositive && l.UserId == userId && l.AlbumId == a.Id),
                                Dislikes = p.Likes.Count(l => !l.IsPositive && l.AlbumId == a.Id),
                                Disliked = p.Likes.Any(l => !l.IsPositive && l.UserId == userId && l.AlbumId == a.Id),
                                ImageId = p.Images.FirstOrDefault(i => i.Size == Constants.ImageSize.Thumbnail).Id
                            }).ToList(),
                        TotalCount = _context.Photos.Count()
                    }
                }).FirstOrDefault(a => a.Id == id);
        }

        public Album EditAlbum(AlbumIndexModel album)
        {
            var albumToEdit = _context.Albums.FirstOrDefault(a => a.Id == album.Id);

            albumToEdit.Name = album.Name;
            albumToEdit.Description = album.Description;

            var newPhotosId = album.Photos?.Items.Select(p => p.Id).ToList();
            var oldPhotosId = albumToEdit.Photos?.Select(p => p.Id).ToList();

            var state = EntityState.Unchanged;

            if (newPhotosId == null)
            {
                if (oldPhotosId != null)
                {
                    state = EntityState.Modified;
                }
            }
            else
            {
                if (oldPhotosId == null)
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
                    albumToEdit.Photos?.Clear();
                }
                else
                {
                    albumToEdit.Photos = _context.Photos.Where(p => newPhotosId.Contains(p.Id)).ToList();
                }
                _context.Entry(albumToEdit).State = EntityState.Modified;
            }

            return _context.Albums.FirstOrDefault(a => a.Id == album.Id);
        }

        public bool AddAlbum(AlbumIndexModel album)
        {
            var result = !_context.Albums.Any(a => a.Name == album.Name);
            var photoIds = album.Photos?.Items.Select(x => x.Id);

            var albumToAdd = new Album
            {
                Name = album.Name,
                OwnerId = album.OwnerId,
                Description = album.Description,
            };

            if (photoIds != null)
            {
                albumToAdd.Photos = _context.Photos.Where(x => photoIds.Contains(x.Id)).ToList();
            }

            _context.Albums.Add(albumToAdd);
            return result;
        }

        public IEnumerable<Album> DeleteAlbums(IEnumerable<int> albumsId)
        {
            _context.Albums.RemoveRange(_context.Albums.Where(a => albumsId.Contains(a.Id)));
            return _context.Albums.ToList();
        }
    }
}