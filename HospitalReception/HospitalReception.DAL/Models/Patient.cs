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
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        [ForeignKey("User")]
        public string CreatedUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public string MiddleName { get; set; }
        [ForeignKey("Gender")]
        public int GenderId { get; set; }

        [ForeignKey("EducationType")]
        public int EducationId { get; set; }
        [ForeignKey("DisabilityGroup")]
        public int DisabilityGroupId { get; set; }
        [ForeignKey("InformationSource")]
        public int InformationSourceId { get; set; }
        [ForeignKey("HabitationMember")]
        public int HabitationMemberId { get; set; }
        [ForeignKey("Policlinic")]
        public int PoliclinicId { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual EducationType EducationType { get; set; }
        public virtual DisabilityGroup DisabilityGroup { get; set; }
        public virtual InformationSource InformationSource { get; set; }
        public virtual HabitationMember HabitationMember { get; set; }
        public virtual Organization Policlinic { get; set; }
    }
}