namespace PhotoManager.DAL.ProjectionModels
{
    public class AlbumSearchModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public string AlbumUrl
        {
            get { return Id != null ? "/api/albums/" + Id : "/api/albums/album?name=" + Name; }
        }
    }
}