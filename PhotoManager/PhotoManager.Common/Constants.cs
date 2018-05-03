using System.ComponentModel.DataAnnotations;

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

        public enum Flash
        {
            [Display(Name = "1/1")]
            F8 = 8,
            [Display(Name = "1/2")]
            F7 = 7,
            [Display(Name = "1/4")]
            F6 = 6,
            [Display(Name = "1/8")]
            F5 = 5,
            [Display(Name = "1/16")]
            F4 = 4,
            [Display(Name = "1/32")]
            F3 = 3,
            [Display(Name = "1/64")]
            F2 = 2,
            [Display(Name = "1/128")]
            F1 = 1,
            [Display(Name = "Not stated")]
            F0 = -1
        }

        public enum Diaphragm
        {
            [Display(Name = "f/1.0")]
            D10 = 10,
            [Display(Name = "f/1.4")]
            D9 = 9,
            [Display(Name = "f/2")]
            D8 = 8,
            [Display(Name = "f/2.8")]
            D7 = 7,
            [Display(Name = "f/4")]
            D6 = 6,
            [Display(Name = "f/5.6")]
            D5 = 5,
            [Display(Name = "f/8")]
            D4 = 4,
            [Display(Name = "f/11")]
            D3 = 3,
            [Display(Name = "f/22")]
            D2 = 2,
            [Display(Name = "f/32")]
            D1 = 1,
            [Display(Name = "Not stated")]
            D0 = -1
        }

        public enum ShutterSpeed
        {
            [Display(Name = "1/32000 s")]
            S17 = 17,
            [Display(Name = "1/16000 s")]
            S16 = 16,
            [Display(Name = "1/8000 s")]
            S15 = 15,
            [Display(Name = "1/4000 s")]
            S14 = 14,
            [Display(Name = "1/2000 s")]
            S13 = 13,
            [Display(Name = "1/1000 s")]
            S12 = 12,
            [Display(Name = "1/500 s")]
            S11 = 11,
            [Display(Name = "1/250 s")]
            S10 = 10,
            [Display(Name = "1/125 s")]
            S9 = 9,
            [Display(Name = "1/60 s")]
            S8 = 8,
            [Display(Name = "1/30 s")]
            S7 = 7,
            [Display(Name = "1/15 s")]
            S6 = 6,
            [Display(Name = "1/8 s")]
            S5 = 5,
            [Display(Name = "1/4 s")]
            S4 = 4,
            [Display(Name = "1/2 s")]
            S3 = 3,
            [Display(Name = "1 s")]
            S2 = 2,
            [Display(Name = "2 s")]
            S1 = 1,
            [Display(Name = "Not stated")]
            S0 = -1
        }

        private const string _templatesFolder = "/Content/Templates/";

        public const string Root = "~/App_Data";
        public const string PhotoTemplate = _templatesFolder + "Photo/Index.html";
        public const string PhotoPropertiesTemplate = _templatesFolder + "Photo/Properties.html";

        public const int FreePhotosCount = 3;
        public const int FreeAlbumsCount = 3;

        public const int MaxLensFocalLength = 500;
        public const int MaxDiaphragm = 1;
        public const int MaxShutterSpeed = 15;
        public const int MaxIso = 32000;
        public const int MaxFlash = 1;
    }
}