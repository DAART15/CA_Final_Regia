using CA_Final_Regia.Domain.Models;
namespace CA_Final_Regia.Infrastructure.Interfaces
{
    public interface ILocationRepository
    {
        Task<Location?> GetLocationAsync(Guid accountId);
        Task<Location> CreateLocationAsync(Location location);
        Task<Location> UpdateLocationAsync(Location location);
    }
}
