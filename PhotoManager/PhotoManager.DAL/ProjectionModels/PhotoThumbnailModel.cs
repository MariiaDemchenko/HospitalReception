using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhotoManager.DAL.ProjectionModels
{
    public class PhotoThumbnailModel : ThumbnailModel
    {
        [DisplayName("Shoot date")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}")]
        public DateTime? CreationDate { get; set; }
        public bool Selected { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public bool Liked { get; set; }
        public bool Disliked { get; set; }
    }
}