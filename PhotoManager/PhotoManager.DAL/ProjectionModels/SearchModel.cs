using System;
using System.ComponentModel;

namespace PhotoManager.DAL.ProjectionModels
{
    public class SearchModel
    {
        public string Name { get; set; }
        [DisplayName("Creation date")]
        public DateTime? CreationDateBegin { get; set; }
        public DateTime? CreationDateEnd { get; set; }
        public string Place { get; set; }
        [DisplayName("Camera model")]
        public string CameraModel { get; set; }
        [DisplayName("Lens focal length")]
        public int? LensFocalLengthBegin { get; set; }
        public int? LensFocalLengthEnd { get; set; }
        [DisplayName("Diaphragm")]
        public Common.Constants.Diaphragm Diaphragm { get; set; }
        [DisplayName("Shutter speed")]
        public Common.Constants.ShutterSpeed ShutterSpeed { get; set; }
        [DisplayName("ISO")]
        public int? IsoBegin { get; set; }
        public int? IsoEnd { get; set; }
        [DisplayName("Flash")]
        public Common.Constants.Flash Flash { get; set; }
    }
}