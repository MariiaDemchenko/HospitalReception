using PhotoManager.BLL.Models;
using System;
using System.Collections.Generic;

namespace PhotoManager.Models
{
    public class Photo : IPhoto
    {
        public string Id { get; set; }

        public string PhotoName { get; set; }

        public string ServerName { get; set; }

        public string Format { get; set; }

        public string PhotoUrl { get; set; }

        public string IsSelected { get; set; }

        public List<string> Albums { get; set; }

        public string CameraModel { get; set; }

        public DateTime ShotDate { get; set; }

        public int LensFocalLength { get; set; }

        public string Owner { get; set; }
    }
}
