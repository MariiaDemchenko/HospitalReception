using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoManager.DAL.Context;
using PhotoManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using Constants = PhotoManager.Common.Constants;

namespace PhotoManager.DAL.Initializer
{
    public class PhotoManagerInitializer : DropCreateDatabaseIfModelChanges<PhotoManagerDbContext>
    {
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
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
                new CameraSettings {Diaphragm = 0.1, CameraModel="Model0", Flash = 0.5, Iso = 1, LensFocalLength = 2},
                new CameraSettings {Diaphragm = 0.1, CameraModel="Model1", Flash = 0.5, Iso = 1, LensFocalLength = 2},
                new CameraSettings {Diaphragm = 0.1, CameraModel="Model2", Flash = 0.5, Iso = 1, LensFocalLength = 2},
                new CameraSettings {Diaphragm = 0.1, CameraModel="Model3", Flash = 0.5, Iso = 1, LensFocalLength = 2},
                new CameraSettings {Diaphragm = 0.1, CameraModel="Model4", Flash = 0.5, Iso = 1, LensFocalLength = 2},
                new CameraSettings {Diaphragm = 0.1, CameraModel="Model5", Flash = 0.5, Iso = 1, LensFocalLength = 2},
                new CameraSettings {Diaphragm = 0.1, CameraModel="Model6", Flash = 0.5, Iso = 1, LensFocalLength = 2},
                new CameraSettings {Diaphragm = 0.1, CameraModel="Model7", Flash = 0.5, Iso = 1, LensFocalLength = 2},
                new CameraSettings {Diaphragm = 0.1, CameraModel="Model8", Flash = 0.5, Iso = 1, LensFocalLength = 2},
                new CameraSettings {Diaphragm = 0.1, CameraModel="Model9", Flash = 0.5, Iso = 1, LensFocalLength = 2},
                new CameraSettings {Diaphragm = 0.1, CameraModel="Model10", Flash = 0.5, Iso = 1, LensFocalLength = 2},
            };
            cameras.ForEach(c => context.CameraSettings.AddOrUpdate(c));
            context.SaveChanges();

            var userId = context.Users.FirstOrDefault()?.Id;

            var images = new List<Models.Image>
            {
                new Models.Image
                {
                    Bytes = ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\blue.jpg"))),
                    Size = Constants.ImageSize.Original
                },
                new Models.Image
                {
                    Bytes = ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\gray.jpg"))),
                    Size = Constants.ImageSize.Original
                },
                new Models.Image
                {
                    Bytes = ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\green.jpg"))),
                    Size = Constants.ImageSize.Original
                },
                new Models.Image
                {
                    Bytes = ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\pink.jpg"))),
                    Size = Constants.ImageSize.Original
                },
                new Models.Image
                {
                    Bytes = ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\purple.jpg"))),
                    Size = Constants.ImageSize.Original
                },
                new Models.Image
                {
                    Bytes = ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\white.jpg"))),
                    Size = Constants.ImageSize.Original
                },
                new Models.Image
                {
                    Bytes = ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\yellow.jpg"))),
                    Size = Constants.ImageSize.Original
                },
                new Models.Image
                {
                    Bytes = ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\sunset.jpg"))),
                    Size = Constants.ImageSize.Original
                },
                new Models.Image
                {
                    Bytes = ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\mountains.jpg"))),
                    Size = Constants.ImageSize.Original
                },
                new Models.Image
                {
                    Bytes = ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\sky.jpg"))),
                    Size = Constants.ImageSize.Original
                },
                new Models.Image
                {
                    Bytes = ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\forest.jpg"))),
                    Size = Constants.ImageSize.Original
                }
            };

            var photos = new List<Photo>
            {
                new Photo {ImageId = 1, Place = "Place0", OwnerId = userId, Name = "Blue", CreationDate = DateTime.Now, CameraSettingsId = 1},
                new Photo {ImageId = 2, Place = "Place1", OwnerId = userId, Name = "Gray", CreationDate = DateTime.Now, CameraSettingsId = 2},
                new Photo {ImageId = 3, Place = "Place2", OwnerId = userId, Name = "Green", CreationDate = DateTime.Now, CameraSettingsId = 3},
                new Photo {ImageId = 4, Place = "Place3", OwnerId = userId, Name = "Pink", CreationDate = DateTime.Now, CameraSettingsId = 4},
                new Photo {ImageId = 5, Place = "Place4", OwnerId = userId, Name = "Purple", CreationDate = DateTime.Now, CameraSettingsId = 5},
                new Photo {ImageId = 6, Place = "Place5", OwnerId = userId, Name = "White", CreationDate = DateTime.Now, CameraSettingsId = 6},
                new Photo {ImageId = 7, Place = "Place6", OwnerId = userId, Name = "Yellow", CreationDate = DateTime.Now, CameraSettingsId = 7},
                new Photo {ImageId = 8, Place = "Place7", OwnerId = userId, Name = "Sunset", CreationDate = DateTime.Now.AddDays(-1), CameraSettingsId = 8},
                new Photo {ImageId = 9, Place = "Place8", OwnerId = userId, Name = "Mountains", CreationDate = DateTime.Now.AddDays(-1), CameraSettingsId = 9},
                new Photo {ImageId = 10, OwnerId = userId, Name = "Sky", CreationDate = DateTime.Now.AddDays(-1), CameraSettingsId = 10},
                new Photo {ImageId = 11, OwnerId = userId, Name = "Forest", CreationDate = DateTime.Now.AddDays(-1), CameraSettingsId = 11}
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

            images.ForEach(i => context.Images.AddOrUpdate(i));
            photos.ForEach(c => context.Photos.AddOrUpdate(c));
            albums.ForEach(a => context.Albums.AddOrUpdate(a));

            context.SaveChanges();
        }
    }
}