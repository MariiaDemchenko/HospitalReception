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
    [RoutePrefix("api/departments")]
    public class DepartmentsController : ApiController
    {
        private readonly IEntityBaseRepository<Department> _departmentsRepository;
        public DepartmentsController(IEntityBaseRepository<Department> departmentsRepository, IUnitOfWork unitOfWork)
        {
            _departmentsRepository = departmentsRepository;
        }

        [Route("")]
        public IHttpActionResult Get(HttpRequestMessage request)
        {
            var departments = _departmentsRepository.GetAll().ToList();
            IEnumerable<DepartmentViewModel> departmentsViewModel = Mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return Ok(departmentsViewModel);
        }
    }
}