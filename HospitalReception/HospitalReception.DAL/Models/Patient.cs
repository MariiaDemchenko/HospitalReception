using System;
using System.Collections.Generic;

namespace HospitalReception.DAL.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}