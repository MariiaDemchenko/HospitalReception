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

        public IEnumerable<IAlbum> Get(int start, int count)
        {
            start = start < 0 ? 0 : start;
            var albums = _albums.Find(album => true).Skip(start).Limit(count).ToList() as IEnumerable<IAlbum>;

            foreach (var album in albums)
            {
                var photoPath = _photos.Find(photo => album.Photos.Contains(photo.Id))?.ToList()?.Select(p => new { Name = p.ServerName, p.Format })?.FirstOrDefault();
                album.Cover = photoPath != null ? $"{photoPath.Name}.{photoPath.Format}" : "emptyImage.jpg";
            }
            return albums;
        }

        public IAlbum Get(string id)
        {
            return _albums.Find(album => album.Id == id).FirstOrDefault() as IAlbum;
        }

        public string ValidateAlbum(IAlbum albumIn)
        {
            var validationError = string.Empty;
            if (_albums.Find(album => album.AlbumName == albumIn.AlbumName && album.Id != albumIn.Id).Any())
            {
                validationError = "Album name must be unique";
            }
            return validationError;
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
            if (_albums.Find(a => a.AlbumName == albumToReplace.AlbumName).Any())
            {
                return null;
            }
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

        public void RemoveMany(IEnumerable<string> albumsId)
        {
            _albums.DeleteMany(album => albumsId.Contains(album.Id));
        }
        
    }
}