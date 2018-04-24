using System;
using System.Collections.Generic;

namespace PhotoManager.DAL.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public int CameraSettingsId { get; set; }
        public string Name { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Place { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public ApplicationUser Owner { get; set; }
        public CameraSettings CameraSettings { get; set; }
        public List<Image> Images { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}