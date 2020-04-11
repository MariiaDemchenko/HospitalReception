using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalReception.ViewModels
{
    public class CardRecord
    {
        public string Doctor { get; set; }
        public int DoctorId { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
        public string AppointmentDate { get; set; }
    }
}