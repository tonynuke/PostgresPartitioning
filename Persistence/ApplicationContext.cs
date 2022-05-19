using Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence
{
    public sealed class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
        }

        public DbSet<Person> Persons { get; internal set; }

        public DbSet<Purchase> Purchases { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
