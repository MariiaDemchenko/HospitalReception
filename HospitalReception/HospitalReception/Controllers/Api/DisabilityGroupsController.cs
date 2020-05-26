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
    [RoutePrefix("api/disabilityGroups")]
    public class DisabilityGroupsController : ApiController
    {
        private readonly IEntityBaseRepository<DisabilityGroup> _disabilityGroupsRepository;
  
        public DisabilityGroupsController(IEntityBaseRepository<DisabilityGroup> disabilityGroupsRepository)
        {
            _disabilityGroupsRepository = disabilityGroupsRepository;
        }

        [Route("")]
        public IHttpActionResult Get(string lookupName)
        {
            var disabilityGroups = _disabilityGroupsRepository.GetAll().ToList();
            IEnumerable<DisabilityGroupViewModel> disabilityGroupsViewModel = Mapper.Map<IEnumerable<DisabilityGroup>, IEnumerable<DisabilityGroupViewModel>>(disabilityGroups);
            return Ok(disabilityGroupsViewModel);
        }
    }
}