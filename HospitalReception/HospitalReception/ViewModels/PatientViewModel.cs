using HospitalReception.DAL.Models;
using System;
using System.Collections.Generic;

namespace HospitalReception.ViewModels
{
    /// <summary>
    /// модель представления пациента
    /// </summary>
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
        public int HabitationMemberId { get; set; }
        public string Policlinic { get; set; }
        public string Organization { get; set; }
        public string Employment { get; set; }
        public int EmploymentId { get; set; }
        public string LocalityType { get; set; }
        public int LocalityTypeId { get; set; }
        public string Region { get; set; }
        public int RegionId { get; set; }
        public string LocalityName { get; set; }
        public int PoliclinicId { get; set; }

        public List<MeasurementViewModel> Measurements { get; set; }

        public List<ChartViewModel> SBPla {get; set;}
        public List<ChartViewModel> SBPra { get; set; }

        public List<ChartViewModel> SBPrl { get; set; }
        public List<ChartViewModel> SBPll { get; set; }

        public List<ChartViewModel> DBPla { get; set; }
        public List<ChartViewModel> DBPra { get; set; }
    }
}