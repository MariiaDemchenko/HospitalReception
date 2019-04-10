using AutoMapper;
using PhotoManager.BLL.Models;
using PhotoManager.Models;
using PhotoManager.ViewModels;

namespace PhotoManager.AutomapperProfiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile(string imagesPath, string thumbsPath)
        {
            CreateMap<IPhoto, PhotoViewModel>().ForMember(photo => photo.PhotoUrl, opt => opt.MapFrom(c => $"{imagesPath}/{c.ServerName}.{c.Format}"))
                .ForMember(photo => photo.PhotoId, opt => opt.MapFrom(c => c.Id.ToString()))
                .ForMember(photo => photo.ThumbUrl, opt => opt.MapFrom(c => $"{thumbsPath}/{c.ServerName}.{c.Format}"));

            CreateMap<PhotoViewModel, Photo>().ForMember(photo => photo.Id, opt => opt.MapFrom(c => c.PhotoId.ToString()));
        }
    }
}