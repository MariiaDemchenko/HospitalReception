using System;
using System.Collections.Generic;
using PhotoManager.DAL.Models;

namespace PhotoManager.DAL.Contracts
{
    public interface IPhotoRepository
    {
        IEnumerable<Photo> GetAllPhotos();

        Photo GetPhotoById(int id);

        IEnumerable<Photo> GetPhotosByKeyWord(string keyWord);

        Image GetImageById(int id);

        void EditCameraSettings(CameraSettings cameraSettings);

        void EditPhoto(Photo photo);

        int AddImage(byte[] imageBytes);

        int AddCameraSettings(CameraSettings cameraSettings);

        int AddPhoto(int albumId, Photo photo);
    }
}