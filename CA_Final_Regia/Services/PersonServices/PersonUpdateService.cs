using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.DTOs;
using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Interfaces;
using CA_Final_Regia.Properties.ActionFilters;

namespace CA_Final_Regia.Services.PersonServices
{
    public class PersonUpdateService(IPersonRepository personRepository, IPictureResizeService pictureResizeService) : IPersonUpdateService
    {
        public async Task<ResponseDto<PersonPostDto>> UpdatePersonAsync(KeyValue personUpdateKeyValue, Guid accountId)
        {
            try
            {
                var validation = personUpdateKeyValue.ValidateKeyValue<PersonPostDto>();
                if (!validation.IsSuccess)
                {
                    return new ResponseDto<PersonPostDto>(false, validation.Message, ResponseDto<PersonPostDto>.Status.Bad_Request);
                }
                var person = await personRepository.GetPersonAsync(accountId);
                if (person == null)
                {
                    return new ResponseDto<PersonPostDto>(false, "Person not found.", ResponseDto<PersonPostDto>.Status.Not_Found);
                }
                var response  = await SwichPersonKeyValue(personUpdateKeyValue, person);
                if (!response.IsSuccess)
                {
                    return new ResponseDto<PersonPostDto>(false, response.Message, ResponseDto<PersonPostDto>.Status.Bad_Request);
                }
                return new ResponseDto<PersonPostDto>(true, "Person updated", ResponseDto<PersonPostDto>.Status.Created);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        private async Task<ResponseDto<Person>> SwichPersonKeyValue(KeyValue personUpdateKeyValue, Person person)
        {
            try
            {
                string valueAsString = personUpdateKeyValue.Value.ToString();
                switch (personUpdateKeyValue.Key)
                {
                    case "FirstName":
                        person.FirstName = valueAsString;
                        break;
                    case "LastName":
                        person.LastName = valueAsString;
                        break;
                    case "PersonalId":
                        person.PersonalId = Convert.ToInt64(personUpdateKeyValue.Value);
                        break;
                    case "PhoneNumber":
                        person.PhoneNumber = valueAsString;
                        break;
                    case "Mail":
                        person.Mail = valueAsString;
                        break;
                    case "Image":
                        person.FileData = await pictureResizeService.ResizePictureAsync((PictureDto)personUpdateKeyValue.Value);
                        break;
                    default:
                        return new ResponseDto<Person>(false, "Bad Key", ResponseDto<Person>.Status.Bad_Request);
                }
                var response = await personRepository.UpdatePersonAsync(person);
                if (response == null)
                {
                    return new ResponseDto<Person>(false, "Person not updated", ResponseDto<Person>.Status.Internal_Server_Error);
                }
                return new ResponseDto<Person>(true, "Person updated", ResponseDto<Person>.Status.Created);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
