using AutoMapper;
using HospitalReception.Profiles;

namespace HospitalReception
{
    public class AutoMapperConfig
    {
        public class AutoMapperConfiguration
        {
            public static void Configure()
            {
                Mapper.Initialize(x => { x.AddProfile<DomainToViewModelMappingProfile>(); });
            }
        }
    }
}