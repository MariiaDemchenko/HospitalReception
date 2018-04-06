using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PhotoManager.DAL.Models;
using PhotoManager.ViewModels.PhotoManagerViewModels;

namespace PhotoManager.Profiles
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile()
        {
            CreateMap<Album, AlbumViewModel>().ForMember(x => x.MainImageUrl, opt => opt.MapFrom(c => $"/api/Image/{c.Photos.FirstOrDefault().ImageId}"));
        }
    }
}