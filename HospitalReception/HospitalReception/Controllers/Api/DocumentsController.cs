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
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;

namespace HospitalReception.Controllers.Api
{
    [RoutePrefix("api/documents")]
    public class DocumentsController : ApiController
    {
        private readonly IEntityBaseRepository<Document> _documentsRepository;
        private readonly IEntityBaseRepository<Appointment> _appointmentsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DocumentsController(IEntityBaseRepository<Document> documentsRepository,  IUnitOfWork unitOfWork)
        {
            _documentsRepository = documentsRepository;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [Route("{id}")]
        public IHttpActionResult Get(int patientId, int documentTypeId)
        {
            var document = _documentsRepository.GetAll().FirstOrDefault(d => d.PatientId == patientId && d.DocumentTypeId == documentTypeId);
            var documentViewModel = Mapper.Map<Document, DocumentViewModel>(document);
            return Ok(documentViewModel);
        }

        //[Authorize]
        //[HttpPost]
        //[Route("")]
        //public IHttpActionResult Edit(PatientViewModel patient)
        //{
        //    var patientToEdit = _patientsRepository.GetSingle(patient.Id);

        //    patientToEdit.FirstName = patient.FirstName;
        //    patientToEdit.LastName = patient.LastName;
        //    patientToEdit.DisabilityGroupId = patient.DisabilityGroupId;
        //    patientToEdit.BirthDate = patient.BirthDate;
        //    patientToEdit.PhoneNumber = patient.PhoneNumber;
        //    patientToEdit.Email = patient.Email;
        //    patientToEdit.EducationId = patient.EducationId;
        //    patientToEdit.GenderId = patient.GenderId;
        //    patientToEdit.InformationSourceId = patient.InformationSourceId;
        //    patientToEdit.HabitationMemberId = patient.HabitationMemberId;
        //    patientToEdit.PoliclinicId = patient.PoliclinicId;

        //    _patientsRepository.Edit(patientToEdit);
        //    _unitOfWork.Save();
        //    return Ok();
        //}

        //[Authorize]
        //[HttpPost]
        //[Route("add")]
        //public IHttpActionResult Add(Patient patient)
        //{
        //    patient.CreationDate = DateTime.Now;
        //    patient.CreatedUserId = User.Identity.GetUserId();
        //    patient.RegistrationDate = DateTime.Now;
        //    _patientsRepository.Add(patient);
        //    _unitOfWork.Save();
        //    return Ok();
        //}

        //[Authorize]
        //[Route("appointments/{id}")]
        //public IHttpActionResult GetDoctorAppointments(int id)
        //{
        //    var appointments = _appointmentsRepository.GetAll().Where(c => c.PatientId == id);
        //    var appointmentViewModel = Mapper.Map<IEnumerable<Appointment>, IEnumerable<CardRecord>>(appointments);
        //    return Ok(appointmentViewModel);
        //}
    }
}
