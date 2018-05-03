using AutoMapper;
using PhotoManager.DAL.Models;
using PhotoManager.DAL.ProjectionModels;

namespace PhotoManager.DAL.Profiles
{
    public class ProjectionsProfile : Profile
    {
        public ProjectionsProfile()
        {
            CreateMap<PhotoEditModel, CameraSettings>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => c.CameraSettingsId));
            CreateMap<PhotoAddModel, CameraSettings>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => c.CameraSettingsId));
        }
    }
}