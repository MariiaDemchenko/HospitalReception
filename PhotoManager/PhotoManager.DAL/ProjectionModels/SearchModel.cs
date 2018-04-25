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
        public int LensFocalLength { get; set; }
        [DisplayName("Diaphragm")]
        public double? DiaphragmBegin { get; set; }
        public double? DiaphragmEnd { get; set; }
        [DisplayName("Shutter speed")]
        public int ShutterSpeed { get; set; }
        public int Iso { get; set; }
        [DisplayName("Flash")]
        public double? FlashBegin { get; set; }
        public double? FlashEnd { get; set; }
    }
}