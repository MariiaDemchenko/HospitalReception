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
    }
}