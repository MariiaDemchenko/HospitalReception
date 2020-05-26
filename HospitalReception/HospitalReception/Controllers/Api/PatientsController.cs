using AutoMapper;
using HomeCinema.Data.Repositories;
using HospitalReception.DAL.Infrastructure;
using HospitalReception.DAL.Models;
using HospitalReception.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace HospitalReception.Controllers.Api
{
    [RoutePrefix("api/patients")]
    public class PatientsController : ApiController
    {
        private readonly IEntityBaseRepository<Patient> _patientsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PatientsController(IEntityBaseRepository<Patient> patientsRepository, IUnitOfWork unitOfWork)
        {
            _patientsRepository = patientsRepository;
            _unitOfWork = unitOfWork;
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            var patients = _patientsRepository.GetAll();
            foreach(var patient in patients)
            {
                var birthDate = patient.BirthDate;
                patient.BirthDate = new DateTime(birthDate.Year, birthDate.Month, birthDate.Day, birthDate.Hour, birthDate.Minute, birthDate.Second, DateTimeKind.Local);
            }
            IEnumerable<PatientViewModel> patientsViewModel = Mapper.Map<IEnumerable<Patient>, IEnumerable<PatientViewModel>>(patients);
            return Ok(patientsViewModel);
        }

        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var patient = _patientsRepository.GetSingle(id);
            var patientsViewModel = Mapper.Map<Patient, PatientViewModel>(patient);
            return Ok(patientsViewModel);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Edit(Patient patient)
        {
            var patientToEdit = _patientsRepository.GetSingle(patient.Id);
            patientToEdit.FirstName = patient.FirstName;
            patientToEdit.LastName = patient.LastName;
            patientToEdit.DisabilityGroupId = patient.DisabilityGroupId;
            patientToEdit.BirthDate = patient.BirthDate;
            patientToEdit.EducationId = patient.EducationId;
            patientToEdit.GenderId = patient.GenderId;
            patientToEdit.InformationSourceId = patient.InformationSourceId;
            patientToEdit.HabitationMemberId = patient.HabitationMemberId;

            _patientsRepository.Edit(patientToEdit);
            _unitOfWork.Save();
            return Ok();
        }

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
    }
}
