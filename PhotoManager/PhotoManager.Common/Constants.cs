namespace PhotoManager.Common
{
    public static class Constants
    {
        public enum ImageSize
        {
            Small,
            Original,
            Large
        }

        private const string _templatesFolder = "/Content/Templates/";

        public const string Root = "~/App_Data";
        public const string AlbumsTemplate = _templatesFolder + "albumsTemplate.html";
        public const string PhotoAlbumTemplate = _templatesFolder + "photoAlbumTemplate.html";
        public const string PhotoSearchTemplate = _templatesFolder + "photoAlbumTemplate.html";
        public const string PhotoEditTemplate = _templatesFolder + "photoEditTemplate.html";
        public const string PhotoTemplate = _templatesFolder + "photoTemplate.html";
        public const string PhotoPropertiesTemplate = _templatesFolder + "photoPropertiesTemplate.html";
        public const string AdvancedSearchTemplate = _templatesFolder + "advancedSearchTemplate.html";
    }
}