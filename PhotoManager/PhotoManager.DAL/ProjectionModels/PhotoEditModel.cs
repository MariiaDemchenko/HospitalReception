namespace PhotoManager.DAL.ProjectionModels
{
    public class PhotoEditModel : PhotoThumbnailModel
    {
        public int CameraSettingsId { get; set; }
        public int AlbumId { get; set; }
        public string Place { get; set; }
        public string CameraModel { get; set; }
        public int LensFocalLength { get; set; }
        public double Diaphragm { get; set; }
        public int ShutterSpeed { get; set; }
        public int Iso { get; set; }
        public double Flash { get; set; }
    }
}