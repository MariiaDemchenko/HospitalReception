using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalReception.DAL.Models
{
    public class PatientsOrganization : IEntityBase
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public string Organization { get; set; }
        [ForeignKey("EmploymentType")]
        public int EmploymentTypeId { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual EmploymentType EmploymentType { get; set; }
    }
}