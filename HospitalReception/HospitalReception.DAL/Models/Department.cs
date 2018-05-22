using System.Collections.Generic;

namespace HospitalReception.DAL.Models
{
    public class Department : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}