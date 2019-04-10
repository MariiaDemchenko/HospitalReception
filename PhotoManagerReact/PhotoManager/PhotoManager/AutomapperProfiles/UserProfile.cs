using AutoMapper;
using PhotoManager.Models;
using PhotoManager.ViewModels;

namespace PhotoManager.AutomapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile(string imagesPath, string thumbsPath)
        {
            CreateMap<UserViewModel, User>();
        }
    }
}