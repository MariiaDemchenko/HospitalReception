using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalReception.DAL.Models
{
    public class Organization : IEntityBase
    {
        public int Id { get; set; }
        public string NameLocal { get; set; }

        [ForeignKey("Phone")]
        public int PhoneId { get; set; }

        public virtual Phone Phone { get; set; }
    }
}