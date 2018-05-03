using AutoMapper;
using PhotoManager.DAL.Profiles;
using PhotoManager.Profiles;

namespace PhotoManager
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<PhotoProfile>();
                cfg.AddProfile<ProjectionsProfile>();
            });
        }
    }
}