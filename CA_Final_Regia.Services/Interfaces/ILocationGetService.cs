using CA_Final_Regia.Services.DTOs;
namespace CA_Final_Regia.Services.Interfaces
{
    public interface ILocationGetService
    {
        Task<ResponseDto<LocationDto>> GetLocationAsync(Guid accountId);
    }
}
