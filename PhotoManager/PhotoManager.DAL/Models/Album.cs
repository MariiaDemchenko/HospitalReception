using System.Collections.Generic;

namespace PhotoManager.DAL.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ApplicationUser Owner { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}