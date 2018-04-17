using AutoMapper;
using PhotoManager.DAL.Models;
using PhotoManager.ViewModels.PhotoManagerViewModels;
using System.Collections.Generic;
using System.Linq;
using PhotoManager.Common;

namespace PhotoManager
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Photo, PhotoViewModel>()
                    .ForMember(x => x.ImageUrl, opt => opt.MapFrom(c => $"/api/Image/{c.Images.FirstOrDefault(i => i.Size == Constants.ImageSize.Thumbnail).Id}"))
                    .ForMember(x => x.CameraModel, opt => opt.MapFrom(c => c.CameraSettings.CameraModel))
                    .ForMember(x => x.LensFocalLength, opt => opt.MapFrom(c => c.CameraSettings.LensFocalLength))
                    .ForMember(x => x.Diaphragm, opt => opt.MapFrom(c => c.CameraSettings.Diaphragm))
                    .ForMember(x => x.ShutterSpeed, opt => opt.MapFrom(c => c.CameraSettings.ShutterSpeed))
                    .ForMember(x => x.Iso, opt => opt.MapFrom(c => c.CameraSettings.Iso))
                    .ForMember(x => x.Flash, opt => opt.MapFrom(c => c.CameraSettings.Flash));
                cfg.CreateMap<Photo, PhotoMediumViewModel>()
                    .ForMember(x => x.ImageUrl, opt => opt.MapFrom(c => $"/api/Image/{c.Images.FirstOrDefault(i => i.Size == Constants.ImageSize.Medium).Id}"))
                    .ForMember(x => x.CameraModel, opt => opt.MapFrom(c => c.CameraSettings.CameraModel))
                    .ForMember(x => x.LensFocalLength, opt => opt.MapFrom(c => c.CameraSettings.LensFocalLength))
                    .ForMember(x => x.Diaphragm, opt => opt.MapFrom(c => c.CameraSettings.Diaphragm))
                    .ForMember(x => x.ShutterSpeed, opt => opt.MapFrom(c => c.CameraSettings.ShutterSpeed))
                    .ForMember(x => x.Iso, opt => opt.MapFrom(c => c.CameraSettings.Iso))
                    .ForMember(x => x.Flash, opt => opt.MapFrom(c => c.CameraSettings.Flash));
                cfg.CreateMap<Photo, PhotoOriginalViewModel>()
                    .ForMember(x => x.ImageUrl, opt => opt.MapFrom(c => $"/api/Image/{c.Images.FirstOrDefault(i => i.Size == Constants.ImageSize.Original).Id}"))
                    .ForMember(x => x.CameraModel, opt => opt.MapFrom(c => c.CameraSettings.CameraModel))
                    .ForMember(x => x.LensFocalLength, opt => opt.MapFrom(c => c.CameraSettings.LensFocalLength))
                    .ForMember(x => x.Diaphragm, opt => opt.MapFrom(c => c.CameraSettings.Diaphragm))
                    .ForMember(x => x.ShutterSpeed, opt => opt.MapFrom(c => c.CameraSettings.ShutterSpeed))
                    .ForMember(x => x.Iso, opt => opt.MapFrom(c => c.CameraSettings.Iso))
                    .ForMember(x => x.Flash, opt => opt.MapFrom(c => c.CameraSettings.Flash));
                cfg.CreateMap<Album, AlbumViewModel>()
                    .ForMember(x => x.MainImageUrl, opt => opt.MapFrom(c => $"/api/Image/{c.Photos.FirstOrDefault().Images.FirstOrDefault(i=>i.Size == Constants.ImageSize.Thumbnail).Id}"))
                    .ForMember(x => x.Photos, opt => opt.MapFrom(c => Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoViewModel>>(c.Photos)));
                cfg.CreateMap<Album, AlbumCoverViewModel>()
                    .ForMember(x => x.MainImageUrl, opt => opt.MapFrom(c => $"/api/Image/{c.Photos.FirstOrDefault().Images.FirstOrDefault(i => i.Size == Constants.ImageSize.Thumbnail).Id}"));
                cfg.CreateMap<PhotoViewModel, CameraSettings>()
                    .ForMember(x => x.Id, opt => opt.MapFrom(c => c.CameraSettingsId));
            });
        }
    }
}