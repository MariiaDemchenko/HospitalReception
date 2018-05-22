using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalReception.DAL.Models
{
    public class Patient : IEntityBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        [ForeignKey("User")]
        public string CreatedUserId { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}