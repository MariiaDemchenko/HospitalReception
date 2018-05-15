namespace Microsoft.ServiceModel.ImageViewer
{
    public static class Constants
    {
        public enum ImageSize
        {
            Small,
            Original
        }

        public const int BufferLength = 64;
        public const int MaxRecievedMessageSize = 1025024;
        public const int SmallPhotoSize = 250;
        public const string ImageFolderPath = "..\\..\\Images";
        public const string FileExtension = "jpg";
    }
}