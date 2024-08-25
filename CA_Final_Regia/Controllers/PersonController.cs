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
    public class PersonController(IPersonAddInfoService personAddInfoServise, ILogger<PersonController> logger, IJwtExtraxtService jwtExtraxtSerevice, IPersonGetInfoService personGetInfoService, IPersonUpdateService personUpdateService) : ControllerBase
    {
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPersonInfoAsync([FromHeader(Name = "Authorization")] string auth,[FromForm] PersonPostDto personDto)
        {
            try
            {
                Guid accountId = jwtExtraxtSerevice.GetAccountIdFromJwtToken(auth);
                if (accountId == Guid.Empty)
                {
                    return Unauthorized("Token is invalid");
                }
                var response = await personAddInfoServise.AddPersonInfoAsync(personDto, accountId);
                return StatusCode((int)response.StatusCode, response.Message);
            }
            catch (ArgumentException ex)
            {
                logger.LogCritical(ex, "An error occurred while adding a new person.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding a new person.");
            }
        }
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<PersonGetDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonGetDto>> GetPersonInfoAsync([FromHeader(Name = "Authorization")] string auth)
        {
            try
            {
                Guid accountId = jwtExtraxtSerevice.GetAccountIdFromJwtToken(auth);
                if (accountId == Guid.Empty)
                {
                    return Unauthorized("Token is invalid");
                }
                var response = await personGetInfoService.GetPersonInfoAsync(accountId);
                if(!response.IsSuccess)
                {
                    return StatusCode((int)response.StatusCode, response.Message);
                }
                return StatusCode((int)response.StatusCode, response.Object);
            }
            catch (ArgumentException ex)
            {
                logger.LogCritical(ex, "An error occurred while getting person info.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting person info.");
            }
        }
        [HttpPut("update/name")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPersonInfoAsync([FromHeader(Name = "Authorization")] string auth, [FromBody] KeyValue personUpdateKeyValue)
        {
            try
            {
                Guid accountId = jwtExtraxtSerevice.GetAccountIdFromJwtToken(auth);
                if (accountId == Guid.Empty)
                {
                    return Unauthorized("Token is invalid");
                }
                var response = await personUpdateService.UpdatePersonAsync(personUpdateKeyValue, accountId);
                return StatusCode((int)response.StatusCode, response.Message);
            }
            catch (ArgumentException ex)
            {
                logger.LogCritical(ex, "An error occurred while adding a new person.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding a new person.");
            }
        }
    }
}
