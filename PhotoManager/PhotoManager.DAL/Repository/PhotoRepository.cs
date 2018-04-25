﻿using AutoMapper;
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

        public PhotoEditModel GetPhotoById(int id, Constants.ImageSize size, int albumId = 0)
        {
            var photos = _context.Photos.Select(p =>
                new
                {
                    p.CameraSettingsId,
                    AlbumId = albumId,
                    p.Id,
                    p.Name,
                    p.CreationDate,
                    p.Place,
                    p.CameraSettings.CameraModel,
                    p.CameraSettings.LensFocalLength,
                    p.CameraSettings.Diaphragm,
                    p.CameraSettings.ShutterSpeed,
                    p.CameraSettings.Iso,
                    p.CameraSettings.Flash,
                    ImageUrl = "/api/Image/" + p.Images.FirstOrDefault(i => i.Size == size).Id,
                    Selected = false
                }).ToList();
            return photos.Select(Mapper.Map<PhotoEditModel>).FirstOrDefault(p => p.Id == id);
        }

        public PhotoThumbnailModel GetPhotoById(int id, int albumId = 0)
        {
            return GetPhotoById(id, Constants.ImageSize.Thumbnail, albumId);
        }

        public IEnumerable<PhotoThumbnailModel> GetAllPhotos()
        {
            var photos = _context.Photos.Select(p =>
                new
                {
                    p.Id,
                    p.Name,
                    p.CreationDate,
                    ImageUrl = "/api/Image/" + p.Images.FirstOrDefault(i => i.Size == Constants.ImageSize.Thumbnail).Id,
                    Selected = false
                }).ToList();

            return photos.Select(Mapper.Map<PhotoThumbnailModel>).ToList();
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

        public IEnumerable<PhotoThumbnailModel> GetPhotosByKeyWord(string keyWord)
        {
            var param = new SqlParameter("@keyword", keyWord);
            var photosId = _context.Database.SqlQuery<int>("EXEC dbo.Search @keyword", param).ToList();

            var photos = _context.Photos.Select(p =>
                new
                {
                    p.Id,
                    p.Name,
                    p.CreationDate,
                    ImageUrl = "/api/Image/" + p.Images.FirstOrDefault(i => i.Size == Constants.ImageSize.Thumbnail).Id,
                    Selected = false
                }).ToList();

            return photos.Select(Mapper.Map<PhotoThumbnailModel>).Where(p => photosId.Contains(p.Id)).ToList();
        }

        public IEnumerable<PhotoThumbnailModel> GetPhotosBySearchModel(SearchModel photo)
        {
            var photoModels = _context.Photos.Select(p =>
                new
                {
                    p.Id,
                    p.Name,
                    p.Place,
                    p.CreationDate,
                    p.CameraSettings,
                    ImageUrl = "/api/Image/" + p.Images.FirstOrDefault(i => i.Size == Constants.ImageSize.Thumbnail).Id,
                    Selected = false
                }).Where(p =>
                (string.IsNullOrEmpty(photo.Name) || p.Name.Contains(photo.Name)) &&
                ((photo.CreationDateBegin == null && photo.CreationDateEnd == null) ||
                 (photo.CreationDateBegin == null && p.CreationDate <= photo.CreationDateEnd) ||
                 (photo.CreationDateEnd == null && p.CreationDate >= photo.CreationDateBegin) ||
                 p.CreationDate >= photo.CreationDateBegin && p.CreationDate <= photo.CreationDateEnd) &&
                (string.IsNullOrEmpty(photo.Place) || p.Place.Contains(photo.Place)) &&
                (string.IsNullOrEmpty(photo.CameraModel) || p.CameraSettings.CameraModel.Contains(photo.CameraModel)) &&
                (photo.LensFocalLength == 0 || p.CameraSettings.LensFocalLength == photo.LensFocalLength) &&
                ((photo.DiaphragmBegin == null && photo.DiaphragmEnd == null) ||
                 (photo.DiaphragmBegin == null && p.CameraSettings.Diaphragm <= photo.DiaphragmEnd) ||
                 (photo.DiaphragmEnd == null && p.CameraSettings.Diaphragm >= photo.DiaphragmBegin) ||
                 (p.CameraSettings.Diaphragm >= photo.DiaphragmBegin && p.CameraSettings.Diaphragm <= photo.DiaphragmEnd)) &&
                (photo.ShutterSpeed == 0 || p.CameraSettings.ShutterSpeed == photo.ShutterSpeed) &&
                (photo.Iso == 0 || p.CameraSettings.Iso == photo.Iso) &&
                ((photo.FlashBegin == null && photo.FlashEnd == null) ||
                 (photo.FlashBegin == null && p.CameraSettings.Flash <= photo.FlashEnd) ||
                 (photo.FlashEnd == null && p.CameraSettings.Flash >= photo.FlashBegin) ||
                 (p.CameraSettings.Flash >= photo.FlashBegin && p.CameraSettings.Flash <= photo.FlashEnd))).ToList();
            return photoModels.Select(Mapper.Map<PhotoThumbnailModel>).ToList();
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
            return albumId != null ? _context.Albums.Include(a => a.Photos.Select(p => p.Images)).FirstOrDefault(a => a.Id == albumId)?.Photos :
                _context.Photos.Include(p => p.Images).ToList();
        }
    }
}