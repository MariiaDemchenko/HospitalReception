using PhotoManager.DAL.Models;
using PhotoManager.DAL.ProjectionModels;
using System.Collections.Generic;

namespace PhotoManager.DAL.Contracts
{
    public interface IPhotoRepository
    {
        IEnumerable<PhotoThumbnailModel> GetAllPhotos();

        int GetUserPhotosCount(string userId);

        PhotoThumbnailModel GetPhotoById(int id, int albumId = 0);

        PhotoEditModel GetPhotoById(int id, Common.Constants.ImageSize size, int albumId = 0);

        IEnumerable<PhotoThumbnailModel> GetPhotosByKeyWord(string keyWord);

        IEnumerable<PhotoThumbnailModel> GetPhotosBySearchModel(SearchModel photo);

        Image GetImageById(int id);

        void EditPhoto(PhotoEditModel photo);

        int AddPhoto(PhotoAddModel photoModel);

        void DeletePhotos(IEnumerable<int> photosId);

        IEnumerable<Photo> GetPhotosByAlbumId(int? albumId);
    }
}