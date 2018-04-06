﻿using System;
using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Headers;

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
            _context.SaveChanges();
        }

        public void EditCameraSettings(CameraSettings cameraSettings)
        {
            _context.Entry(cameraSettings).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public int AddCameraSettings(CameraSettings cameraSettings)
        {
            _context.CameraSettings.Add(cameraSettings);
            _context.SaveChanges();
            return cameraSettings.Id;
        }

        public IEnumerable<Photo> GetPhotosByKeyWord(string keyWord)
        {
            return string.IsNullOrEmpty(keyWord) ?
                _context.Photos.ToList() : _context.Photos.Where(p => p.Name.Contains(keyWord)).ToList();
        }

        public Image GetImageById(int id)
        {
            return _context.Images.FirstOrDefault(i => i.Id == id);
        }

        public int AddImage(byte[] imageBytes)
        {
            var image = new Image { Bytes = imageBytes };
            _context.Images.Add(image);
            _context.SaveChanges();
            return image.Id;
        }

        public int AddPhoto(int albumId, Photo photo)
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

            _context.Photos.Add(photoToAdd);

            if (albumId != 0)
            {
                var album = _albumRepository.GetAlbumById(albumId);
                album.Photos.Add(photo);
            }

            _context.SaveChanges();
            return photo.Id;
        }
    }
}