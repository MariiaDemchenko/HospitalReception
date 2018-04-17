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
        protected override void Seed(PhotoManagerDbContext context)
        {
            var path = AppDomain.CurrentDomain.RelativeSearchPath;

            if (!context.Users.Any(u => u.UserName == "test@login.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "test@login.com" };

                manager.Create(user, "123456Abc*");
            }

            var cameras = new List<CameraSettings>
            {
                new CameraSettings {Diaphragm = 0.1, CameraModel="Model0", Flash = 0.5, Iso = 20, LensFocalLength = 2},
                new CameraSettings {Diaphragm = 0.1, CameraModel="Model1", Flash = 0.5, Iso = 30, LensFocalLength = 3},
                new CameraSettings {Diaphragm = 0.1, CameraModel="ModelA", Flash = 0.5, Iso = 20, LensFocalLength = 2},
                new CameraSettings {Diaphragm = 0.1, CameraModel="ModelA", Flash = 0.7, Iso = 40, LensFocalLength = 3},
                new CameraSettings {Diaphragm = 0.1, CameraModel="ModelB", Flash = 0.7, Iso = 10, LensFocalLength = 2},
                new CameraSettings {Diaphragm = 0.1, CameraModel="ModelB", Flash = 0.7, Iso = 10, LensFocalLength = 2},
                new CameraSettings {Diaphragm = 0.5, CameraModel="AAA", Flash = 0.7, Iso = 10, LensFocalLength = 4},
                new CameraSettings {Diaphragm = 0.5, CameraModel="BBB", Flash = 0.1, Iso = 10, LensFocalLength = 2},
                new CameraSettings {Diaphragm = 0.5, CameraModel="55555", Flash = 0.8, Iso = 10, LensFocalLength = 5},
                new CameraSettings {Diaphragm = 0.5, CameraModel="1111", Flash = 0.8, Iso = 10, LensFocalLength = 5},
                new CameraSettings {Diaphragm = 0.5, CameraModel="55555", Flash = 0.1, Iso = 10, LensFocalLength = 5},
            };
            cameras.ForEach(c => context.CameraSettings.AddOrUpdate(c));
            context.SaveChanges();

            var userId = context.Users.FirstOrDefault()?.Id;

            var guids = new List<string>();
            for (var i = 0; i < 11; i++)
            {
                guids.Add(new Guid().ToString("N"));
            }

            var images = new List<IEnumerable<Image>>
            {
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\blue.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\gray.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\green.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\pink.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\purple.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\white.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\yellow.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\sunset.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\mountains.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\sky.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\forest.jpg"))))
            };

            var photos = new List<Photo>
            {
                new Photo {Place = "Place0", OwnerId = userId, Name = "Blue", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 1},
                new Photo {Place = "Place1", OwnerId = userId, Name = "Gray", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 2},
                new Photo {Place = "Place2", OwnerId = userId, Name = "Green", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 3},
                new Photo {Place = "Place3", OwnerId = userId, Name = "Pink", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 4},
                new Photo {Place = "Place4", OwnerId = userId, Name = "Purple", CreationDate = new DateTime(2018, 04, 04, 04, 04, 0), CameraSettingsId = 5},
                new Photo {Place = "Place5", OwnerId = userId, Name = "White", CreationDate = new DateTime(2018, 03, 03, 03, 03, 0), CameraSettingsId = 6},
                new Photo {Place = "Place6", OwnerId = userId, Name = "Yellow", CreationDate = new DateTime(2018, 02, 02, 02, 02, 0), CameraSettingsId = 7},
                new Photo {Place = "Place7", OwnerId = userId, Name = "Sunset", CreationDate = new DateTime(2018, 02, 02, 02, 02, 0), CameraSettingsId = 8},
                new Photo {Place = "Place8", OwnerId = userId, Name = "Mountains", CreationDate = new DateTime(2018, 01, 01, 01, 01, 0), CameraSettingsId = 9},
                new Photo {OwnerId = userId, Name = "Sky", CreationDate = new DateTime(2018, 01, 01, 01, 01, 0), CameraSettingsId = 10},
                new Photo { OwnerId = userId, Name = "Forest", CreationDate = new DateTime(2018, 01, 01, 01, 01, 0), CameraSettingsId = 11}
            };

            var albums = new List<Album>
            {
                new Album {Name = "Colors", Description="Whitespace pictures", OwnerId = userId, Photos = new List<Photo>()},
                new Album {Name = "Nature", Description="Pictures of nature", OwnerId = userId, Photos = new List<Photo>()},
                new Album {Name = "Common", Description="Common album", OwnerId = userId, Photos = new List<Photo>()}
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

            for (var i = 0; i < 11; i++)
            {
                photos[i].Images = new List<Image>();
                photos[i].Images.AddRange(images[i]);
            }

            photos.ForEach(c => context.Photos.AddOrUpdate(c));
            albums.ForEach(a => context.Albums.AddOrUpdate(a));

            context.SaveChanges();
            var fullPath = Path.Combine(path, @"..\..\PhotoManager.DAL\Context\Procedures.sql");

            context.Database.ExecuteSqlCommand(File.ReadAllText(fullPath));
        }

        private IEnumerable<Image> GetImagesInDifferentShapes(byte[] imageOriginal)
        {
            byte[] imageObjectMedium;
            byte[] imageObjectThumbnail;

            using (var stream = new MemoryStream(imageOriginal))
            {
                imageObjectThumbnail = Common.Extensions.ResizeImage(stream, 250);
                imageObjectMedium = Common.Extensions.ResizeImage(stream, 300);
            }

            return new List<Image>
            {
                new Image
                {
                    Bytes = imageObjectThumbnail,
                    Size = Constants.ImageSize.Thumbnail
                },
                new Image
                {
                    Bytes = imageObjectMedium,
                    Size = Constants.ImageSize.Medium
                },
                new Image
                {
                    Bytes = imageOriginal,
                    Size = Constants.ImageSize.Original
                }
            };
        }
    }
}