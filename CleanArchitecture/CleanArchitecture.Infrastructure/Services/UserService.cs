using AutoMapper;
using CleanArchitecture.Core.DTOs.User;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Interfaces.Services;
using CleanArchitecture.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CleanArchitecture.Infrastructure.Helpers.HashHelper;

namespace CleanArchitecture.Infrastructure.Services
{
    public class UserService : IUserService
    {
        //private readonly IUserRepositoryAsync _userRepository;
        private readonly IBorrowRepositoryAsync _borrowRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IBookRepositoryAsync _bookRepository;
        private readonly IReservationRepositoryAsync _reservationRepository;

        public UserService(IMapper mapper, IBorrowRepositoryAsync borrowRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IBookRepositoryAsync bookRepository, IReservationRepositoryAsync reservationRepository)
        {
            //_userRepository = userRepository;
            _mapper = mapper;
            _borrowRepository = borrowRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _bookRepository = bookRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<UserDto> RegisterAsync(UserDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.Name,
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, dto.PasswordHash);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            await _userManager.AddToRoleAsync(user, dto.Role.ToString());
            return new UserDto
            {
                Id = user.Id,
                Name = user.FirstName,
                Email = user.Email,
                Role = Enum.Parse<Roles>(dto.Role.ToString())
            };
        }        

        public async Task<IEnumerable<UserListDto>> GetAllUsersWithRolesAsync()
        {
            var users = _userManager.Users.ToList();
            var userList = new List<UserListDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userList.Add(new UserListDto
                {
                    Id = user.Id,
                    Name = user.FirstName,
                    Email = user.Email,
                    Role = roles.FirstOrDefault() ?? "No Role"
                });
            }

            return userList;
        }        

        public async Task<UserStatisticsDto> GetUserStatisticsAsync(string userId)
        {
            // Total borrowed books by the user
            var totalBorrowed = (await _borrowRepository.GetUserBorrowedBooksAsync(userId)).Count();

            // Not returned books by the user
            var notReturned = (await _borrowRepository.GetUserBorrowedBooksAsync(userId))
                .Count(b => b.ReturnedAt == null);

            // Total user count in the database
            var totalUsers = _userManager.Users.Count();

            // Total borrowed books by all users
            var borrowedBooks = (await _borrowRepository.GetAllAsync()).Count();

            // Overdue books count
            var overdueBooks = (await _borrowRepository.GetOverdueBooksAsync()).Count();

            return new UserStatisticsDto
            {
                TotalBorrowed = totalBorrowed,
                NotReturned = notReturned,
                TotalVisitors = totalUsers,
                BorrowedBooks = borrowedBooks,
                OverdueBooks = overdueBooks
            };
        }

        public async Task<bool> AuthenticateAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password.");

            // Verify the plain-text password against the stored hashed password
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isPasswordValid)
                throw new UnauthorizedAccessException("Invalid email or password.");

            return true;
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            var roles = await _userManager.GetRolesAsync(user);
            return new UserDto
            {
                Id = user.Id,
                Name = user.FirstName,
                Email = user.Email,
                Role = Enum.Parse<Roles>(roles.FirstOrDefault() ?? Roles.Reader.ToString())
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            return users.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.FirstName,
                Email = user.Email,
                Role = Enum.Parse<Roles>(_userManager.GetRolesAsync(user).Result.FirstOrDefault() ?? Roles.Reader.ToString())
            });
        }

        public async Task UpdateUserAsync(string id, UpdateUserDto dto)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            user.FirstName = dto.Name;
            user.Email = dto.Email;
            user.UserName = dto.Email;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task AssignRoleAsync(string userId, string newRole)
        {
            // Ensure the role exists
            if (!await _roleManager.RoleExistsAsync(newRole))
            {
                throw new Exception($"Role '{newRole}' does not exist.");
            }
            // Find the user by ID
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Get the user's current roles
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Remove the user from all current roles
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                throw new Exception("Failed to remove user from current roles.");
            }

            // Add the user to the new role
            var addResult = await _userManager.AddToRoleAsync(user, newRole);
            if (!addResult.Succeeded)
            {
                throw new Exception("Failed to add user to the new role.");
            }
        }

        public async Task<bool> ChangePasswordAsync(string userId, ChangePasswordDto dto)
        {
            if (dto.NewPassword != dto.ConfirmNewPassword)
                throw new ArgumentException("New password and confirmation do not match.");

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            // Verify old password
            var isOldPasswordValid = await _userManager.CheckPasswordAsync(user, dto.CurrentPassword);
            if (!isOldPasswordValid)
                throw new UnauthorizedAccessException("Current password is incorrect.");

            // Change the password
            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            return true;
        }

        public async Task<UserStatisticsDto> GetLibraryStatisticsAsync()
        {
            // Total borrowed books
            var totalBorrowed = await _borrowRepository.GetAllAsync();
            var totalBorrowedCount = totalBorrowed.Count();

            // Not returned books
            var notReturnedCount = totalBorrowed.Count(b => b.ReturnedAt == null);

            // Total visitors (unique users who borrowed books)
            var totalVisitorsCount = totalBorrowed.Select(b => b.UserId).Distinct().Count();

            // Overdue books
            var overdueBooksCount = totalBorrowed.Count(b => b.Status == BorrowStatus.Overdue);

            // Available books
            var allBooks = await _bookRepository.GetAllAsync();
            var availableBooksCount = allBooks.Count() - notReturnedCount;

            // Reserved books
            var reservedBooksCount = await _reservationRepository.GetActiveReservationsCountAsync();

            // New members (users created in the last 30 days)
            var newMembersCount = _userManager.Users.Count(u => u.CreatedAt >= DateTime.UtcNow.AddDays(-30));

            return new UserStatisticsDto
            {
                TotalBorrowed = totalBorrowedCount,
                NotReturned = notReturnedCount,
                TotalVisitors = totalVisitorsCount,
                BorrowedBooks = notReturnedCount,
                OverdueBooks = overdueBooksCount,
                NewMembers = newMembersCount,
                AvailableBooks = availableBooksCount,
                ReservedBooks = reservedBooksCount
            };
        }


    }
}
