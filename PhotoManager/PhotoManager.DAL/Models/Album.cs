using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoManager.DAL.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public virtual ApplicationUser Owner { get; set; }
        public virtual ICollection<Photo2Album> Photo2Albums { get; set; }
    }
}
