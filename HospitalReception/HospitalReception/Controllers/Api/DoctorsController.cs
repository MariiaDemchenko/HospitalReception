using AutoMapper;
using HomeCinema.Data.Repositories;
using HospitalReception.DAL.Infrastructure;
using HospitalReception.DAL.Models;
using HospitalReception.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace HospitalReception.Controllers.Api
{
    [RoutePrefix("api/doctors")]
    public class DoctorsController : ApiController
    {
        private readonly IEntityBaseRepository<Doctor> _doctorsRepository;
        public DoctorsController(IEntityBaseRepository<Doctor> doctorsRepository, IUnitOfWork unitOfWork)
        {
            _doctorsRepository = doctorsRepository;
        }

        [Route("new")]
        public IHttpActionResult Get(HttpRequestMessage request)
        {
            var doctors = _doctorsRepository.GetAll();
            IEnumerable<DoctorViewModel> doctorsViewModel = Mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorViewModel>>(doctors);
            return Ok(doctorsViewModel);
        }
    }
}