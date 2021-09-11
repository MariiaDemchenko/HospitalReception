using AutoMapper;
using HomeCinema.Data.Repositories;
using HospitalReception.DAL.Infrastructure;
using HospitalReception.DAL.Models;
using HospitalReception.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;


namespace HospitalReception.Controllers.Api
{
    [RoutePrefix("api/patients")]
    public class PatientsController : ApiController
    {
        private readonly IEntityBaseRepository<Patient> _patientsRepository;
        private readonly IEntityBaseRepository<Appointment> _appointmentsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PatientsController(IEntityBaseRepository<Patient> patientsRepository, IEntityBaseRepository<Appointment> appointmentsRepository, IUnitOfWork unitOfWork)
        {
            _patientsRepository = patientsRepository;
            _appointmentsRepository = appointmentsRepository;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [Route("")]
        public IHttpActionResult Get(HttpRequestMessage request)
        {
            var patients = _patientsRepository.GetAll();
            var p = patients.ToList();
            IEnumerable<PatientViewModel> patientsViewModel = Mapper.Map<IEnumerable<Patient>, IEnumerable<PatientViewModel>>(patients);
            return Ok(patientsViewModel);
        }

        [Authorize]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var patient = _patientsRepository.GetSingle(id);
            var patientsViewModel = Mapper.Map<Patient, PatientViewModel>(patient);
            return Ok(patientsViewModel);
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public IHttpActionResult Edit(PatientViewModel patient)
        {
            var patientToEdit = _patientsRepository.GetSingle(patient.Id);

            patientToEdit.FirstName = patient.FirstName;
            patientToEdit.LastName = patient.LastName;
            patientToEdit.DisabilityGroupId = patient.DisabilityGroupId;
            patientToEdit.BirthDate = patient.BirthDate;
            patientToEdit.PhoneNumber = patient.PhoneNumber;
            patientToEdit.Email = patient.Email;
            patientToEdit.EducationId = patient.EducationId;
            patientToEdit.GenderId = patient.GenderId;
            patientToEdit.InformationSourceId = patient.InformationSourceId;
            patientToEdit.HabitationMemberId = patient.HabitationMemberId;
            patientToEdit.PoliclinicId = patient.PoliclinicId;
            patientToEdit.EmploymentId = patient.EmploymentId;
            patientToEdit.LocalityTypeId = patient.LocalityTypeId;

            _patientsRepository.Edit(patientToEdit);
            _unitOfWork.Save();
            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("predict")]
        public IHttpActionResult PredictRisk(PatientViewModel patient)
        {
            var patientToEdit = _patientsRepository.GetSingle(patient.Id);

            _patientsRepository.Predict(patientToEdit.Measurements.ToArray());

            //patientToEdit.FirstName = patient.FirstName;
            //patientToEdit.LastName = patient.LastName;
            //patientToEdit.DisabilityGroupId = patient.DisabilityGroupId;
            //patientToEdit.BirthDate = patient.BirthDate;
            //patientToEdit.PhoneNumber = patient.PhoneNumber;
            //patientToEdit.Email = patient.Email;
            //patientToEdit.EducationId = patient.EducationId;
            //patientToEdit.GenderId = patient.GenderId;
            //patientToEdit.InformationSourceId = patient.InformationSourceId;
            //patientToEdit.HabitationMemberId = patient.HabitationMemberId;
            //patientToEdit.PoliclinicId = patient.PoliclinicId;
            //patientToEdit.EmploymentId = patient.EmploymentId;

            //_patientsRepository.Edit(patientToEdit);
            //_unitOfWork.Save();
            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("add")]
        public IHttpActionResult Add(Patient patient)
        {
            patient.CreationDate = DateTime.Now;
            patient.CreatedUserId = User.Identity.GetUserId();
            patient.RegistrationDate = DateTime.Now;
            _patientsRepository.Add(patient);
            _unitOfWork.Save();
            return Ok();
        }

        [Authorize]
        [Route("appointments/{id}")]
        public IHttpActionResult GetDoctorAppointments(int id)
        {
            var appointments = _appointmentsRepository.GetAll().Where(c => c.PatientId == id);
            var appointmentViewModel = Mapper.Map<IEnumerable<Appointment>, IEnumerable<CardRecordViewModel>>(appointments);
            return Ok(appointmentViewModel);
        }
    }
}
