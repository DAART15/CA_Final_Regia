using CA_Final_Regia.Domain.Entities;
namespace CA_Final_Regia.Domain.Interfaces.Repository
{
    public interface ILocationRepository
    {
        Task<Location?> GetLocationAsync(Guid accountId);
        Task CreateLocationAsync(Location location);
        Task UpdateLocationAsync(Location location);
    }
}
