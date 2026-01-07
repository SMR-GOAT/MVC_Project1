using Microsoft.AspNetCore.Identity;
using MVCCourse.Models;
using MVCCourse.ViewModels;
using MVCCourse.Services.Interfaces; // استدعاء الواجهة
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace MVCCourse.Services;


public class UserService : IUserService
{
    private readonly UserManager<ApplicationUserModel> _userManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<ApplicationUserModel> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

  public async Task CreateUserAsync(CreateUserViewModel model)
{
    // 1. Manual check to ensure the username is not already taken
    var existingUser = await _userManager.FindByNameAsync(model.UserName);
    if (existingUser != null)
    {
        // تم تغيير رسالة الخطأ هنا إلى الإنجليزية
        throw new Exception("This username is already taken, please choose another one.");
    }

    // 2. Map ViewModel to the actual Model
    var user = _mapper.Map<ApplicationUserModel>(model);

    // 3. Attempt to create the user
    var result = await _userManager.CreateAsync(user, model.Password);

    if (!result.Succeeded)
    {
        // Collect Identity errors (e.g., weak password, duplicate email)
        var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
        throw new Exception(errorMessages);
    }

    // 4. Assign the default role
    await _userManager.AddToRoleAsync(user, model.Role);
}
    public async Task<List<UserListViewModel>> GetAllUsersWithRolesAsync()
    {
        // 1. جلب المستخدمين من الداتابيز
        var users = await _userManager.Users.ToListAsync();

        // 2. المابينق الأولي
        var userList = _mapper.Map<List<UserListViewModel>>(users);

        // 3. جلب الأدوار (Logic)
        foreach (var userViewModel in userList)
        {
            var user = users.First(u => u.Id == userViewModel.Id);
            var roles = await _userManager.GetRolesAsync(user);
            userViewModel.Role = roles.FirstOrDefault() ?? "No Role";
        }

        return userList;
    }
}