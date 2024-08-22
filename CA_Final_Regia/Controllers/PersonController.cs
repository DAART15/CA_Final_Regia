using CA_Final_Regia.DTOs;
using CA_Final_Regia.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CA_Final_Regia.Controllers
{
    [Route("regia/person")]
    [ApiController]
    [Authorize]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    public class PersonController(IPersonAddInfoService personAddInfoServise, ILogger<PersonController> logger, IJwtExtraxtService jwtExtraxtSerevice) : ControllerBase
    {
        private readonly IPersonAddInfoService _personAddInfoServise = personAddInfoServise;
        private readonly ILogger<PersonController> _logger = logger;
        private readonly IJwtExtraxtService _jwtExtraxtSerevice = jwtExtraxtSerevice;

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPersonInfoAsync(PersonDto personDto)
        {
            try
            {
                Guid accountId = _jwtExtraxtSerevice.GetAccountIdFromJwtToken(Request.Headers["Authorization"]);
                if (accountId == Guid.Empty)
                {
                    return Unauthorized("Token is invalid");
                }
                var response = await _personAddInfoServise.AddPersonInfoAsync(personDto, accountId);
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
