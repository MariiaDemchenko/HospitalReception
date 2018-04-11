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

        public const string Root = "~/App_Data";
        public const string AlbumsTemplate = "/Content/Templates/albumsTemplate.html";
        public const string PhotoAlbumTemplate = "/Content/Templates/photoAlbumTemplate.html";
        public const string PhotoEditTemplate = "/Content/Templates/photoEditTemplate.html";
        public const string PhotoTemplate = "/Content/Templates/photoTemplate.html";
    }
}