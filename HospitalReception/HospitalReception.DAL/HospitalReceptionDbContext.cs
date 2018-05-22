using System.Data.Entity;
using System.Net.Mime;
using HospitalReception.DAL.Infrastructure;
using HospitalReception.DAL.Initializer;
using HospitalReception.DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HospitalReception.DAL
{
    public class HospitalReceptionDbContext : IdentityDbContext<ApplicationUser>
    {
        public HospitalReceptionDbContext()
            : base("HospitalReception", throwIfV1Schema: false)
        {
            Database.SetInitializer(new HospitalReceptionInitializer());
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Depatments { get; set; }
        public DbSet<ConsultaionHours> ConsultaionHours { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public static HospitalReceptionDbContext Create()
        {
            return new HospitalReceptionDbContext();
        }
    }
}
