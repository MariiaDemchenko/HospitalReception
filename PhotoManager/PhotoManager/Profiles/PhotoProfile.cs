using AutoMapper;
using PhotoManager.Common;
using PhotoManager.DAL.ProjectionModels;
using PhotoManager.ViewModels.PhotoManagerViewModels;

namespace PhotoManager.Profiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<PhotoEditModel, PhotoPropertiesModel>()
                .ForMember(x => x.ShutterSpeed, opt => opt.MapFrom(x => x.ShutterSpeed.GetDisplayName().Name))
                .ForMember(x => x.Diaphragm, opt => opt.MapFrom(x => x.Diaphragm.GetDisplayName().Name))
                .ForMember(x => x.Flash, opt => opt.MapFrom(x => x.Flash.GetDisplayName().Name));
        }
    }
}