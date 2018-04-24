using AutoMapper;
using PhotoManager.DAL.Models;
using PhotoManager.DAL.ProjectionModels;

namespace PhotoManager.DAL
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMissingTypeMaps = true;
                cfg.CreateMap<PhotoEditModel, CameraSettings>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => c.CameraSettingsId));
                cfg.CreateMap<PhotoAddModel, CameraSettings>()
                    .ForMember(x => x.Id, opt => opt.MapFrom(c => c.CameraSettingsId));
            });
        }
    }
}
