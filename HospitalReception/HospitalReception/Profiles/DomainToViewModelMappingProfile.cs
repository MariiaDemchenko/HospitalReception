using System.Linq;
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
               .ForMember(vm=> vm.Gender, map => map.MapFrom(x=>x.Gender.NameEng))
               .ForMember(vm => vm.Education, map => map.MapFrom(x => x.EducationType.NameEng))
               .ForMember(vm => vm.DisabilityGroup, map => map.MapFrom(x=>x.DisabilityGroup.NameEng))
               .ForMember(vm=>vm.InformationSource, map=>map.MapFrom(x=>x.InformationSource.NameEng))
               .ForMember(vm=>vm.HabitationMember, map=>map.MapFrom(x=>x.HabitationMember.NameEng))
               .ForMember(vm=>vm.Policlinic, map=>map.MapFrom(x=>x.Policlinic.NameLocal));
            CreateMap<Doctor, DoctorViewModel>()
                .ForMember(vm => vm.Department, map => map.MapFrom(x => x.Department.Name));
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
            CreateMap<Appointment, AppointmentViewModel>()
                .ForMember(vm => vm.PatientFullName, map => map.MapFrom(x => x.Patient.FirstName+" "+ x.Patient.LastName));
            CreateMap<ConsultationHours, ConsultationHoursViewModel>()
                .ForMember(vm => vm.DayOfWeek, map => map.MapFrom(x => (int)x.DayOfWeek));
        }
    }
}