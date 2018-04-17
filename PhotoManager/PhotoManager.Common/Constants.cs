﻿namespace PhotoManager.Common
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
        public const string AlbumsTemplate = _templatesFolder + "Home/Index.html";
        public const string PhotoAlbumTemplate = _templatesFolder + "Album/Index.html";
        public const string PhotoSearchTemplate = _templatesFolder + "Gallery/Index.html";
        public const string PhotoEditTemplate = _templatesFolder + "Photo/Edit.html";
        public const string PhotoTemplate = _templatesFolder + "Photo/Index.html";
        public const string PhotoPropertiesTemplate = _templatesFolder + "Photo/Properties.html";
        public const string AdvancedSearchTemplate = _templatesFolder + "Gallery/AdvancedSearch.html";
    }
}