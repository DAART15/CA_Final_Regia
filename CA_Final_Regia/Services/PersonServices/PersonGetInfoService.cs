using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.DTOs;
using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Interfaces;
namespace CA_Final_Regia.Services.PersonServices
{
    public class PersonGetInfoService(IPersonRepository personRepository) : IPersonGetInfoService
    {
        private readonly IPersonRepository _personRepository = personRepository;
        public async Task<ResponseDto<Person>> GetPersonInfoAsync(Guid accountId)
        {
            try
            {
                var person = await _personRepository.GetPersonAsync(accountId);
                if (person == null)
                {
                    return new ResponseDto<Person>(false, "Person not found.", ResponseDto<Person>.Status.Not_Found);
                }
                return new ResponseDto<Person>(true, person, ResponseDto<Person>.Status.Ok);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
