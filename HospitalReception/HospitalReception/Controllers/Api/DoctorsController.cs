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
        private readonly IEntityBaseRepository<ConsultationHours> _consultationHoursRepository;
        private readonly IEntityBaseRepository<Appointment> _appointmentsRepository;

        private readonly IUnitOfWork _unitOfWork;

        public DoctorsController(IEntityBaseRepository<Doctor> doctorsRepository, IEntityBaseRepository<ConsultationHours> consultationHoursRepository, IEntityBaseRepository<Appointment> appointmentsRepository, IUnitOfWork unitOfWork)
        {
            _doctorsRepository = doctorsRepository;
            _consultationHoursRepository = consultationHoursRepository;
            _appointmentsRepository = appointmentsRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// запрос на выборку всех докторов
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("new")]
        public IHttpActionResult Get(HttpRequestMessage request)
        {
            var doctors = _doctorsRepository.GetAll();
            IEnumerable<DoctorViewModel> doctorsViewModel = Mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorViewModel>>(doctors);
            return Ok(doctorsViewModel);
        }

        /// <summary>
        /// запрос на выборку докторов, ассоциированных с заданным департаментом
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("department/{id}")]
        public IHttpActionResult GetByDepartmentId(int id)
        {
            var doctors = _doctorsRepository.GetAll();
            IEnumerable<DoctorViewModel> doctorsViewModel = Mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorViewModel>>(doctors);
            return Ok(doctorsViewModel.Where(d => d.DepartmentId == id));
        }

        /// <summary>
        /// запрос доктора по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var doctor = _doctorsRepository.GetSingle(id);
            var doctorsViewModel = Mapper.Map<Doctor, DoctorViewModel>(doctor);
            return Ok(doctorsViewModel);
        }

        /// <summary>
        /// добавление  доктора
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// запрос приемных часов доктора
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [Route("consultationHours/{id}")]
        public IHttpActionResult GetDoctorConsultationHours(int id)
        {
            var consultations = _consultationHoursRepository.GetAll().Where(c => c.DoctorId == id);
            var consultationHours = Mapper.Map<IEnumerable<ConsultationHours>, IEnumerable<ConsultationHoursViewModel>>(consultations);
            return Ok(consultationHours);
        }

        [Authorize]
        [Route("consultationHours/{id}/closest/{duration}")]
        public IHttpActionResult GetClosestDoctorConsultationHours(int id, int duration)
        {
            var consultations = _consultationHoursRepository.GetAll().Where(c => c.DoctorId == id).ToList();
            var appointments = _appointmentsRepository.GetAll().Where(a => a.DoctorId == id).ToList();

            var currentDate = DateTime.Today;
            ConsultationHours currentConsultation;
            var slotIsFound = false;
            var timeSlot = new TimeSlotViewModel();

            for (var day = currentDate; !slotIsFound; day = day.AddDays(1))
            {
                currentConsultation = consultations.FirstOrDefault(c => c.DayOfWeek == day.DayOfWeek);
                if (currentConsultation != null)
                {
                    for (var hour = currentConsultation.StartHour; hour <= currentConsultation.EndHour && !slotIsFound; hour++)
                    {
                        var maxMinutes = hour == currentConsultation.EndHour ? currentConsultation.EndMinutes : 60;
                        for (var minute = currentConsultation.StartMinutes;
                            minute < maxMinutes && !slotIsFound;
                            minute += duration)
                        {
                            var startTime = new DateTime(day.Year, day.Month, day.Day, hour, minute, 0);
                            var endTime = startTime.AddMinutes(duration);
                            if (DateTime.Now >= startTime)
                            {
                                continue;
                            }
                            var reservedAppointmentsCount =
                                appointments.Count(a => a.EndDate > startTime && a.StartDate < endTime);
                            if (reservedAppointmentsCount == 0)
                            {
                                timeSlot = new TimeSlotViewModel { StartDate = startTime, EndDate = endTime };
                                slotIsFound = true;
                            }
                        }
                    }
                }
            }
            return Ok(timeSlot);
        }

        /// <summary>
        /// запрос назначенных визитов доктора
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [Route("appointments/{id}")]
        public IHttpActionResult GetDoctorAppointments(int id)
        {
            var appointments = _appointmentsRepository.GetAll().Where(c => c.DoctorId == id);
            var appointmentViewModel = Mapper.Map<IEnumerable<Appointment>, IEnumerable<AppointmentViewModel>>(appointments);
            return Ok(appointmentViewModel);
        }

        /// <summary>
        /// добавление нового визита
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("appointments/add")]
        public IHttpActionResult AddDoctorAppointment(Appointment appointment)
        {
            appointment.CreatedUserId = User.Identity.GetUserId();
            appointment.CreationDate = DateTime.Now;
            _appointmentsRepository.Add(appointment);
            _unitOfWork.Save();
            var appointmentViewModel = Mapper.Map<Appointment, AppointmentViewModel>(appointment);
            return Ok(appointmentViewModel);
        }

        /// <summary>
        /// редактирование визита
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("appointments/edit")]
        public IHttpActionResult EditAppointment(Appointment appointment)
        {
            appointment.CreationDate = DateTime.Now;
            _appointmentsRepository.Edit(appointment);
            _unitOfWork.Save();
            return Ok();
        }

        /// <summary>
        /// удаление визита
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("appointments/{id}")]
        public IHttpActionResult DeleteAppointment(int id)
        {
            var appointmentToDelete = _appointmentsRepository.GetSingle(id);
            _appointmentsRepository.Delete(appointmentToDelete);
            _unitOfWork.Save();
            return Ok();
        }

        [Authorize]
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
                var doctors1 = _doctorsRepository.GetAll().OrderBy(d => d.ImageId);
                int.TryParse(_doctorsRepository.GetAll().ToList().OrderBy(d => d.ImageId).LastOrDefault()?.ImageId.ToString(),
                    out imageId);
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