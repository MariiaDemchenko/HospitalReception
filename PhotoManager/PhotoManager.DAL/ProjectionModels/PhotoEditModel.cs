using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhotoManager.DAL.ProjectionModels
{
    public class PhotoEditModel : PhotoThumbnailModel
    {
        public int CameraSettingsId { get; set; }
        public int? AlbumId { get; set; }
        public string Place { get; set; }
        public string CameraModel { get; set; }

        [DisplayName("Lens focal length")]
        [Range(0, Common.Constants.MaxLensFocalLength)]
        public int LensFocalLength { get; set; }

        [DisplayName("Diaphragm")]
        [Range(0, Common.Constants.MaxDiaphragm)]
        public double Diaphragm { get; set; }

        [DisplayName("Shutter speed")]
        [Range(0, Common.Constants.MaxShutterSpeed)]
        public int ShutterSpeed { get; set; }

        [DisplayName("ISO")]
        [Range(0, Common.Constants.MaxIso)]
        public int Iso { get; set; }

        [DisplayName("Flash")]
        [Range(0, Common.Constants.MaxFlash)]
        public double Flash { get; set; }
    }
}