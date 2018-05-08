using AutoMapper;
using PhotoManager.Common;
using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Models;
using PhotoManager.DAL.ProjectionModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace PhotoManager.DAL.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly IPhotoManagerDbContext _context;

        public PhotoRepository(IPhotoManagerDbContext context)
        {
            _context = context;
        }

        public PhotoAddModel GetPhotoById(int id, Constants.ImageSize size, int albumId = 0)
        {
            var photo = _context.Photos.Select(p =>
                new PhotoAddModel
                {
                    CameraSettingsId = p.CameraSettingsId,
                    AlbumId = albumId,
                    Id = p.Id,
                    Name = p.Name,
                    CreationDate = p.CreationDate,
                    Place = p.Place,
                    CameraModel = p.CameraSettings.CameraModel,
                    LensFocalLength = p.CameraSettings.LensFocalLength,
                    Diaphragm = p.CameraSettings.Diaphragm,
                    ShutterSpeed = p.CameraSettings.ShutterSpeed,
                    Iso = p.CameraSettings.Iso,
                    Flash = p.CameraSettings.Flash,
                    ImageId = p.Images.FirstOrDefault(i => i.Size == size).Id,
                    Selected = false,
                    Likes = p.Likes.Count(l => l.IsPositive),
                    Liked = false,
                    Dislikes = p.Likes.Count(l => !l.IsPositive),
                    Disliked = false
                }).FirstOrDefault(p => p.Id == id);
            return photo;
        }

        public PhotoThumbnailModel GetPhotoById(int id, int albumId = 0)
        {
            return GetPhotoById(id, Constants.ImageSize.Thumbnail, albumId);
        }

        public CollectionModel<PhotoThumbnailModel> GetAllPhotos(int pageIndex, int pageSize)
        {
            var skipCount = pageIndex * pageSize;
            var photos = new CollectionModel<PhotoThumbnailModel>
            {
                Items = _context.Photos.OrderBy(p => p.Id).Skip(skipCount).Take(pageSize).Select(p =>
                    new PhotoThumbnailModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        CreationDate = p.CreationDate,
                        ImageId = p.Images.FirstOrDefault(i => i.Size == Constants.ImageSize.Thumbnail).Id,
                        Selected = false,
                        Likes = p.Likes.Count(l => l.IsPositive),
                        Liked = false,
                        Dislikes = p.Likes.Count(l => !l.IsPositive),
                        Disliked = false
                    }).ToList(),
                TotalCount = _context.Photos.Count()
            };

            return photos;
        }

        public int GetUserPhotosCount(string userId)
        {
            return _context.Photos.Count(p => p.OwnerId == userId);
        }

        public void EditPhoto(PhotoEditModel photo)
        {
            var cameraSettings = Mapper.Map<CameraSettings>(photo);
            _context.Entry(cameraSettings).State = EntityState.Modified;

            var photoToEdit = _context.Photos.FirstOrDefault(p => p.Id == photo.Id);
            photoToEdit.Name = photo.Name;
            photoToEdit.CreationDate = photo.CreationDate;
            photoToEdit.Place = photo.Place;
            _context.Entry(photoToEdit).State = EntityState.Modified;
        }

        public CollectionModel<PhotoThumbnailModel> GetPhotosByKeyWord(string keyWord, int pageIndex, int pageSize)
        {
            var param = new SqlParameter("@keyword", keyWord);
            var photosId = _context.Database.SqlQuery<int>("EXEC dbo.Search @keyword", param).ToList();
            return new CollectionModel<PhotoThumbnailModel>
            {
                Items = _context.Photos.Where(p => photosId.Contains(p.Id)).OrderBy(p => p.Id)
                    .Skip(pageIndex * pageSize).Take(pageSize).Select(p =>
                        new PhotoThumbnailModel
                        {
                            Id = p.Id,
                            Name = p.Name,
                            CreationDate = p.CreationDate,
                            ImageId = p.Images.FirstOrDefault(i => i.Size == Constants.ImageSize.Thumbnail).Id,
                            Selected = false,
                            Likes = p.Likes.Count(l => l.IsPositive),
                            Liked = false,
                            Dislikes = p.Likes.Count(l => !l.IsPositive),
                            Disliked = false,
                        }).ToList(),
                TotalCount = photosId.Count
            };
        }

        public CollectionModel<PhotoThumbnailModel> GetPhotosBySearchModel(SearchModel photo, int pageIndex, int pageSize)
        {
            IQueryable<Photo> query = _context.Photos;
            if (!string.IsNullOrEmpty(photo.Name))
            {
                query = query.Where(p => p.Name.Contains(photo.Name));
            }

            if (!string.IsNullOrEmpty(photo.Place))
            {
                query = query.Where(p => p.Name.Contains(photo.Place));
            }

            if (!string.IsNullOrEmpty(photo.CameraModel))
            {
                query = query.Where(p => p.Name.Contains(photo.CameraModel));
            }

            if (photo.ShutterSpeed != 0)
            {
                query = query.Where(p => p.CameraSettings.ShutterSpeed == photo.ShutterSpeed);
            }

            if (photo.Diaphragm != 0)
            {
                query = query.Where(p => p.CameraSettings.Diaphragm == photo.Diaphragm);
            }

            if (photo.Flash != 0)
            {
                query = query.Where(p => p.CameraSettings.Flash == photo.Flash);
            }

            if (!(photo.CreationDateBegin == null && photo.CreationDateEnd == null))
            {
                if (photo.CreationDateBegin == null)
                {
                    query = query.Where(p => p.CreationDate.Value <= photo.CreationDateEnd.Value);
                }
                else if (photo.CreationDateEnd == null)
                {
                    query = query.Where(p => p.CreationDate.Value >= photo.CreationDateBegin.Value);
                }
                else
                {
                    query = query.Where(p => p.CreationDate.Value <= photo.CreationDateEnd.Value &&
                                             p.CreationDate.Value >= photo.CreationDateBegin.Value);
                }
            }

            if (!(photo.LensFocalLengthBegin == null && photo.LensFocalLengthEnd == null))
            {
                if (photo.LensFocalLengthBegin == null)
                {
                    query = query.Where(p => p.CameraSettings.LensFocalLength <= photo.LensFocalLengthEnd);
                }
                else if (photo.LensFocalLengthEnd == null)
                {
                    query = query.Where(p => p.CameraSettings.LensFocalLength >= photo.LensFocalLengthBegin);
                }
                else
                {
                    query = query.Where(p => p.CameraSettings.LensFocalLength >= photo.LensFocalLengthBegin && p.CameraSettings.LensFocalLength <= photo.LensFocalLengthEnd);
                }
            }

            if (!(photo.IsoBegin == null && photo.IsoEnd == null))
            {
                if (photo.IsoBegin == null)
                {
                    query = query.Where(p => p.CameraSettings.Iso <= photo.IsoEnd);
                }
                else if (photo.IsoEnd == null)
                {
                    query = query.Where(p => p.CameraSettings.Iso >= photo.IsoBegin);
                }
                else
                {
                    query = query.Where(p => p.CameraSettings.Iso >= photo.IsoBegin && p.CameraSettings.Iso <= photo.IsoEnd);
                }
            }

            var photoModels = new CollectionModel<PhotoThumbnailModel>()
            {
                Items = query.OrderBy(p => p.Id).Skip(pageIndex * pageSize).Take(pageSize).Select(p =>
                    new PhotoThumbnailModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        CreationDate = p.CreationDate,
                        ImageId = p.Images.FirstOrDefault(i => i.Size == Constants.ImageSize.Thumbnail).Id,
                        Selected = false,
                        Likes = p.Likes.Count(l => l.IsPositive),
                        Liked = false,
                        Dislikes = p.Likes.Count(l => !l.IsPositive),
                        Disliked = false
                    }).ToList(),
                TotalCount = query.Count()
            };
            return photoModels;
        }

        public Image GetImageById(int id)
        {
            return _context.Images.FirstOrDefault(i => i.Id == id);
        }

        public int AddPhoto(PhotoAddModel photoModel)
        {
            var cameraSettings = Mapper.Map<CameraSettings>(photoModel);
            _context.CameraSettings.Add(cameraSettings);

            var photo = new Photo
            {
                OwnerId = photoModel.OwnerId,
                CameraSettingsId = photoModel.CameraSettingsId,
                Name = photoModel.Name,
                CreationDate = photoModel.CreationDate,
                Place = photoModel.Place,
                Images = photoModel.Images
            };

            if (photoModel.AlbumId != 0)
            {
                var album = _context.Albums.FirstOrDefault(a => a.Id == photoModel.AlbumId);

                album.Photos.Add(photo);
            }
            else
            {
                _context.Photos.Add(photo);
            }

            return photo.Id;
        }

        public void DeletePhotos(IEnumerable<int> photosId)
        {
            var photosToDelete = _context.Photos.Include(p => p.Images).Where(p => photosId.Contains(p.Id));
            _context.Photos.RemoveRange(photosToDelete);
        }

        public IEnumerable<Photo> GetPhotosByAlbumId(int? albumId)
        {
            return albumId != null ?
                _context.Albums.Include(a => a.Photos.Select(p => p.Images)).FirstOrDefault(a => a.Id == albumId)?.Photos :
                _context.Photos.Include(p => p.Images).ToList();
        }

        public LikesModel AddLike(string userId, int photoId, int albumId, bool isPositive)
        {
            var likesCount = _context.Likes.Count(l => l.PhotoId == photoId && l.AlbumId == albumId && l.IsPositive == isPositive);
            var dislikesCount = _context.Likes.Count(l => l.PhotoId == photoId && l.AlbumId == albumId && l.IsPositive == !isPositive);

            if (_context.Albums.FirstOrDefault(a => a.Id == albumId)?.OwnerId == userId)
            {
                return GetLikesModel(likesCount, dislikesCount, isPositive);
            }

            var existingLike = _context.Likes.FirstOrDefault(l => l.PhotoId == photoId && l.UserId == userId && l.AlbumId == albumId && l.IsPositive == isPositive);
            if (existingLike != null)
            {
                _context.Likes.Remove(existingLike);
                likesCount--;
            }
            else
            {
                var existingDislike = _context.Likes.FirstOrDefault(l => l.PhotoId == photoId && l.UserId == userId && l.AlbumId == albumId && l.IsPositive == !isPositive);
                if (existingDislike != null)
                {
                    _context.Likes.Remove(existingDislike);
                    dislikesCount--;
                }
                _context.Likes.Add(new Like { PhotoId = photoId, UserId = userId, AlbumId = albumId, IsPositive = isPositive });
                likesCount++;
            }
            return GetLikesModel(likesCount, dislikesCount, isPositive);
        }

        private LikesModel GetLikesModel(int likes, int dislikes, bool isPositive)
        {
            var likesModel = new LikesModel();
            if (isPositive)
            {
                likesModel.LikesCount = likes;
                likesModel.DislikesCount = dislikes;
            }
            else
            {
                likesModel.DislikesCount = likes;
                likesModel.LikesCount = dislikes;
            }

            return likesModel;
        }
    }
}