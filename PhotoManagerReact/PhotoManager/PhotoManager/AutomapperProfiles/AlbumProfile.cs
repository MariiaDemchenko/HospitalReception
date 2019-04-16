using AutoMapper;
using PhotoManager.BLL.Models;
using PhotoManager.Models;
using PhotoManager.ViewModels;

namespace PhotoManager.AutomapperProfiles
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile(string imagesPath, string thumbsPath)
        {
            CreateMap<IAlbum, AlbumViewModel>().ForMember(album => album.CoverUrl, opt => opt.MapFrom(c => $"{thumbsPath}/{c.Cover}"))
                .ForMember(album => album.AlbumId, opt => opt.MapFrom(c => c.Id.ToString()));

            CreateMap<AlbumViewModel, Album>().ForMember(album => album.Id, opt => opt.MapFrom(c => c.AlbumId.ToString()));
        }
    }
}