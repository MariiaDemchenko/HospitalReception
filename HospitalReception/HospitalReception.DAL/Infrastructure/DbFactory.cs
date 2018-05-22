using System;

namespace HospitalReception.DAL.Infrastructure
{
    public class DbFactory : IDbFactory
    {
        private HospitalReceptionDbContext _dbContext;

        private bool _isDisposed;

        public HospitalReceptionDbContext Init()
        {
            return _dbContext ?? (_dbContext = new HospitalReceptionDbContext());
        }

        protected void DisposeCore()
        {
            _dbContext?.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                DisposeCore();
            }

            _isDisposed = true;
        }
    }
}