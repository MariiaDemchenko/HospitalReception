using AutoMapper;
using HomeCinema.Data.Repositories;
using HospitalReception.DAL.Infrastructure;
using HospitalReception.DAL.Models;
using HospitalReception.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HospitalReception.Controllers.Api
{
    [RoutePrefix("api/lookups")]
    public class LookupsController : ApiController
    {
        private readonly IEntityBaseRepository<Gender> _genderRepository;
        private readonly IEntityBaseRepository<EducationType> _educationTypesRepository;
        private readonly IEntityBaseRepository<DisabilityGroup> _disabilityGroupsRepository;
        private readonly IEntityBaseRepository<InformationSource> _informationSourcesRepository;
        private readonly IEntityBaseRepository<HabitationMember> _habitationMembersRepository;

        public LookupsController(IEntityBaseRepository<Gender> genderRepository,
            IEntityBaseRepository<EducationType> educationTypesRepository,
            IEntityBaseRepository<DisabilityGroup> disabilityGroupsRepository,
            IEntityBaseRepository<InformationSource> informationSourcesRepository,
            IEntityBaseRepository<HabitationMember> habitationMembersRepository)
        {
            _genderRepository = genderRepository;
            _educationTypesRepository = educationTypesRepository;
            _disabilityGroupsRepository = disabilityGroupsRepository;
            _informationSourcesRepository = informationSourcesRepository;
            _habitationMembersRepository = habitationMembersRepository;
        }

        [Route("genders")]
        public IHttpActionResult GetGenders()
        {
            var genders = _genderRepository.GetAll().ToList();
            var gendersViewModel = Mapper.Map<IEnumerable<Gender>, IEnumerable<GenderViewModel>>(genders);
            return Ok(gendersViewModel);
        }

        [Route("educationTypes")]
        public IHttpActionResult GetEducationTypes()
        {
            var educationTypes = _educationTypesRepository.GetAll().ToList();
            var educationTypesViewModel = Mapper.Map<IEnumerable<EducationType>, IEnumerable<EducationTypeViewModel>>(educationTypes);
            return Ok(educationTypesViewModel);
        }

        [Route("disabilityGroups")]
        public IHttpActionResult GetDisabilityGroups()
        {
            var disabilityGroups = _disabilityGroupsRepository.GetAll().ToList();
            var disabilityGroupsViewModel = Mapper.Map<IEnumerable<DisabilityGroup>, IEnumerable<DisabilityGroupViewModel>>(disabilityGroups);
            return Ok(disabilityGroupsViewModel);
        }

        [Route("informationSources")]
        public IHttpActionResult GetInformationSources()
        {
            var informationSources = _informationSourcesRepository.GetAll().ToList();
            var informationSourcesViewModel = Mapper.Map<IEnumerable<InformationSource>, IEnumerable<InformationSourceViewModel>>(informationSources);
            return Ok(informationSourcesViewModel);
        }

        [Route("habitationMembers")]
        public IHttpActionResult GetHabitationMembers()
        {
            var habitationMembers = _habitationMembersRepository.GetAll().ToList();
            var habitationMembersViewModel = Mapper.Map<IEnumerable<HabitationMember>, IEnumerable<HabitationMemberViewModel>>(habitationMembers);
            return Ok(habitationMembersViewModel);
        }
    }
}