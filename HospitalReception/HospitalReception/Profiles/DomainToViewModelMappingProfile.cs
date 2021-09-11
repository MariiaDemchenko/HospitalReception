using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using HospitalReception.DAL.Models;
using HospitalReception.ViewModels;

namespace HospitalReception.Profiles
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        public DomainToViewModelMappingProfile()
        {
            CreateMap<Patient, PatientViewModel>()
               .ForMember(vm => vm.Gender, map => map.MapFrom(x => x.Gender.NameEng))
               .ForMember(vm => vm.Education, map => map.MapFrom(x => x.EducationType.NameEng))
               .ForMember(vm => vm.DisabilityGroup, map => map.MapFrom(x => x.DisabilityGroup.NameEng))
               .ForMember(vm => vm.InformationSource, map => map.MapFrom(x => x.InformationSource.NameEng))
               .ForMember(vm => vm.HabitationMember, map => map.MapFrom(x => x.HabitationMember.NameEng))
               .ForMember(vm => vm.Policlinic, map => map.MapFrom(x => x.Policlinic.NameLocal))
               .ForMember(vm => vm.Employment, map => map.MapFrom(x => x.EmploymentType.NameEng))
               .ForMember(vm => vm.LocalityType, map => map.MapFrom(x => x.LocalityType.NameEng))
               .ForMember(vm => vm.Region, map => map.MapFrom(x => x.Region.NameEng))
               .ForMember(vm => vm.Measurements, map => map.MapFrom(x => x.Measurements.GroupBy(g => g.Item.Label).
               Select(g => g.OrderByDescending(d => d.MeasurementDate).FirstOrDefault()).
               Select(m => new MeasurementViewModel
               {
                   Label = m.Item.Label,
                   Value = m.Value,
                   MeasurementDate = m.MeasurementDate,
                   UnitName = m.Item.UnitName
               })))
               .ForMember(vm => vm.SBPla, map => map.MapFrom(x => x.Measurements.Where(m => m.ItemId == 17).Select(m => new ChartViewModel() { Value = Convert.ToDouble(m.Value), MeasurementDate = m.MeasurementDate }).ToList()))
               .ForMember(vm => vm.SBPra, map => map.MapFrom(x => x.Measurements.Where(m => m.ItemId == 14).Select(m => new ChartViewModel() { Value = Convert.ToDouble(m.Value), MeasurementDate = m.MeasurementDate }).ToList()))
               .ForMember(vm => vm.SBPrl, map => map.MapFrom(x => x.Measurements.Where(m => m.ItemId == 21).Select(m => new ChartViewModel() { Value = Convert.ToDouble(m.Value), MeasurementDate = m.MeasurementDate }).ToList()))
               .ForMember(vm => vm.SBPll, map => map.MapFrom(x => x.Measurements.Where(m => m.ItemId == 22).Select(m => new ChartViewModel() { Value = Convert.ToDouble(m.Value), MeasurementDate = m.MeasurementDate }).ToList()))
               .ForMember(vm => vm.DBPla, map => map.MapFrom(x => x.Measurements.Where(m => m.ItemId == 18).Select(m => new ChartViewModel() { Value = Convert.ToDouble(m.Value), MeasurementDate = m.MeasurementDate }).ToList()))
               .ForMember(vm => vm.DBPra, map => map.MapFrom(x => x.Measurements.Where(m => m.ItemId == 15).Select(m => new ChartViewModel() { Value = Convert.ToDouble(m.Value), MeasurementDate = m.MeasurementDate }).ToList()))
               ;

            CreateMap<Doctor, DoctorViewModel>()
                .ForMember(vm => vm.Department, map => map.MapFrom(x => x.Department.Name))
                .ForMember(vm => vm.FullName, map => map.MapFrom(x => $"{x.FirstName} {x.LastName}"));
            CreateMap<Department, DepartmentViewModel>()
                .ForMember(vm => vm.NumberOfDoctors, map => map.MapFrom(x => x.Doctors.Count));
            CreateMap<Gender, GenderViewModel>()
               .ForMember(vm => vm.Name, map => map.MapFrom(x => x.NameEng));
            CreateMap<EducationType, EducationTypeViewModel>()
                .ForMember(vm => vm.Name, map => map.MapFrom(x => x.NameEng));
            CreateMap<DisabilityGroup, DisabilityGroupViewModel>()
                .ForMember(vm => vm.Name, map => map.MapFrom(x => x.NameEng));
            CreateMap<InformationSource, InformationSourceViewModel>()
                .ForMember(vm => vm.Name, map => map.MapFrom(x => x.NameEng));
            CreateMap<HabitationMember, HabitationMemberViewModel>()
                .ForMember(vm => vm.Name, map => map.MapFrom(x => x.NameEng));
            CreateMap<LocalityType, LocalityTypeViewModel>()
                .ForMember(vm => vm.Name, map => map.MapFrom(x => x.NameEng));
            CreateMap<Region, RegionViewModel>()
                .ForMember(vm => vm.Name, map => map.MapFrom(x => x.NameEng));
            CreateMap<DisabilityGroup, DisabilityGroupViewModel>()
                .ForMember(vm => vm.Name, map => map.MapFrom(x => x.NameEng));
            CreateMap<Organization, OrganizationViewModel>()
                .ForMember(vm => vm.Name, map => map.MapFrom(x => x.NameLocal));
            CreateMap<Appointment, AppointmentViewModel>()
                .ForMember(vm => vm.PatientFullName, map => map.MapFrom(x => x.Patient.FirstName + " " + x.Patient.LastName));
            CreateMap<EmploymentType, EmploymentTypeViewModel>()
                .ForMember(vm => vm.Name, map => map.MapFrom(x => x.NameEng));
            CreateMap<Appointment, CardRecordViewModel>()
                .ForMember(vm => vm.Department, map => map.MapFrom(x => x.Doctor.Department.Name))
                .ForMember(vm => vm.AppointmentDate, map => map.MapFrom(x => x.StartDate.ToLongDateString()))
                .ForMember(vm => vm.Doctor, map => map.MapFrom(x => x.Doctor.FirstName + " " + x.Doctor.LastName));
            CreateMap<ConsultationHours, ConsultationHoursViewModel>()
                .ForMember(vm => vm.DayOfWeek, map => map.MapFrom(x => (int)x.DayOfWeek));
        }
    }
}