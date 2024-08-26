using CA_Final_Regia.Services.DTOs;
namespace CA_Final_Regia.Services.Interfaces
{
    public interface ILocationUpdateService
    {
        Task<ResponseDto<LocationDto>> UpdateLocationAsync(KeyValue locationUpdateKeyValue, Guid accountId);
    }
}
