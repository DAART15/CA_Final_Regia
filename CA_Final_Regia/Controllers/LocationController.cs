using CA_Final_Regia.DTOs;
using CA_Final_Regia.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CA_Final_Regia.Controllers
{
    [Route("regia/location")]
    [ApiController]
    [Authorize]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    public class LocationController(ILogger<PersonController> logger, IJwtExtraxtService jwtExtraxtSerevice, ILocationAddService locationAddService, ILocationGetService locationGetService) : ControllerBase
    {
        private readonly ILogger<PersonController> _logger = logger;
        private readonly IJwtExtraxtService _jwtExtraxtSerevice = jwtExtraxtSerevice;
        private readonly ILocationAddService _locationAddService = locationAddService;
        private readonly ILocationGetService _locationGetService = locationGetService;
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddLocationAsync([FromHeader(Name = "Authorization")] string auth, [FromBody]LocationDto locationDto)
        {
            try
            {
                Guid accountId = _jwtExtraxtSerevice.GetAccountIdFromJwtToken(auth);
                if (accountId == Guid.Empty)
                {
                    return Unauthorized("Token is invalid");
                }
                var response = await _locationAddService.AddLocationAsync(locationDto, accountId);
                return StatusCode((int)response.StatusCode, response.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogCritical(ex, "An error occurred while adding a new location.");
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
                Guid accountId = _jwtExtraxtSerevice.GetAccountIdFromJwtToken(auth);
                if (accountId == Guid.Empty)
                {
                    return Unauthorized("Token is invalid");
                }
                var response = await _locationGetService.GetLocationAsync(accountId);
                if (!response.IsSuccess)
                {
                    return StatusCode((int)response.StatusCode, response.Message);
                }
                return StatusCode((int)response.StatusCode, response.Object);
            }
            catch (ArgumentException ex)
            {
                _logger.LogCritical(ex, "An error occurred while getting location.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting location.");
            }
        }
    }
}
