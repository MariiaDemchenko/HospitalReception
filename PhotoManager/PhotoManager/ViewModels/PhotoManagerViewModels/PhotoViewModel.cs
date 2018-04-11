using System;

namespace PhotoManager.ViewModels.PhotoManagerViewModels
{
    public class PhotoViewModel
    {
        public int? Id { get; set; }
        public string OwnerId { get; set; }

        public int? CameraSettingsId { get; set; }
        public string CameraModel { get; set; }
        public int? LensFocalLength { get; set; }
        public double? Diaphragm { get; set; }
        public int? ShutterSpeed { get; set; }
        public int? Iso { get; set; }
        public double? Flash { get; set; }

        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Place { get; set; }
        public int? Likes { get; set; }
        public int? Dislikes { get; set; }

        public int? AlbumId { get; set; }
        public byte[] Image { get; set; }
    }
}