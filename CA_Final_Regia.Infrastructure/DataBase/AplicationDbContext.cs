using Microsoft.EntityFrameworkCore;
using CA_Final_Regia.Domain.Models;

namespace CA_Final_Regia.Infrastructure.DataBase
{
    internal class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
