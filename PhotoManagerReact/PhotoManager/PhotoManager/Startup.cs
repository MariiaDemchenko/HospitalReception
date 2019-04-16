using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PhotoManager.AutomapperProfiles;
using PhotoManager.BLL.Repositories;
using PhotoManager.DAL.Repositories;
using PhotoManager.Extensions;

namespace PhotoManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.ConfigureJwtHandler(Configuration);

            var mediaServerUrl = Configuration.GetSection("MediaServerSettings").GetSection("MediaServerUrl").Value;
            var imagesFolder = Configuration.GetSection("MediaServerSettings").GetSection("ImagesCatalog").Value;
            var thumbsFolder = Configuration.GetSection("MediaServerSettings").GetSection("ThumbsCatalog").Value;

            var imagesPath = mediaServerUrl + imagesFolder;
            var thumbsPath = mediaServerUrl + thumbsFolder;

            var automapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AlbumProfile(imagesPath, thumbsPath));
                mc.AddProfile(new PhotoProfile(imagesPath, thumbsPath));
                mc.AddProfile(new UserProfile(imagesPath, thumbsPath));
            });

            var mapper = automapperConfig.CreateMapper();
            var hashcodeHelper = new HashcodeHelper();

            services.AddSingleton(mapper);
            services.AddSingleton<IHashcodeHelper>(new HashcodeHelper());

            var connectionString = Configuration.GetConnectionString("PhotosDb");

            services.AddScoped<IAlbumRepository>(provider => new AlbumRepository(connectionString));
            services.AddScoped<IPhotoRepository>(provider => new PhotoRepository(connectionString));
            services.AddScoped<IUserRepository>(provider => new UserRepository(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            var logger = loggerFactory.CreateLogger("PhotoManager");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}