using CA_Final_Regia.Domain.Entities;
using CA_Final_Regia.Services.DTOs;
namespace CA_Final_Regia.Services.Interfaces
{
    public interface ILocationAddService
    {
        Task<ResponseDto<LocationDto>> AddLocationAsync(LocationDto locationDto, Guid accountId);
    }
}
