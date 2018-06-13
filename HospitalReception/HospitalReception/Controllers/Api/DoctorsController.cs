using AutoMapper;
using HomeCinema.Data.Repositories;
using HospitalReception.DAL.Infrastructure;
using HospitalReception.DAL.Models;
using HospitalReception.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace HospitalReception.Controllers.Api
{
    [RoutePrefix("api/doctors")]
    public class DoctorsController : ApiController
    {
        private readonly IEntityBaseRepository<Doctor> _doctorsRepository;
        private readonly IEntityBaseRepository<ConsultaionHours> _consultationHoursRepository;
        private readonly IEntityBaseRepository<Appointment> _appointmentsRepository;

        private readonly IUnitOfWork _unitOfWork;

        public DoctorsController(IEntityBaseRepository<Doctor> doctorsRepository, IEntityBaseRepository<ConsultaionHours> consultationHoursRepository, IEntityBaseRepository<Appointment> appointmentsRepository, IUnitOfWork unitOfWork)
        {
            _doctorsRepository = doctorsRepository;
            _consultationHoursRepository = consultationHoursRepository;
            _appointmentsRepository = appointmentsRepository;
            _unitOfWork = unitOfWork;
        }

        [Route("new")]
        public IHttpActionResult Get(HttpRequestMessage request)
        {
            var doctors = _doctorsRepository.GetAll();
            IEnumerable<DoctorViewModel> doctorsViewModel = Mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorViewModel>>(doctors);
            return Ok(doctorsViewModel);
        }

        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var doctor = _doctorsRepository.GetSingle(id);
            var doctorsViewModel = Mapper.Map<Doctor, DoctorViewModel>(doctor);
            return Ok(doctorsViewModel);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> Add()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = HttpContext.Current.Server.MapPath("~/App_Data");
            var targetPath = HostingEnvironment.MapPath("~/Content/Images/");
            var provider = new MultipartFormDataStreamProvider(root);
            await Request.Content.ReadAsMultipartAsync(provider);
            var imagePath = string.Empty;
            byte[] imageBytes;

            var localFileName = provider.FileData.FirstOrDefault()?.LocalFileName;
            int.TryParse(_doctorsRepository?.GetAll().ToList().LastOrDefault()?.ImageId.ToString(), out var imageId);
            imageId++;

            imageBytes = File.ReadAllBytes(!string.IsNullOrEmpty(localFileName) ? localFileName : Path.Combine(targetPath, "empty.jpg"));
            imagePath = Path.Combine(targetPath, imageId + ".jpg");
            File.WriteAllBytes(imagePath, imageBytes);

            var firstName = provider.FormData.GetValues("firstName")?.FirstOrDefault();
            var lastName = provider.FormData.GetValues("lastName")?.FirstOrDefault();
            int.TryParse(provider.FormData.GetValues("departmentId")?.FirstOrDefault(), out var departmentId);

            var doctorToAdd = new Doctor
            {
                FirstName = firstName,
                LastName = lastName,
                CreatedUserId = User.Identity.GetUserId(),
                CreationDate = DateTime.Now,
                DepartmentId = departmentId
            };

            if (File.Exists(imagePath))
            {
                doctorToAdd.ImageId = imageId;
            }
            _doctorsRepository.Add(doctorToAdd);
            _unitOfWork.Save();

            return Ok();
        }

        [Route("consultationHours/{id}")]
        public IHttpActionResult GetDoctorConsultationHours(int id)
        {
            var consultations = _consultationHoursRepository.GetAll().Where(c => c.DoctorId == id);
            var consultationHours = Mapper.Map<IEnumerable<ConsultaionHours>, IEnumerable<ConsultationHoursViewModel>>(consultations);
            return Ok(consultationHours);
        }

        [Route("appointments/{id}")]
        public IHttpActionResult GetDoctorAppointments(int id)
        {
            var appointments = _appointmentsRepository.GetAll().Where(c => c.DoctorId == id);
            var appointmentViewModel = Mapper.Map<IEnumerable<Appointment>, IEnumerable<AppointmentViewModel>>(appointments);
            return Ok(appointmentViewModel);
        }

        [HttpPost]
        [Route("appointments/add")]
        public IHttpActionResult AddDoctorAppointment(Appointment appointment)
        {
            appointment.CreatedUserId = User.Identity.GetUserId();
            appointment.CreationDate = DateTime.Now;
            _appointmentsRepository.Add(appointment);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpPut]
        [Route("appointments/edit")]
        public IHttpActionResult EditAppointment(Appointment appointment)
        {
            appointment.CreationDate = DateTime.Now;
            _appointmentsRepository.Edit(appointment);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpDelete]
        [Route("appointments/{id}")]
        public IHttpActionResult DeleteAppointment(int id)
        {
            var appointmentToDelete = _appointmentsRepository.GetSingle(id);
            _appointmentsRepository.Delete(appointmentToDelete);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpPost]
        [Route("edit/{id}")]
        public async Task<IHttpActionResult> Edit(int id)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var targetPath = HostingEnvironment.MapPath("~/Content/Images/");
            var provider = new MultipartFormDataStreamProvider(root);
            await Request.Content.ReadAsMultipartAsync(provider);

            var imagePath = string.Empty;
            var imageId = 0;

            var localFileName = provider.FileData.FirstOrDefault()?.LocalFileName;

            if (!string.IsNullOrEmpty(localFileName))
            {
                var imageBytes = File.ReadAllBytes(localFileName);
                int.TryParse(_doctorsRepository?.GetAll().ToList().LastOrDefault()?.ImageId.ToString(), out imageId);
                imageId++;
                imagePath = Path.Combine(targetPath, imageId + ".jpg");
                File.WriteAllBytes(imagePath, imageBytes);
            }

            var firstName = provider.FormData.GetValues("firstName")?.FirstOrDefault();
            var lastName = provider.FormData.GetValues("lastName")?.FirstOrDefault();
            int.TryParse(provider.FormData.GetValues("departmentId")?.FirstOrDefault(), out var departmentId);

            var doctor = _doctorsRepository.GetSingle(id);
            doctor.FirstName = firstName;
            doctor.LastName = lastName;
            doctor.DepartmentId = departmentId;

            if (File.Exists(imagePath))
            {
                doctor.ImageId = imageId;
            }
            _doctorsRepository.Edit(doctor);
            _unitOfWork.Save();

            return Ok();
        }
    }
}