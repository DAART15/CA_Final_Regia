using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.Infrastructure.DataBase;
using CA_Final_Regia.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace CA_Final_Regia.Infrastructure.Repositories
{
    public class LocationRepository(AplicationDbContext dbContext) : ILocationRepository
    {
        private readonly AplicationDbContext _dbContext = dbContext;
        public async Task<Location?> GetLocationAsync(Guid accountId)
        {
            try
            {
                return await _dbContext.Locations.FirstOrDefaultAsync(l => l.AccountId == accountId);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Location> CreateLocationAsync(Location location)
        {
            try
            {
                await _dbContext.Locations.AddAsync(location);
                await _dbContext.SaveChangesAsync();
                return location;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
