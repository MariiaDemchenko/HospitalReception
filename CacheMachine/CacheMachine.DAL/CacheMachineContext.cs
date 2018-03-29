using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CacheMachine.DAL.Models;

namespace CacheMachine.DAL
{
    public class CacheMachineContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<Operation> Operations { get; set; }

        public CacheMachineContext() : base("CacheMachineContext")
        {
            Database.SetInitializer(new CacheMachineInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}