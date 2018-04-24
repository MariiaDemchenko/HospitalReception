using PhotoManager.DAL.Models;
using System.Collections.Generic;

namespace PhotoManager.DAL.ProjectionModels
{
    public class PhotoAddModel : PhotoEditModel
    {
        public string OwnerId { get; set; }
        public List<Image> Images { get; set; }
    }
}