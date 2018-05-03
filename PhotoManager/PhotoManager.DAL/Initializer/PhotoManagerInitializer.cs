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
                new CameraSettings {Diaphragm = Constants.Diaphragm.D1, ShutterSpeed = Constants.ShutterSpeed.S1, CameraModel="Model10", Flash = Constants.Flash.F0, Iso = 20, LensFocalLength = 2},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D1, ShutterSpeed = Constants.ShutterSpeed.S1, CameraModel="Model1", Flash = Constants.Flash.F0, Iso = 30, LensFocalLength = 3},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D1, ShutterSpeed = Constants.ShutterSpeed.S1, CameraModel="ModelAAAA", Flash = Constants.Flash.F0, Iso = 20, LensFocalLength = 2},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D1, ShutterSpeed = Constants.ShutterSpeed.S17, CameraModel="ModelABBAA", Flash = Constants.Flash.F5, Iso = 40, LensFocalLength = 3},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D1, ShutterSpeed = Constants.ShutterSpeed.S17, CameraModel="ModelBAA", Flash = Constants.Flash.F4, Iso = 10, LensFocalLength = 2},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D1, ShutterSpeed = Constants.ShutterSpeed.S17, CameraModel="ModelBBBCC", Flash = Constants.Flash.F4, Iso = 10, LensFocalLength = 2},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D3, ShutterSpeed = Constants.ShutterSpeed.S3, CameraModel="ABA", Flash = Constants.Flash.F7, Iso = 10, LensFocalLength = 4},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D3, ShutterSpeed = Constants.ShutterSpeed.S3, CameraModel="BBB", Flash = Constants.Flash.F7, Iso = 10, LensFocalLength = 2},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D3, ShutterSpeed = Constants.ShutterSpeed.S3, CameraModel="5115555", Flash = Constants.Flash.F8, Iso = 10, LensFocalLength = 5},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D3, ShutterSpeed = Constants.ShutterSpeed.S3, CameraModel="1111", Flash = Constants.Flash.F8, Iso = 10, LensFocalLength = 5},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D3, ShutterSpeed = Constants.ShutterSpeed.S3, CameraModel="55555", Flash = Constants.Flash.F0, Iso = 10, LensFocalLength = 5},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D8, ShutterSpeed = Constants.ShutterSpeed.S8, CameraModel="Model0A", Flash = Constants.Flash.F2, Iso = 20, LensFocalLength = 2},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D8, ShutterSpeed = Constants.ShutterSpeed.S8, CameraModel="Model1", Flash = Constants.Flash.F2, Iso = 30, LensFocalLength = 3},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D8, ShutterSpeed = Constants.ShutterSpeed.S8, CameraModel="ModelAC", Flash = Constants.Flash.F2, Iso = 20, LensFocalLength = 2},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D8, ShutterSpeed = Constants.ShutterSpeed.S8, CameraModel="ModelA", Flash = Constants.Flash.F0, Iso = 40, LensFocalLength = 3},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D8, ShutterSpeed = Constants.ShutterSpeed.S15, CameraModel="ModelB", Flash = Constants.Flash.F0, Iso = 10, LensFocalLength = 2},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D8, ShutterSpeed = Constants.ShutterSpeed.S15, CameraModel="ModelB", Flash = Constants.Flash.F0, Iso = 10, LensFocalLength = 2},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D0, ShutterSpeed = Constants.ShutterSpeed.S0, CameraModel="AAA", Flash = Constants.Flash.F3, Iso = 10, LensFocalLength = 4},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D0, ShutterSpeed = Constants.ShutterSpeed.S0, CameraModel="BBB", Flash = Constants.Flash.F3, Iso = 10, LensFocalLength = 2},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D0, ShutterSpeed = Constants.ShutterSpeed.S0, CameraModel="55555", Flash = Constants.Flash.F0, Iso = 10, LensFocalLength = 5},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D0, ShutterSpeed = Constants.ShutterSpeed.S0, CameraModel="1111", Flash = Constants.Flash.F4, Iso = 10, LensFocalLength = 5},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D0, ShutterSpeed = Constants.ShutterSpeed.S0, CameraModel="55555", Flash = Constants.Flash.F0, Iso = 10, LensFocalLength = 5},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D10, ShutterSpeed = Constants.ShutterSpeed.S7, CameraModel="Model0", Flash = Constants.Flash.F2, Iso = 20, LensFocalLength = 2},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D10, ShutterSpeed = Constants.ShutterSpeed.S7, CameraModel="Model1", Flash = Constants.Flash.F2, Iso = 30, LensFocalLength = 3},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D10, ShutterSpeed = Constants.ShutterSpeed.S7, CameraModel="ModelAC", Flash = Constants.Flash.F2, Iso = 20, LensFocalLength = 2},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D10, ShutterSpeed = Constants.ShutterSpeed.S7, CameraModel="ModelA", Flash = Constants.Flash.F5, Iso = 40, LensFocalLength = 3},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D10, ShutterSpeed = Constants.ShutterSpeed.S7, CameraModel="ModelB", Flash = Constants.Flash.F5, Iso = 10, LensFocalLength = 2},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D10, ShutterSpeed = Constants.ShutterSpeed.S7, CameraModel="ModelB", Flash = Constants.Flash.F5, Iso = 10, LensFocalLength = 2},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D5, ShutterSpeed = Constants.ShutterSpeed.S5, CameraModel="AACA", Flash = Constants.Flash.F5, Iso = 10, LensFocalLength = 4},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D5, ShutterSpeed = Constants.ShutterSpeed.S5, CameraModel="BBCB", Flash = Constants.Flash.F5, Iso = 10, LensFocalLength = 2},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D5, ShutterSpeed = Constants.ShutterSpeed.S5, CameraModel="55555", Flash = Constants.Flash.F1, Iso = 10, LensFocalLength = 5},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D5, ShutterSpeed = Constants.ShutterSpeed.S5, CameraModel="1111", Flash = Constants.Flash.F1, Iso = 10, LensFocalLength = 5},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D5, ShutterSpeed = Constants.ShutterSpeed.S5, CameraModel="55555", Flash = Constants.Flash.F1, Iso = 10, LensFocalLength = 5},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D0, ShutterSpeed = Constants.ShutterSpeed.S0, CameraModel="Model0", Flash = Constants.Flash.F1, Iso = 20, LensFocalLength = 2},
                new CameraSettings {Diaphragm = Constants.Diaphragm.D0, ShutterSpeed = Constants.ShutterSpeed.S0, CameraModel="Model1", Flash = Constants.Flash.F1, Iso = 30, LensFocalLength = 3}
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
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\black.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\blue.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\brown.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\colors.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\darkblue.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\gray.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\green.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\lavender.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\limegreen.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\orange.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\pink.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\purple.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\red.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\skyblue.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\sunrize.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\turqoise.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\white.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\whitewash.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\wooden.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\yellow.jpg")))),

                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\clearLake.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\desert.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\forest.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\lake.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\meadow.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\mountains.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\sky.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\smoky.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\sunset.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\volcano.jpg")))),

                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\coffee.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\dessert.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\pears.jpg")))),
                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\raspberries.jpg")))),

                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\building.jpg")))),

                GetImagesInDifferentShapes(Common.Extensions.ImageToByteArray(System.Drawing.Image.FromFile(Path.Combine(path, @"..\Content\Images\emptyImage.jpg"))))
            };

            var photos = new List<Photo>
            {
                //colors
                new Photo {Place = "Place000", OwnerId = userId, Name = "Black", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 1},
                new Photo {Place = "Place010", OwnerId = userId, Name = "Blue", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 2},
                new Photo {Place = "Place200", OwnerId = userId, Name = "Brown", CreationDate = new DateTime(2018, 02, 02, 02, 02, 0), CameraSettingsId = 3},
                new Photo {Place = "Place113", OwnerId = userId, Name = "Colors", CreationDate = new DateTime(2018, 02, 02, 02, 02, 0), CameraSettingsId = 4},
                new Photo {Place = "Place422", OwnerId = userId, Name = "DarkBlue", CreationDate = new DateTime(2018, 02, 02, 02, 02, 0), CameraSettingsId = 5},
                new Photo {Place = "Place522", OwnerId = userId, Name = "Gray", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 6},
                new Photo {Place = "Place600", OwnerId = userId, Name = "Green", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 7},
                new Photo {Place = "Place713", OwnerId = userId, Name = "Lavender", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 8},
                new Photo {Place = "Place858", OwnerId = userId, Name = "Limegreen", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 9},
                new Photo {Place = "Place923", OwnerId = userId, Name = "Orange", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 10},
                new Photo {Place = "Place111", OwnerId = userId, Name = "Pink", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 11},
                new Photo {Place = "Place222", OwnerId = userId, Name = "Purple", CreationDate = new DateTime(2018, 04, 04, 04, 04, 0), CameraSettingsId = 12},
                new Photo {Place = "Place123456", OwnerId = userId, Name = "Red", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 13},
                new Photo {Place = "Place143", OwnerId = userId, Name = "Skyblue", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 14},
                new Photo {Place = "Place8899", OwnerId = userId, Name = "Sunrize", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 15},
                new Photo {Place = "Place015", OwnerId = userId, Name = "Turqoise", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 16},
                new Photo {Place = "Place16", OwnerId = userId, Name = "White", CreationDate = new DateTime(2018, 03, 03, 03, 03, 0), CameraSettingsId = 17},
                new Photo {Place = "Place17", OwnerId = userId, Name = "Whitewash", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId =18},
                new Photo {Place = "Place18", OwnerId = userId, Name = "Wooden", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 19},
                new Photo {Place = "Place019", OwnerId = userId, Name = "Yellow", CreationDate = new DateTime(2018, 02, 02, 02, 02, 0), CameraSettingsId = 20},

                //nature
                new Photo {Place = "Place1", OwnerId = userId, Name = "ClearLake", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 21},
                new Photo {Place = "Place2", OwnerId = userId, Name = "Desert", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 22},
                new Photo {Place = "Place3", OwnerId = userId, Name = "Forest", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 23},
                new Photo {Place = "Place4", OwnerId = userId, Name = "Lake", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 24},
                new Photo {Place = "Place5", OwnerId = userId, Name = "Meadow", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 25},
                new Photo {Place = "Place6", OwnerId = userId, Name = "Mountains", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 26},
                new Photo {Place = "Place7", OwnerId = userId, Name = "Sky", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 27},
                new Photo {Place = "Place8", OwnerId = userId, Name = "Smoky", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 28},
                new Photo {Place = "Place9", OwnerId = userId, Name = "Sunset", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 29},
                new Photo {Place = "Place10", OwnerId = userId,Name = "Volcano", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 30},

                //coffee
                new Photo {Place = "Place1", OwnerId = userId, Name = "Coffee", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 31},

                //food
                new Photo {Place = "Place2", OwnerId = userId, Name = "Dessert", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 32},
                new Photo {Place = "Place3", OwnerId = userId, Name = "Pears", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 33},
                new Photo {Place = "Place4", OwnerId = userId, Name = "Raspberries", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 34},

                //building
                new Photo {Place = "Place1", OwnerId = userId, Name = "Building", CreationDate = new DateTime(2018, 05, 05, 05, 05, 0), CameraSettingsId = 35},
            };

            var albums = new List<Album>
            {
                new Album {Name = "Colors", Description="Whitespace pictures", OwnerId = userId, Photos = new List<Photo>()},
                new Album {Name = "Nature", Description="Pictures of nature", OwnerId = userId, Photos = new List<Photo>()},
                new Album {Name = "Common", Description="Common album", OwnerId = userId, Photos = new List<Photo>()},
                new Album {Name = "Fruits", Description="Fruits pictures", OwnerId = userId, Photos = new List<Photo>()},
                new Album {Name = "Lakes", Description="Lakes album", OwnerId = userId, Photos = new List<Photo>()},
                new Album {Name = "Buildings", Description="Some buildings", OwnerId = userId, Photos = new List<Photo>()},
                new Album {Name = "Sky", Description="Shy photos", OwnerId = userId, Photos = new List<Photo>()},
                new Album {Name = "Mountains", Description="Mountains pictures", OwnerId = userId, Photos = new List<Photo>()},
                new Album {Name = "Forests", Description="Forest album", OwnerId = userId, Photos = new List<Photo>()},
                new Album {Name = "Food", Description="Some food", OwnerId = userId, Photos = new List<Photo>()},
                new Album {Name = "Coffee", Description="Coffee photos", OwnerId = userId, Photos = new List<Photo>()},
            };

            for (var i = 0; i < 20; i++)
            {
                albums[0].Photos.Add(photos[i]);
            }

            for (var i = 20; i < 30; i++)
            {
                albums[1].Photos.Add(photos[i]);
            }

            photos.ForEach(p => albums[2].Photos.Add(p));

            for (var i = 32; i < 34; i++)
            {
                albums[3].Photos.Add(photos[i]);
            }

            albums[4].Photos.Add(photos[23]);
            albums[5].Photos.Add(photos[34]);
            albums[6].Photos.Add(photos[26]);
            albums[7].Photos.Add(photos[25]);
            albums[8].Photos.Add(photos[22]);

            for (var i = 31; i < 34; i++)
            {
                albums[9].Photos.Add(photos[i]);
            }

            albums[10].Photos.Add(photos[30]);

            for (var i = 0; i < 35; i++)
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