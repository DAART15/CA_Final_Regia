﻿using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CA_Final_Regia.Web.API.Controllers
{
    [Route("regia/admin")]
    [ApiController]
    [Authorize]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class AdminController(IGetUsersService getUsersService, ILogger<AdminController> logger, IDeleteUserService deleteUserService) : ControllerBase
    {
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
                var response = await getUsersService.GetUsersAsync();
                if (!response.IsSuccess)
                {
                    return StatusCode((int)response.StatusCode, response.Message);
                }
                return StatusCode((int)response.StatusCode, response.List);

            }
            catch (ArgumentException ex)
            {
                logger.LogCritical(ex, "An error occurred while getting users.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting users.");
            }
        }
        [HttpDelete("delete/{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUserByIdAsync( Guid accountId)
        {
            try
            {
                var response = await deleteUserService.DeleteUserAsync(accountId);
                return StatusCode((int)response.StatusCode, response.Message);
            }
            catch (ArgumentException ex)
            {
                logger.LogCritical(ex, "An error occurred while deleting a user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting a user.");
            }
        }
    }
}
