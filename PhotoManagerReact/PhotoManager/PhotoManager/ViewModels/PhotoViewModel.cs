using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoManager.ViewModels
{
    public class PhotoViewModel
    {
        public string PhotoName { get; set; }
        public string ServerName { get; set; }
        public string PhotoUrl { get; set; }
        public string ThumbUrl { get; set; }
        public string IsSelected { get; set; }
        public string CameraModel { get; set; }
        public DateTime ShotDate { get; set; }
        public int LensFocalLength { get; set; }
        public string PhotoId { get; set; }
        public string Format { get; set; }
        public string AlbumId { get; set; }
        public string Owner { get; set; }
    }
}
