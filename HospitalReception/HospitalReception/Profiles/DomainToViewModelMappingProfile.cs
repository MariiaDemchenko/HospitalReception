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
            CreateMap<Doctor, DoctorViewModel>()
                .ForMember(vm => vm.Department, map => map.MapFrom(x => x.Department.Name));
            CreateMap<Department, DepartmentViewModel>()
                .ForMember(vm => vm.NumberOfDoctors, map => map.MapFrom(x => x.Doctors.Count));
            CreateMap<Appointment, AppointmentViewModel>()
                .ForMember(vm => vm.PatientFullName, map => map.MapFrom(x => x.Patient.FirstName+" "+ x.Patient.LastName));
            CreateMap<ConsultaionHours, ConsultationHoursViewModel>()
                .ForMember(vm => vm.DayOfWeek, map => map.MapFrom(x => (int)x.DayOfWeek));
        }
    }
}