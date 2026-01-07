using Microsoft.AspNetCore.Identity;
using MVCCourse.Models;
using MVCCourse.ViewModels;
using MVCCourse.Services.Interfaces; // استدعاء الواجهة

namespace MVCCourse.Services;

public class AccountService : IAccountService
{
    private readonly SignInManager<ApplicationUserModel> _signInManager;

    public AccountService(SignInManager<ApplicationUserModel> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<SignInResult> LoginAsync(LoginViewModel model)
    {
        return await _signInManager.PasswordSignInAsync(
            model.UserName, 
            model.Password, 
            model.RememberMe, 
            lockoutOnFailure: false);
    }

    public async Task LogoutAsync() => await _signInManager.SignOutAsync();
}