using CA_Final_Regia.DTOs;
using CA_Final_Regia.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CA_Final_Regia.Controllers
{
    [Route("regia/admin")]
    [ApiController]
    [Authorize]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class AdminController(IGetUsersService getUsersService, ILogger<AdminController> logger, IDeleteUserService deleteUserService) : ControllerBase
    {
        private readonly IGetUsersService _getUsersService = getUsersService;
        private readonly ILogger<AdminController> _logger = logger;
        private readonly IDeleteUserService _deleteUserService = deleteUserService;
        [HttpGet("users")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<IList<AccountDto>>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<AccountDto>>> GetUsersAsync()
        {
            try
            {
                var response = await _getUsersService.GetUsersAsync();
                if (!response.IsSuccess)
                {
                    return StatusCode((int)response.StatusCode, response.Message);
                }
                return StatusCode((int)response.StatusCode, response.List);

            }
            catch (ArgumentException ex)
            {
                _logger.LogCritical(ex, "An error occurred while getting users.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting users.");
            }
        }
        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult>DeleteUserByIdAsync([FromBody] Guid accountId)
        {
            try
            {
                var response = await _deleteUserService.DeleteUserAsync(accountId);
                return StatusCode((int)response.StatusCode, response.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogCritical(ex, "An error occurred while deleting a user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting a user.");
            }
        }
    }
}
