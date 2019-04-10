using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PhotoManagerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoManagerApi.Services
{
    public class AlbumService
    {
        private readonly IMongoCollection<Album> _albums;

        public AlbumService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("PhotosDb"));
            var database = client.GetDatabase("PhotosDb");
            _albums = database.GetCollection<Album>("Albums");
        }

        public List<Album> Get()
        {
            return _albums.Find(album => true).ToList();
        }

        public Album Get(string id)
        {
            return _albums.Find<Album>(album => album.Id == id).FirstOrDefault();
        }

        public Album Create(Album album)
        {
            _albums.InsertOne(album);
            return album;
        }

        public void Update(string id, Album albumIn)
        {
            _albums.ReplaceOne(album => album.Id == id, albumIn);
        }

        public void Remove(Album albumIn)
        {
            _albums.DeleteOne(album => album.Id == albumIn.Id);
        }

        public void Remove(string id)
        {
            _albums.DeleteOne(album => album.Id == id);
        }

    }
}
