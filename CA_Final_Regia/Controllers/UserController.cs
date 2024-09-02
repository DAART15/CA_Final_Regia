using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Interfaces;
using CA_Final_Regia.Services.Services.JwtServices;
using Microsoft.AspNetCore.Mvc;
namespace CA_Final_Regia.Web.API.Controllers
{
    [Route("regia/user")]
    [ApiController]
    public class UserController(IUserLogInService userLogInService, IUserRegisterService userRegisterService, ILogger<UserController> logger, IJwtTokenService jwtService) : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterAsync(User user)
        {
            try
            {
                var response = await userRegisterService.RegisterAsync(user);
                return StatusCode((int)response.StatusCode, response.Message);
            }
            catch (ArgumentException ex)
            {
                logger.LogCritical(ex, "An error occurred while registering a new user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while registering a new user.");
            }
        }
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> LogInAsync(User user)
        {
            try
            {
                var response = await userLogInService.LogInAsync(user);
                if (!response.IsSuccess)
                {
                    return StatusCode((int)response.StatusCode, response.Message);
                }
                return Ok(jwtService.GenerateToken(response.AccountId, response.Role));
            }
            catch (ArgumentException ex)
            {
                logger.LogCritical(ex, "An error occurred while logging in.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while logging in.");
            }
        }
    }
}
