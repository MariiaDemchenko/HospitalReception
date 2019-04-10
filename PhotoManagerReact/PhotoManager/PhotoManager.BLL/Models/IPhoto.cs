using System;
using System.Collections.Generic;

namespace PhotoManager.BLL.Models
{
    public interface IPhoto
    {
        string Id { get; set; }

        string PhotoName { get; set; }

        string ServerName { get; set; }

        string Format { get; set; }

        string CameraModel { get; set; }

        DateTime ShotDate { get; set; }
        
        int LensFocalLength { get; set; }

        List<string> Albums { get; set; }

        string Owner { get; set; }
    }
}
