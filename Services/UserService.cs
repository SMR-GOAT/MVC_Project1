using Microsoft.AspNetCore.Identity;
using MVCCourse.Models;
using MVCCourse.ViewModels;
using MVCCourse.Services.Interfaces;
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
        var existingUser = await _userManager.FindByNameAsync(model.UserName);
        if (existingUser != null)
        {
            throw new Exception("This username is already taken, please choose another one.");
        }

        var user = _mapper.Map<ApplicationUserModel>(model);
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception(errorMessages);
        }

        await _userManager.AddToRoleAsync(user, model.Role);
    }

public async Task DeleteUserAsync(string id)
{
    var user = await _userManager.FindByIdAsync(id);
    if (user == null) 
    {
        throw new Exception("User not found.");
    }

    // Security Wall: Prevent deleting the SuperAdmin
    var roles = await _userManager.GetRolesAsync(user);
    if (roles.Any(r => r.Equals("SuperAdmin", StringComparison.OrdinalIgnoreCase)))
    {
        throw new Exception("Security Alert: SuperAdmin account cannot be deleted!");
    }

    var result = await _userManager.DeleteAsync(user);
    if (!result.Succeeded)
    {
        // Collect errors if deletion fails for any reason
        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        throw new Exception($"Failed to delete user: {errors}");
    }
}

    public async Task<List<UserListViewModel>> GetAllUsersWithRolesAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var userList = _mapper.Map<List<UserListViewModel>>(users);

        foreach (var userViewModel in userList)
        {
            var user = users.First(u => u.Id == userViewModel.Id);
            var roles = await _userManager.GetRolesAsync(user);
            userViewModel.Role = roles.FirstOrDefault() ?? "No Role";
        }

        return userList;
    }

    // جلب بيانات المستخدم للتعديل
    public async Task<EditUserViewModel> GetUserForEditAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) throw new Exception("User not found");

        var model = _mapper.Map<EditUserViewModel>(user);
        
        var roles = await _userManager.GetRolesAsync(user);
        model.Role = roles.FirstOrDefault() ?? "No Role";
        
        return model;
    }

    // حفظ التعديلات
    public async Task UpdateUserAsync(EditUserViewModel model)
{
    var user = await _userManager.FindByIdAsync(model.Id);
    if (user == null) throw new Exception("User not found");

    // تحديث البيانات الأساسية (Mapping)
    _mapper.Map(model, user);
    
    // تحديث رقم الجوال يدوياً (لأن Identity له ميثود خاصة به أحياناً)
    user.PhoneNumber = model.PhoneNumber;

    var result = await _userManager.UpdateAsync(user);
    
    if (result.Succeeded)
    {
        // إذا كتب المستخدم كلمة مرور جديدة، نقوم بتحديثها
        if (!string.IsNullOrEmpty(model.NewPassword))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
        }
    }
    else
    {
        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        throw new Exception(errors);
    }
}
}