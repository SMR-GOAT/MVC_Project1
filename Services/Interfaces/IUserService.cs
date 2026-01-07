using Microsoft.AspNetCore.Identity;
using MVCCourse.ViewModels;

namespace MVCCourse.Services.Interfaces;

public interface IUserService
{
    Task CreateUserAsync(CreateUserViewModel model);
    Task<List<UserListViewModel>> GetAllUsersWithRolesAsync();
}