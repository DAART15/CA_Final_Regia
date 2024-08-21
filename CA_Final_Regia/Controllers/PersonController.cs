using CA_Final_Regia.DTOs;
using CA_Final_Regia.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CA_Final_Regia.Controllers
{
    [Route("api/regia/person")]
    [ApiController]
    [Authorize]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    public class PersonController(IPersonAddInfoServise personAddInfoServise, ILogger<PersonController> logger) : ControllerBase
    {
        private readonly IPersonAddInfoServise _personAddInfoServise = personAddInfoServise;
        private readonly ILogger<PersonController> _logger = logger;

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPersonInfoAsync(PersonDto personDto)
        {
            try
            {
                string authorizationHeader = Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
                {
                    return BadRequest("Invalid Authorization header.");
                }
                string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();


                /*LocationDto locationDto = new LocationDto
                {
                    City = "Miestas",
                    Street = "Gatve",
                    HouseNr = "11",
                    ApartmentNr = "22"
                };*/
                var response = await _personAddInfoServise.AddPersonInfoAsync(personDto, jwtToken);
                return StatusCode((int)response.StatusCode, response.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogCritical(ex, "An error occurred while adding a new person.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding a new person.");
            }
        }
    }
}
