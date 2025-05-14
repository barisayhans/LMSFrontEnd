using CleanArchitecture.Core.DTOs.User;
using CleanArchitecture.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersWithRolesAsync();
            return Ok(users);
        }

        [HttpGet("statistics/{userId}")]
        public async Task<IActionResult> GetUserStatisticsAsync(string userId)
        {
            var result = await _userService.GetUserStatisticsAsync(userId);
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.RegisterAsync(request);
            return CreatedAtAction(nameof(GetUserById), user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.UpdateUserAsync(userId, request);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await _userService.DeleteUserAsync(userId);
            return NoContent();
        }

        [HttpPut("{userId}/role")]
        public async Task<IActionResult> ChangeUserRole(String userId, [FromBody] string newRole)
        {
            try
            {
                await _userService.AssignRoleAsync(userId, newRole);
                return Ok("User role updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("library-statistics")]
        public async Task<IActionResult> GetLibraryStatistics()
        {
            var statistics = await _userService.GetLibraryStatisticsAsync();
            return Ok(statistics);
        }

    }
}
