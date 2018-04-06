using AutoMapper;
using PhotoManager.DAL.Models;
using PhotoManager.ViewModels.PhotoManagerViewModels;
using System.Collections.Generic;
using System.Linq;

namespace PhotoManager
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Photo, PhotoViewModel>()
                    .ForMember(x => x.ImageUrl, opt => opt.MapFrom(c => $"/api/Image/{c.ImageId}"))
                    .ForMember(x => x.CameraModel, opt => opt.MapFrom(c => c.CameraSettings.CameraModel))
                    .ForMember(x => x.LensFocalLength, opt => opt.MapFrom(c => c.CameraSettings.LensFocalLength))
                    .ForMember(x => x.Diaphragm, opt => opt.MapFrom(c => c.CameraSettings.Diaphragm))
                    .ForMember(x => x.ShutterSpeed, opt => opt.MapFrom(c => c.CameraSettings.ShutterSpeed))
                    .ForMember(x => x.Iso, opt => opt.MapFrom(c => c.CameraSettings.Iso))
                    .ForMember(x => x.Flash, opt => opt.MapFrom(c => c.CameraSettings.Flash));
                cfg.CreateMap<Album, AlbumViewModel>()
                    .ForMember(x => x.MainImageUrl, opt => opt.MapFrom(c => $"/api/Image/{c.Photos.FirstOrDefault().ImageId}"))
                    .ForMember(x => x.Photos, opt => opt.MapFrom(c => Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoViewModel>>(c.Photos)))
                    .ForMember(x => x.Photos, opt => opt.MapFrom(c => Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoViewModel>>(c.Photos)));
                cfg.CreateMap<PhotoViewModel, CameraSettings>();
            });
        }
    }
}