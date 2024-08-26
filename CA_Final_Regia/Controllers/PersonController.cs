using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CA_Final_Regia.Web.API.Controllers
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
        public async Task<IActionResult> AddPersonInfoAsync([FromHeader(Name = "Authorization")] string auth, [FromForm] PersonPostDto personDto)
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
                if (!response.IsSuccess)
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
        public async Task<IActionResult> UpdateFirstNamAsync([FromHeader(Name = "Authorization")] string auth, [FromBody] KeyValue personUpdateKeyValue)
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
                logger.LogCritical(ex, "An error occurred while updating person First Name.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred updating person First Name.");
            }
        }
        [HttpPut("update/surname")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateLastNameAsync([FromHeader(Name = "Authorization")] string auth, [FromBody] KeyValue personUpdateKeyValue)
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
                logger.LogCritical(ex, "An error occurred while updating person Last Name.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating person Last Name.");
            }
        }
        [HttpPut("update/personalid")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePersonalIdAsync([FromHeader(Name = "Authorization")] string auth, [FromBody] KeyValue personUpdateKeyValue)
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
                logger.LogCritical(ex, "An error occurred while updating person personal ID.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating person personal ID.");
            }
        }
        [HttpPut("update/mail")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMaildAsync([FromHeader(Name = "Authorization")] string auth, [FromBody] KeyValue personUpdateKeyValue)
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
                logger.LogCritical(ex, "An error occurred while updating person mail.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating person mail.");
            }
        }
        [HttpPut("update/picture")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePictureAsync([FromHeader(Name = "Authorization")] string auth, [FromForm] PictureDto pictureDto)
        {
            try
            {
                Guid accountId = jwtExtraxtSerevice.GetAccountIdFromJwtToken(auth);
                if (accountId == Guid.Empty)
                {
                    return Unauthorized("Token is invalid");
                }
                var response = await personUpdateService.UpdatePersonPictureAsync(pictureDto, accountId);
                return StatusCode((int)response.StatusCode, response.Message);
            }
            catch (ArgumentException ex)
            {
                logger.LogCritical(ex, "An error occurred while updating person picture.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating person picture.");
            }
        }
    }
}
