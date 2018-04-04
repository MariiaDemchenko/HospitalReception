using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoManager.DAL.Context;
using PhotoManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;

namespace PhotoManager.DAL.Initializer
{
    public class PhotoManagerInitializer : DropCreateDatabaseIfModelChanges<PhotoManagerDbContext>
    {
        public byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }

        protected override void Seed(PhotoManagerDbContext context)
        {
            var appDomain = AppDomain.CurrentDomain;
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? appDomain.BaseDirectory;

            if (!context.Users.Any(u => u.UserName == "test"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "test" };

                manager.Create(user, "123abc*");
            }

            var cameras = new List<CameraSettings>
            {
                new CameraSettings {Diaphragm = 1}
            };
            cameras.ForEach(c => context.CameraSettings.AddOrUpdate(c));
            context.SaveChanges();

            var userId = context.Users.FirstOrDefault()?.Id;
            var cameraSettingsId = context.CameraSettings.FirstOrDefault().Id;

            var images = new List<Image>
            {
                Image.FromFile(Path.Combine(path, @"..\Content\Images\blue.jpg")),
                Image.FromFile(Path.Combine(path, @"..\Content\Images\gray.jpg")),
                Image.FromFile(Path.Combine(path, @"..\Content\Images\green.jpg")),
                Image.FromFile(Path.Combine(path, @"..\Content\Images\pink.jpg")),
                Image.FromFile(Path.Combine(path, @"..\Content\Images\purple.jpg")),
                Image.FromFile(Path.Combine(path, @"..\Content\Images\white.jpg")),
                Image.FromFile(Path.Combine(path, @"..\Content\Images\yellow.jpg")),
                Image.FromFile(Path.Combine(path, @"..\Content\Images\sunset.jpg")),
                Image.FromFile(Path.Combine(path, @"..\Content\Images\mountains.jpg")),
                Image.FromFile(Path.Combine(path, @"..\Content\Images\sky.jpg")),
                Image.FromFile(Path.Combine(path, @"..\Content\Images\forest.jpg"))
            };

            var photos = new List<Photo>
            {
                new Photo {Image = ImageToByteArray(images[0]), OwnerId = userId, Name = "Blue", CreationDate = DateTime.Now, CameraSettingsId = cameraSettingsId},
                new Photo {Image = ImageToByteArray(images[1]), OwnerId = userId, Name = "Gray", CreationDate = DateTime.Now, CameraSettingsId = cameraSettingsId},
                new Photo {Image = ImageToByteArray(images[2]), OwnerId = userId, Name = "Green",CreationDate = DateTime.Now,  CameraSettingsId = cameraSettingsId},
                new Photo {Image = ImageToByteArray(images[3]), OwnerId = userId, Name = "Pink", CreationDate = DateTime.Now, CameraSettingsId = cameraSettingsId},
                new Photo {Image = ImageToByteArray(images[4]), OwnerId = userId, Name = "Purple", CreationDate = DateTime.Now, CameraSettingsId = cameraSettingsId},
                new Photo {Image = ImageToByteArray(images[5]), OwnerId = userId, Name = "White",CreationDate = DateTime.Now, CameraSettingsId = cameraSettingsId},
                new Photo
                {
                    Image = ImageToByteArray(images[6]),
                    Name = "Yellow",
                    OwnerId = userId,
                    CreationDate = DateTime.Now,
                    CameraSettingsId = cameraSettingsId,
                },
                new Photo
                {
                    Image = ImageToByteArray(images[7]),
                    Name = "Sunset",
                    OwnerId = userId,
                    CreationDate = DateTime.Now.AddDays(-1),
                    CameraSettingsId = cameraSettingsId
                },
                new Photo
                {
                    Image = ImageToByteArray(images[8]),
                    Name = "Mountains",
                    OwnerId = userId,
                    CreationDate = DateTime.Now.AddDays(-1),
                    CameraSettingsId = cameraSettingsId
                },
                new Photo
                    { Image = ImageToByteArray(images[9]),
                        Name = "Sky",
                        OwnerId = userId,
                        CreationDate = DateTime.Now.AddDays(-1),
                        CameraSettingsId = cameraSettingsId
                    },
                new Photo
                {
                    Image = ImageToByteArray(images[10]),
                    Name = "Forest",
                    OwnerId = userId,
                    CreationDate = DateTime.Now.AddDays(-1),
                    CameraSettingsId = cameraSettingsId
                }
            };

            var albums = new List<Album>
            {
                new Album {Name = "Colors", OwnerId = userId, Photos = new List<Photo>()},
                new Album {Name = "Nature", OwnerId = userId, Photos = new List<Photo>()},
                new Album {Name = "Common", OwnerId = userId, Photos = new List<Photo>()}
            };

            for (var i = 0; i < 7; i++)
            {
                albums[0].Photos.Add(photos[i]);
            }


            for (var i = 7; i < 11; i++)
            {
                albums[1].Photos.Add(photos[i]);
            }

            photos.ForEach(p => albums[2].Photos.Add(p));

            photos.ForEach(c => context.Photos.AddOrUpdate(c));
            albums.ForEach(a => context.Albums.AddOrUpdate(a));

            context.SaveChanges();
        }
    }
}