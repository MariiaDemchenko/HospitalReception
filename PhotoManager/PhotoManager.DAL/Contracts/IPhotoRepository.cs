using PhotoManager.DAL.Models;
using System.Collections.Generic;

namespace PhotoManager.DAL.Contracts
{
    public interface IPhotoRepository
    {
        IEnumerable<Photo> GetAllPhotos();

        Photo GetPhotoById(int id);

        IEnumerable<Photo> GetPhotosByKeyWord(string keyWord);

        IEnumerable<Photo> GetPhotosBySearchModel(Photo photo);

        Image GetImageById(int id);

        void EditCameraSettings(CameraSettings cameraSettings);

        void EditPhoto(Photo photo);

        int AddImage(byte[] imageBytes);

        int AddCameraSettings(CameraSettings cameraSettings);

        int AddPhoto(int? albumId, Photo photo);

        void DeletePhotos(IEnumerable<int> photosId);

        IEnumerable<Photo> GetPhotosByAlbumId(int? albumId);
    }
}