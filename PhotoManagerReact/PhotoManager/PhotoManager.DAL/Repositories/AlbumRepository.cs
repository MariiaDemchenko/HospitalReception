using MongoDB.Driver;
using PhotoManager.BLL.Models;
using PhotoManager.BLL.Repositories;
using PhotoManager.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace PhotoManager.DAL.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly IMongoCollection<Album> _albums;
        private readonly IMongoCollection<Photo> _photos;

        public AlbumRepository(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("PhotosDb");
            _albums = database.GetCollection<Album>("Albums");

            _photos = database.GetCollection<Photo>("Photos");
        }

        public IEnumerable<IAlbum> Get(int start, int? count = null)
        {
            start = start < 0 ? 0 : start;
            var albums = count == null ? _albums.Find(album => true).Skip(start):
                _albums.Find(album => true).Skip(start).Limit(count);

            return albums.ToList();
        }

        public IAlbum Create(IAlbum album)
        {
            var albumToReplace = new Album()
            {
                Id = album.Id,
                Cover = album.Cover,
                AlbumName = album.AlbumName,
                Description = album.Description,
                Owner = album.Owner,
                Photos = album.Photos
            };
            _albums.InsertOne(albumToReplace as Album);
            return album;
        }

        public void Update(string id, IAlbum albumIn)
        {
            var albumToReplace = new Album()
            {
                Id = albumIn.Id,
                Cover = albumIn.Cover,
                AlbumName = albumIn.AlbumName,
                Description = albumIn.Description,
                Owner = albumIn.Owner,
                Photos = albumIn.Photos
            };
            _albums.ReplaceOne(album => album.Id == id, albumToReplace);
        }

        public void Remove(string albumId)
        {
            _albums.DeleteOne(album => album.Id == albumId);
        }
    }
}