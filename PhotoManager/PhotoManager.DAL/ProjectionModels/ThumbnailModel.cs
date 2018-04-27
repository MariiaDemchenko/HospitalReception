using System.ComponentModel.DataAnnotations;

namespace PhotoManager.DAL.ProjectionModels
{
    public class ThumbnailModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}