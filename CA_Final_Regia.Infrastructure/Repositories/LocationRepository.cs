using CA_Final_Regia.Domain.Entities;
using CA_Final_Regia.Domain.Interfaces.Repository;
using CA_Final_Regia.Infrastructure.DataBase;
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
        public async Task CreateLocationAsync(Location location)
        {
            try
            {
                await _dbContext.Locations.AddAsync(location);
                await _dbContext.SaveChangesAsync();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task UpdateLocationAsync(Location location)
        {
            try
            {
                _dbContext.Locations.Update(location);
                await _dbContext.SaveChangesAsync();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
