using HospitalReception.DAL.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace HospitalReception.DAL.Infrastructure
{
    /// <summary>
    /// Контест для работы с базами данных
    /// </summary>
    public interface IHospitalReceptionDbContext
    {
        DbSet<Address> Address { get; set; }
        DbSet<Appointment> Appointments { get; set; }
        DbSet<LocalityType> LocalityTypes { get; set; }
        DbSet<ConsultationHours> ConsultationHours { get; set; }
        DbSet<Department> Depatments { get; set; }
        DbSet<DisabilityGroup> DisabilityGroups { get; set; }
        DbSet<Doctor> Doctors { get; set; }
        DbSet<EducationType> EducationTypes { get; set; }
        DbSet<Gender> Genders { get; set; }
        DbSet<HabitationMember> HabitationMembers { get; set; }
        DbSet<InformationSource> InformationSources { get; set; }
        DbSet<Organization> Organizations { get; set; }
        DbSet<Patient> Patients { get; set; }
        DbSet<Phone> Phones { get; set; }
        DbSet<Region> Region { get; set; }
        int SaveChanges();
        void Dispose();
        DbEntityEntry Entry<TEntity>(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        Database Database { get; }
    }
}