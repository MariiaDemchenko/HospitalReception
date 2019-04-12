using MongoDB.Driver;
using PhotoManager.BLL.Models;
using PhotoManager.BLL.Repositories;
using PhotoManager.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace PhotoManager.DAL.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly IMongoCollection<Photo> _photos;
        private readonly IMongoCollection<Album> _albums;
        private readonly IMongoClient _client;

        public PhotoRepository(string connectionString)
        {
            _client = new MongoClient(connectionString);
            var database = _client.GetDatabase("PhotosDb");
            _photos = database.GetCollection<Photo>("Photos");
            _albums = database.GetCollection<Album>("Albums");
        }

        public IEnumerable<IPhoto> GetByAlbumId(int start, int count, string albumId)
        {
            start = start < 0 ? 0 : start;
            var photosId = _albums.Find(album => album.Id == albumId).FirstOrDefault().Photos;
            return _photos.Find(photo => photosId.Contains(photo.Id)).Skip(start).Limit(count).ToList() as IEnumerable<IPhoto>;
        }

        public IEnumerable<IPhoto> GetAll(int start, int count)
        {
            start = start < 0 ? 0 : start;
            var photos = _photos.Find(photo => true).Skip(start).Limit(count).ToList() as IEnumerable<IPhoto>;
            foreach (var photo in photos)
            {
                photo.Albums = _albums.Find(album => album.Photos.Contains(photo.Id)).ToList().Select(album => album.Id).ToList();
            }

            return photos;
        }

        public IPhoto Get(string id)
        {
            return _photos.Find(photo => photo.Id == id).FirstOrDefault() as IPhoto;
        }

        public IPhoto Create(IPhoto photo, string albumId)
        {
            var photoToReplace = new Photo
            {
                PhotoName = photo.PhotoName,
                ServerName = photo.ServerName,
                ShotDate = photo.ShotDate,
                CameraModel = photo.CameraModel,
                Format = photo.Format,
                Owner = photo.Owner,
                LensFocalLength = photo.LensFocalLength
            };
            var albumToReplace = _albums.Find(album => album.Id == albumId).FirstOrDefault();

            using (var session = _client.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    _photos.InsertOne(photoToReplace);
                    albumToReplace.Photos.Add(photoToReplace.Id);
                    _albums.ReplaceOne(album => album.Id == albumToReplace.Id, albumToReplace);
                    session.CommitTransaction();
                }
                catch
                {
                    session.AbortTransaction();
                }

            }

            return photoToReplace;
        }

        public void Update(string id, IPhoto photoIn)
        {
            var photoToReplace = new Photo
            {
                Id = photoIn.Id,
                PhotoName = photoIn.PhotoName,
                ServerName = photoIn.ServerName,
                ShotDate = photoIn.ShotDate,
                CameraModel = photoIn.CameraModel,
                Format = photoIn.Format,
                LensFocalLength = photoIn.LensFocalLength
            };
            _photos.ReplaceOne(photo => photo.Id == id, photoToReplace);
        }

        public void Remove(IPhoto photo, string albumId)
        {
            var albumToReplace = _albums.Find(album => album.Id == albumId).FirstOrDefault();

            using (var session = _client.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    albumToReplace.Photos.Remove(photo.Id);
                    _albums.ReplaceOne(album => album.Id == albumToReplace.Id, albumToReplace);
                    _photos.DeleteOne(p => p.Id == photo.Id);
                    session.CommitTransaction();
                }
                catch
                {
                    session.AbortTransaction();
                }
            }
        }
    }
}