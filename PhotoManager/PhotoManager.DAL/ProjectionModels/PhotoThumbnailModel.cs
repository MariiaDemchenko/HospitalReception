using System;

namespace PhotoManager.DAL.ProjectionModels
{
    public class PhotoThumbnailModel : ThumbnailModel
    {
        public DateTime? CreationDate { get; set; }
        public bool Selected { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public bool Liked { get; set; }
        public bool Disliked { get; set; }
    }
}