namespace PhotoManager.DAL.Contracts
{
    public interface IUnitOfWork
    {
        IPhotoRepository Photos { get; set; }

        IAlbumRepository Albums { get; set; }

        void Save();

        void Dispose();
    }
}
