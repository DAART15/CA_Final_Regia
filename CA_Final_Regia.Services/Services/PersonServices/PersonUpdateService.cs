using CA_Final_Regia.Domain.Entities;
using CA_Final_Regia.Domain.Interfaces.Repository;
using CA_Final_Regia.Services.ActionFilters;
using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Interfaces;
namespace CA_Final_Regia.Services.Services.PersonServices
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
                var response = await SwichPersonKeyValue(personUpdateKeyValue, person);
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
                        person.PersonalId = Convert.ToInt64(valueAsString);
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
                await personRepository.UpdatePersonAsync(person);
                return new ResponseDto<Person>(true, "Person updated", ResponseDto<Person>.Status.Created);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<ResponseDto<PictureDto>> UpdatePersonPictureAsync(PictureDto pictureDto, Guid accountId)
        {
            try
            {
                var keyValue = new KeyValue { Key = "Image", Value = pictureDto.Image };
                var validation = keyValue.ValidateKeyValue<PictureDto>();
                if (!validation.IsSuccess)
                {
                    return new ResponseDto<PictureDto>(false, validation.Message, ResponseDto<PictureDto>.Status.Bad_Request);
                }
                var person = await personRepository.GetPersonAsync(accountId);
                if (person == null)
                {
                    return new ResponseDto<PictureDto>(false, "Person not found.", ResponseDto<PictureDto>.Status.Not_Found);
                }
                var keyValueUpdate = new KeyValue { Key = "Image", Value = pictureDto };
                var response = await SwichPersonKeyValue(keyValueUpdate, person);
                if (!response.IsSuccess)
                {
                    return new ResponseDto<PictureDto>(false, response.Message, ResponseDto<PictureDto>.Status.Bad_Request);
                }
                return new ResponseDto<PictureDto>(true, "Person updated", ResponseDto<PictureDto>.Status.Created);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
