using PhotoManager.Common;
using System.Collections.ObjectModel;

namespace PhotoManager.DAL.Models
{
    public class CameraSettings
    {
        public int Id { get; set; }
        public string CameraModel { get; set; }
        public int LensFocalLength { get; set; }
        public Constants.Diaphragm Diaphragm { get; set; }
        public Constants.ShutterSpeed ShutterSpeed { get; set; }
        public int Iso { get; set; }
        public Constants.Flash Flash { get; set; }

        public Collection<Photo> Photo { get; set; }
    }
}