using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.DTOs;

namespace CA_Final_Regia.Interfaces
{
    public interface ILocationGetService
    {
        Task<ResponseDto<LocationDto>> GetLocationAsync(Guid accountId);
    }
}
