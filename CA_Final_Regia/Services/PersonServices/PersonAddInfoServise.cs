using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.DTOs;
using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Interfaces;
using Microsoft.Identity.Client;

namespace CA_Final_Regia.Services.PersonServices
{
    public class PersonAddInfoServise(IPersonRepository personRepository, IPictureToByteService pictureToByteService, IPictureResizeService pictureResizeService, IJwtService jwtService) : IPersonAddInfoServise
    {
        private readonly IPersonRepository _personRepository = personRepository;
        private readonly IPictureToByteService _pictureToByteService = pictureToByteService;
        private readonly IPictureResizeService _pictureResizeService = pictureResizeService;
        private readonly IJwtService _jwtService = jwtService;
        public async Task<ResponseDto<Person>> AddPersonInfoAsync(PersonDto personDto, string token)
        {
            try
            {
                if (Guid.TryParse(_jwtService.ExtractUsernameFromToken(token), out Guid accountId))
                {
                    return new ResponseDto<Person>(false, "Token is invalid", ResponseDto<Person>.Status.Unauthorized);
                }
                if (personDto == null)
                { 
                    return new ResponseDto<Person>(false, "Person not added", ResponseDto<Person>.Status.Bad_Request); 
                }
                PictureDto picture = new PictureDto { Image = personDto.Image };
                var resizedImage = await _pictureResizeService.ResizePictureAsync(picture);
                var imageBytes = await _pictureToByteService.ConvertToByteArrayAsync(resizedImage);
                Person person = new Person
                {
                    AccountId = accountId,
                    FirstName = personDto.FirstName,
                    LastName = personDto.LastName,
                    PersonalId = personDto.PersonalId,
                    PhoneNumber = personDto.PhoneNumber,
                    Mail = personDto.Mail,
                    FileData = imageBytes,
                };
                var response = await _personRepository.CreatePersonAsync(person);
                if (response == null)
                {
                    return new ResponseDto<Person>(false, "Person not added", ResponseDto<Person>.Status.Not_Found);
                }
                return new ResponseDto<Person>(true, "Person added", ResponseDto<Person>.Status.Ok);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
