﻿using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace PhotoManager.DAL.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly IPhotoManagerDbContext _context;
        private readonly IAlbumRepository _albumRepository;

        public PhotoRepository(IPhotoManagerDbContext context, IAlbumRepository albumRepository)
        {
            _context = context;
            _albumRepository = albumRepository;
        }

        public Photo GetPhotoById(int id)
        {
            return _context.Photos.Include(p => p.CameraSettings).FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Photo> GetAllPhotos()
        {
            return _context.Photos.Include(p => p.CameraSettings);
        }

        public void EditPhoto(Photo photo)
        {
            var photoToModify = GetPhotoById(photo.Id);
            photoToModify.Name = photo.Name;
            photoToModify.CreationDate = photo.CreationDate;
            photoToModify.Place = photo.Place;

            _context.Entry(photoToModify).State = EntityState.Modified;
        }

        public void EditCameraSettings(CameraSettings cameraSettings)
        {
            _context.Entry(cameraSettings).State = EntityState.Modified;
        }

        public int AddCameraSettings(CameraSettings cameraSettings)
        {
            _context.CameraSettings.Add(cameraSettings);
            return cameraSettings.Id;
        }

        public IEnumerable<Photo> GetPhotosByKeyWord(string keyWord)
        {
            var param = new SqlParameter("@keyword", keyWord);
            var photos = _context.Database.SqlQuery<Photo>("EXEC dbo.Search @keyword", param).ToList();
            return photos;
        }

        public IEnumerable<Photo> GetPhotosBySearchModel(Photo photo)
        {
            return _context.Photos.Include(p => p.CameraSettings).Where(p =>
                (string.IsNullOrEmpty(photo.Name) || p.Name.Contains(photo.Name)) &&
                (photo.CreationDate == null || p.CreationDate == photo.CreationDate) &&
                (string.IsNullOrEmpty(photo.Place) || p.Place.Contains(photo.Place)) &&
                (string.IsNullOrEmpty(photo.CameraSettings.CameraModel) || p.CameraSettings.CameraModel.Contains(photo.CameraSettings.CameraModel)) &&
                (photo.CameraSettings.LensFocalLength == 0 || p.CameraSettings.LensFocalLength == photo.CameraSettings.LensFocalLength) &&
                (photo.CameraSettings.Diaphragm == 0 || p.CameraSettings.Diaphragm == photo.CameraSettings.Diaphragm) &&
                (photo.CameraSettings.ShutterSpeed == 0 || p.CameraSettings.ShutterSpeed == photo.CameraSettings.ShutterSpeed) &&
                (photo.CameraSettings.Iso == 0 || p.CameraSettings.Iso == photo.CameraSettings.Iso) &&
                (photo.CameraSettings.Flash == 0 || p.CameraSettings.Flash == photo.CameraSettings.Flash));
        }

        public Image GetImageById(int id)
        {
            return _context.Images.FirstOrDefault(i => i.Id == id);
        }

        public int AddImage(byte[] imageBytes)
        {
            var image = new Image { Bytes = imageBytes };
            _context.Images.Add(image);
            return image.Id;
        }

        public int AddPhoto(int? albumId, Photo photo)
        {
            var photoToAdd = new Photo
            {
                OwnerId = photo.OwnerId,
                CameraSettingsId = photo.CameraSettingsId,
                ImageId = photo.ImageId,
                Name = photo.Name,
                CreationDate = photo.CreationDate,
                Place = photo.Place
            };

            if (albumId != 0)
            {
                var album = _albumRepository.GetAlbumById(albumId);
                album.Photos.Add(photo);
            }
            else
            {
                _context.Photos.Add(photoToAdd);
            }

            return photo.Id;
        }

        public void DeletePhotos(IEnumerable<int> photosId)
        {
            var photosToDelete = _context.Photos.Where(p => photosId.Contains(p.Id));

            _context.Photos.RemoveRange(photosToDelete);
        }

        public IEnumerable<Photo> GetPhotosByAlbumId(int? albumId)
        {
            var photos = albumId != null ? _context.Albums.Include(a => a.Photos).FirstOrDefault(a => a.Id == albumId).Photos : _context.Photos.ToList();
            return photos;
        }
    }
}