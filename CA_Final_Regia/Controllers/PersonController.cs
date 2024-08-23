using CA_Final_Regia.Domain.Models;
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
    public class PersonController(IPersonAddInfoService personAddInfoServise, ILogger<PersonController> logger, IJwtExtraxtService jwtExtraxtSerevice, IPersonGetInfoService personGetInfoService) : ControllerBase
    {
        private readonly IPersonAddInfoService _personAddInfoServise = personAddInfoServise;
        private readonly ILogger<PersonController> _logger = logger;
        private readonly IJwtExtraxtService _jwtExtraxtSerevice = jwtExtraxtSerevice;
        private readonly IPersonGetInfoService _personGetInfoService = personGetInfoService;

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPersonInfoAsync([FromHeader(Name = "Authorization")] string auth,[FromForm] PersonDto personDto)
        {
            try
            {
                Guid accountId = _jwtExtraxtSerevice.GetAccountIdFromJwtToken(auth);
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
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<Person>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Person>> GetPersonInfoAsync([FromHeader(Name = "Authorization")] string auth)
        {
            try
            {
                Guid accountId = _jwtExtraxtSerevice.GetAccountIdFromJwtToken(auth);
                if (accountId == Guid.Empty)
                {
                    return Unauthorized("Token is invalid");
                }
                var response = await _personGetInfoService.GetPersonInfoAsync(accountId);
                if(!response.IsSuccess)
                {
                    return StatusCode((int)response.StatusCode, response.Message);
                }
                return StatusCode((int)response.StatusCode, response.Object);
            }
            catch (ArgumentException ex)
            {
                _logger.LogCritical(ex, "An error occurred while getting person info.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting person info.");
            }
        }
    }
}
