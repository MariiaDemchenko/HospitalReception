﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;

namespace PhotoManager.DAL
{
    public class PhotoManagerInitializer : DropCreateDatabaseIfModelChanges<PhotoManagerContext>
    {
        public byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }

        protected override void Seed(PhotoManagerContext context)
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
                Image.FromFile(Path.Combine(path, @"..\Content\Images\green.jpg"))
            };

            var photos = new List<Photo>();
            images.ForEach(i => photos.Add(new Photo { Image = ImageToByteArray(i), OwnerId = userId, CameraSettingsId = cameraSettingsId }));

            var albums = new List<Album>
            {
                new Album {Name = "TestAlbum", OwnerId = userId},
                new Album {Name = "Backgrounds", OwnerId = userId}
            };

            var photo2Albums = new List<Photo2Album>
            {
                new Photo2Album {PhotoId = 1, AlbumId = 1},
                new Photo2Album {PhotoId = 1, AlbumId = 2},
                new Photo2Album {PhotoId = 2, AlbumId = 1}
            };

            photos.ForEach(c => context.Photos.AddOrUpdate(c));
            albums.ForEach(a => context.Albums.AddOrUpdate(a));
            photo2Albums.ForEach(pa => context.Photo2Albums.AddOrUpdate(pa));

            context.SaveChanges();
        }
    }
}