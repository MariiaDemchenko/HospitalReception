using System;

namespace HospitalReception.DAL.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        HospitalReceptionDbContext Init();
    }
}