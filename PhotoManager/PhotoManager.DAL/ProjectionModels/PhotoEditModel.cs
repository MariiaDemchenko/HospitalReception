using PhotoManager.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhotoManager.DAL.ProjectionModels
{
    public class PhotoEditModel : PhotoThumbnailModel
    {
        public int CameraSettingsId { get; set; }
        public int AlbumId { get; set; }
        public string Place { get; set; }
        [DisplayName("Camera Model")]
        public string CameraModel { get; set; }

        [DisplayName("Lens focal length")]
        [Range(1, Constants.MaxLensFocalLength)]
        public int LensFocalLength { get; set; }

        [DisplayName("Diaphragm")]
        public Constants.Diaphragm Diaphragm { get; set; }

        [DisplayName("Shutter speed")]
        public Constants.ShutterSpeed ShutterSpeed { get; set; }

        [DisplayName("ISO")]
        [Range(1, Constants.MaxIso)]
        public int Iso { get; set; }

        [DisplayName("Flash")]
        public Constants.Flash Flash { get; set; }
    }
}