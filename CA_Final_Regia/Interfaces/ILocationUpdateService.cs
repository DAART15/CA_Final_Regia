using CA_Final_Regia.DTOs;

namespace CA_Final_Regia.Interfaces
{
    public interface ILocationUpdateService
    {
        Task<ResponseDto<LocationDto>> UpdateLocationAsync(KeyValue locationUpdateKeyValue, Guid accountId);
    }
}
