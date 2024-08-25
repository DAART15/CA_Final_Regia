using CA_Final_Regia.DTOs;
using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Interfaces;
namespace CA_Final_Regia.Services.PersonServices
{
    public class PersonGetInfoService(IPersonRepository personRepository) : IPersonGetInfoService
    {
        public async Task<ResponseDto<PersonGetDto>> GetPersonInfoAsync(Guid accountId)
        {
            try
            {
                var person = await personRepository.GetPersonAsync(accountId);
                if (person == null)
                {
                    return new ResponseDto<PersonGetDto>(false, "Person not found.", ResponseDto<PersonGetDto>.Status.Not_Found);
                }
                PersonGetDto personGetDto = new PersonGetDto
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    PersonalId = person.PersonalId,
                    PhoneNumber = person.PhoneNumber,
                    Mail = person.Mail,
                    FileData = person.FileData
                };
                return new ResponseDto<PersonGetDto>(true, personGetDto, ResponseDto<PersonGetDto>.Status.Ok);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
