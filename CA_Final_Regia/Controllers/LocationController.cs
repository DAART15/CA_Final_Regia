using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CA_Final_Regia.Web.API.Controllers
{
    [Route("regia/location")]
    [ApiController]
    [Authorize]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    public class LocationController(ILogger<LocationController> logger, IJwtExtractService jwtExtraxtSerevice, ILocationAddService locationAddService, ILocationGetService locationGetService, ILocationUpdateService locationUpdateService) : ControllerBase
    {
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddLocationAsync([FromHeader(Name = "Authorization")] string auth, [FromBody] LocationDto locationDto)
        {
            try
            {
                Guid accountId = jwtExtraxtSerevice.GetAccountIdFromJwtToken(auth);
                if (accountId == Guid.Empty)
                {
                    return Unauthorized("Token is invalid");
                }
                var response = await locationAddService.AddLocationAsync(locationDto, accountId);
                return StatusCode((int)response.StatusCode, response.Message);
            }
            catch (ArgumentException ex)
            {
                logger.LogCritical(ex, "An error occurred while adding a new location.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding a new location.");
            }
        }
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<LocationDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LocationDto>> GetLocationAsync([FromHeader(Name = "Authorization")] string auth)
        {
            try
            {
                Guid accountId = jwtExtraxtSerevice.GetAccountIdFromJwtToken(auth);
                if (accountId == Guid.Empty)
                {
                    return Unauthorized("Token is invalid");
                }
                var response = await locationGetService.GetLocationAsync(accountId);
                if (!response.IsSuccess)
                {
                    return StatusCode((int)response.StatusCode, response.Message);
                }
                return StatusCode((int)response.StatusCode, response.Object);
            }
            catch (ArgumentException ex)
            {
                logger.LogCritical(ex, "An error occurred while getting location.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting location.");
            }
        }
        [HttpPut("update/city")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCityAsync([FromHeader(Name = "Authorization")] string auth, [FromBody] KeyValue locationUpdateKeyValue)
        {
            try
            {
                Guid accountId = jwtExtraxtSerevice.GetAccountIdFromJwtToken(auth);
                if (accountId == Guid.Empty)
                {
                    return Unauthorized("Token is invalid");
                }
                var response = await locationUpdateService.UpdateLocationAsync(locationUpdateKeyValue, accountId);
                return StatusCode((int)response.StatusCode, response.Message);
            }
            catch (ArgumentException ex)
            {
                logger.LogCritical(ex, "An error occurred while updating location city.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating location city.");
            }
        }
        [HttpPut("update/street")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateStreetAsync([FromHeader(Name = "Authorization")] string auth, [FromBody] KeyValue locationUpdateKeyValue)
        {
            try
            {
                Guid accountId = jwtExtraxtSerevice.GetAccountIdFromJwtToken(auth);
                if (accountId == Guid.Empty)
                {
                    return Unauthorized("Token is invalid");
                }
                var response = await locationUpdateService.UpdateLocationAsync(locationUpdateKeyValue, accountId);
                return StatusCode((int)response.StatusCode, response.Message);
            }
            catch (ArgumentException ex)
            {
                logger.LogCritical(ex, "An error occurred while updating location street.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating location street.");
            }
        }
        [HttpPut("update/housenr")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateHouseNrAsync([FromHeader(Name = "Authorization")] string auth, [FromBody] KeyValue locationUpdateKeyValue)
        {
            try
            {
                Guid accountId = jwtExtraxtSerevice.GetAccountIdFromJwtToken(auth);
                if (accountId == Guid.Empty)
                {
                    return Unauthorized("Token is invalid");
                }
                var response = await locationUpdateService.UpdateLocationAsync(locationUpdateKeyValue, accountId);
                return StatusCode((int)response.StatusCode, response.Message);
            }
            catch (ArgumentException ex)
            {
                logger.LogCritical(ex, "An error occurred while updating location House Nr.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating location House Nr.");
            }
        }
        [HttpPut("update/apartmentnr")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateApartmentNrAsync([FromHeader(Name = "Authorization")] string auth, [FromBody] KeyValue locationUpdateKeyValue)
        {
            try
            {
                Guid accountId = jwtExtraxtSerevice.GetAccountIdFromJwtToken(auth);
                if (accountId == Guid.Empty)
                {
                    return Unauthorized("Token is invalid");
                }
                var response = await locationUpdateService.UpdateLocationAsync(locationUpdateKeyValue, accountId);
                return StatusCode((int)response.StatusCode, response.Message);
            }
            catch (ArgumentException ex)
            {
                logger.LogCritical(ex, "An error occurred while updating location Apartment Nr.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating location Apartment Nr.");
            }
        }
    }
}

