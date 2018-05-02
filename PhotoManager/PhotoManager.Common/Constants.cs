namespace PhotoManager.Common
{
    public static class Constants
    {
        public enum ImageSize
        {
            Thumbnail,
            Medium,
            Original
        }

        private const string _templatesFolder = "/Content/Templates/";

        public const string Root = "~/App_Data";
        public const string PhotoTemplate = _templatesFolder + "Photo/Index.html";
        public const string PhotoPropertiesTemplate = _templatesFolder + "Photo/Properties.html";

        public const int FreePhotosCount = 3;
        public const int FreeAlbumsCount = 3;

        public const int MaxLensFocalLength = 5;
        public const int MaxDiaphragm = 1;
        public const int MaxShutterSpeed = 15;
        public const int MaxIso = 40;
        public const int MaxFlash = 1;
    }
}