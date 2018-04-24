using System.Collections.Generic;

namespace PhotoManager.DAL.ProjectionModels
{
    public class AlbumIndexModel : ThumbnailModel
    {
        public string Description { get; set; }
        public string OwnerId { get; set; }
        public IEnumerable<PhotoThumbnailModel> Photos { get; set; }
    }
}