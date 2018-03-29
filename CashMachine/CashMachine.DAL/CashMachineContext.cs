using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CashMachine.DAL.Models;

namespace CashMachine.DAL
{
    public class CashMachineContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<Operation> Operations { get; set; }

        public CashMachineContext() : base("CashMachineContext")
        {
            Database.SetInitializer(new CashMachineInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}