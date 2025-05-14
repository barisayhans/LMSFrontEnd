using CleanArchitecture.Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserListDto>> GetAllUsersWithRolesAsync();
        Task<UserStatisticsDto> GetUserStatisticsAsync(string userId);
        Task<UserDto> RegisterAsync(UserDto dto);
        Task<bool> AuthenticateAsync(LoginDto dto);
        Task<UserDto> GetUserByIdAsync(string id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task UpdateUserAsync(string id, UpdateUserDto dto);
        Task DeleteUserAsync(string id);
        Task AssignRoleAsync(string userId, string newRole);
        Task<bool> ChangePasswordAsync(string userId, ChangePasswordDto dto);
        Task<UserStatisticsDto> GetLibraryStatisticsAsync();
        //Task UpdateUserRoleAsync(Guid userId, string newRole);
    }
}
