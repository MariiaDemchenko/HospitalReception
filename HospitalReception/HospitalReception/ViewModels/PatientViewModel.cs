using System;

namespace HospitalReception.ViewModels
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public int GenderId { get; set; }
        public string Education { get; set; }
        public int EducationId { get; set; }
        public string DisabilityGroup { get; set; }
        public int DisabilityGroupId { get; set; }
        public string InformationSource { get; set; }
        public int InformationSourceId { get; set; }
        public string HabitationMember { get; set; }
        public string HabitationMemberId { get; set; }
        public string Policlinic { get; set; }
    }
}