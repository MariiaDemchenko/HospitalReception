using PhotoManager.Common;
using PhotoManager.DAL.Models;
using PhotoManager.DAL.ProjectionModels;
using System.Collections.Generic;

namespace PhotoManager.DAL.Contracts
{
    public interface IPhotoRepository
    {
        CollectionModel<PhotoThumbnailModel> GetAllPhotos(int pageIndex, int pageSize);

        int GetUserPhotosCount(string userId);

        PhotoThumbnailModel GetPhotoById(int id, int albumId = 0);

        PhotoAddModel GetPhotoById(int id, Common.Constants.ImageSize size, int albumId = 0);

        CollectionModel<PhotoThumbnailModel> GetPhotosByKeyWord(string keyWord, int pageIndex, int pageSize);

        CollectionModel<PhotoThumbnailModel> GetPhotosBySearchModel(SearchModel photo, int pageIndex, int pageSize);

        Image GetImageById(int id);

        void EditPhoto(PhotoEditModel photo);

        int AddPhoto(PhotoAddModel photoModel);

        void DeletePhotos(IEnumerable<int> photosId);

        LikesModel AddLike(string userId, int photoId, int albumId, bool isPositive);
    }
}