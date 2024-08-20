using Microsoft.EntityFrameworkCore;
using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.Infrastructure.DataBase.Configuration;

namespace CA_Final_Regia.Infrastructure.DataBase
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Location> Locations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());

        }
    }
}
