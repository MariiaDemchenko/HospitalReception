using AutoMapper;
using HomeCinema.Data.Repositories;
using HospitalReception.DAL.Infrastructure;
using HospitalReception.DAL.Models;
using HospitalReception.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace HospitalReception.Controllers.Api
{
    [RoutePrefix("api/educationTypes")]
    public class EducationTypesController : ApiController
    {
        private readonly IEntityBaseRepository<EducationType> _educationTypesRepository;
        public EducationTypesController(IEntityBaseRepository<EducationType> educationTypesRepository, IUnitOfWork unitOfWork)
        {
            _educationTypesRepository = educationTypesRepository;
        }

        [Route("")]
        public IHttpActionResult Get(HttpRequestMessage request)
        {
            var educationTypes = _educationTypesRepository.GetAll().ToList();
            IEnumerable<EducationTypeViewModel> educationTypesViewModel = Mapper.Map<IEnumerable<EducationType>, IEnumerable<EducationTypeViewModel>>(educationTypes);
            return Ok(educationTypesViewModel);
        }
    }
}