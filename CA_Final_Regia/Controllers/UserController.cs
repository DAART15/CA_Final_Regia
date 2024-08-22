using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.Interfaces;
using CA_Final_Regia.Services.JwtServices;
using Microsoft.AspNetCore.Mvc;

namespace CA_Final_Regia.Controllers
{
    [Route("regia/user")]
    [ApiController]
    public class UserController(IUserLogInService userLogInService, IUserRegisterService userRegisterService, ILogger<UserController> logger, IJwtTokenService jwtService) : ControllerBase
    {
        private readonly IUserLogInService _userLogInService = userLogInService;
        private readonly IUserRegisterService _userRegisterService = userRegisterService;
        private readonly ILogger _logger = logger;
        private readonly IJwtTokenService _jwtService = jwtService;

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                var response = await _userRegisterService.RegisterAsync(user);
                return StatusCode((int)response.StatusCode, response.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogCritical(ex, "An error occurred while registering a new user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while registering a new user.");
            }
        }
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<JwtTokenService>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JwtTokenService>> LogInAsync(User user)
        {
            try
            {
                var response = await _userLogInService.LogInAsync(user);
                if (!response.IsSuccess)
                {
                    return StatusCode((int)response.StatusCode, response.Message);
                }
                return Ok(_jwtService.GenerateToken(response.AccountId, response.Role));

            }
            catch (ArgumentException ex)
            {
                _logger.LogCritical(ex, "An error occurred while logging in.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while logging in.");
            }
        }
    }
}
