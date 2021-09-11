using System.Data.Entity;
using HospitalReception.DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HospitalReception.DAL
{
    public class HospitalReceptionDbContext : IdentityDbContext<ApplicationUser>
    {
        public HospitalReceptionDbContext()
            : base("HospitalReception", throwIfV1Schema: false)
        {
            Database.SetInitializer<HospitalReceptionDbContext>(null);
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<LocalityType> LocalityTypes { get; set; }
        public DbSet<ConsultationHours> ConsultationHours { get; set; }
        public DbSet<Department> Depatments { get; set; }
        public DbSet<DisabilityGroup> DisabilityGroups { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<EducationType> EducationTypes { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<HabitationMember> HabitationMembers { get; set; }
        public DbSet<InformationSource> InformationSources { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Region> Region { get; set; }

        public static HospitalReceptionDbContext Create()
        {    
            return new HospitalReceptionDbContext();
        }
    }
}
