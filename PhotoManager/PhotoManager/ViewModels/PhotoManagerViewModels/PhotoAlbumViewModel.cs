using PhotoManager.Common;
using PhotoManager.DAL.ProjectionModels;

namespace PhotoManager.ViewModels.PhotoManagerViewModels
{
    public class PhotoAlbumViewModel : AlbumIndexModel
    {
        public new CollectionModel<PhotoThumbnailModel> Photos { get; set; }
    }
}