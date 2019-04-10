using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PhotoManager.AutomapperProfiles;
using PhotoManager.BLL.Repositories;
using PhotoManager.DAL.Repositories;
using System;
using System.Text;

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "PhotoManagerIssuer",
                    ValidAudience = "PhotoManagerAudience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("IssuerSigningSecretKey")),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            var mediaServerPath = Configuration.GetSection("MediaServerPath").Value;
            var imagesFolder = Configuration.GetSection("ImagesCatalog").Value;
            var thumbsFolder = Configuration.GetSection("ThumbsCatalog").Value;

            var automapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AlbumProfile(mediaServerPath + imagesFolder, mediaServerPath + thumbsFolder));
                mc.AddProfile(new PhotoProfile(mediaServerPath + imagesFolder, mediaServerPath + thumbsFolder));
                mc.AddProfile(new UserProfile(mediaServerPath + imagesFolder, mediaServerPath + thumbsFolder));
            });

            var mapper = automapperConfig.CreateMapper();
            var hashcodeHelper = new HashcodeHelper();

            services.AddSingleton(mapper);
            services.AddSingleton<IHashcodeHelper>(new HashcodeHelper());

            services.AddScoped<IAlbumRepository>(provider => new AlbumRepository(Configuration.GetConnectionString("PhotosDb")));
            services.AddScoped<IPhotoRepository>(provider => new PhotoRepository(Configuration.GetConnectionString("PhotosDb")));
            services.AddScoped<IUserRepository>(provider => new UserRepository(Configuration.GetConnectionString("PhotosDb")));
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