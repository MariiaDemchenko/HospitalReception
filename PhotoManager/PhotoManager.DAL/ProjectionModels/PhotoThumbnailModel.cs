using System;

namespace PhotoManager.DAL.ProjectionModels
{
    public class PhotoThumbnailModel : ThumbnailModel
    {
        public DateTime? CreationDate { get; set; }
        public bool Selected { get; set; }
    }
}