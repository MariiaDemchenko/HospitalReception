using HospitalReception.DAL.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace HospitalReception.DAL.Infrastructure
{
    public interface IHospitalReceptionDbContext
    {
        DbSet<Patient> Patients { get; set; }
        DbSet<Doctor> Doctors { get; set; }
        DbSet<Department> Depatments { get; set; }
        DbSet<ConsultaionHours> ConsultaionHours { get; set; }
        DbSet<Appointment> Appointments { get; set; }

        int SaveChanges();
        void Dispose();
        DbEntityEntry Entry<TEntity>(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        Database Database { get; }
    }
}