using KraevedAPI.Models;
using KraevedAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace KraevedAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize] 
    public class UsersController : ControllerBase
    {
        private readonly IKraevedService _kraevedService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IKraevedService kraevedService, ILogger<UsersController> logger)
        {
            _kraevedService = kraevedService;
            _logger = logger;
        }

        [HttpGet("current")]
        public async Task<ActionResult> GetCurrentUser()
        {
            try
            {
                var result = await _kraevedService.GetCurrentUserInfo();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting the current user.");
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpPatch("current")]
        public async Task<ActionResult> PatchUser([FromBody] UserInDto userInDto)
        {
            try
            {
                var result = await _kraevedService.PatchUser(userInDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while patching the user.");
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpGet("all")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> GetAllUsers()
        {
            try
            {
                var result = await _kraevedService.GetAllUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all users.");
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> GetUserById(int id)
        {
            try
            {
                var result = await _kraevedService.GetUserById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user by ID.");
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _kraevedService.DeleteUser(id);
                return Ok(new { message = "User deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting user.");
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpPatch("{id}/role")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> UpdateUserRole(int id, [FromBody] UserRoleInDto roleDto)
        {
            try
            {
                var result = await _kraevedService.UpdateUserRole(id, roleDto.RoleName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating user role.");
                return StatusCode(500, new { ex.Message });
            }
        }
    }
}
