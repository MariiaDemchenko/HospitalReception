using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalReception.ViewModels
{
    public class DoctorViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public double Rating { get; set; }
    }
}