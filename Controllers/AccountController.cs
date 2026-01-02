
using Microsoft.AspNetCore.Mvc;
using MVCCourse.Models;
using Microsoft.AspNetCore.Identity;
using MVCCourse.ViewModels;

namespace MVCCourse.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        // هنا السحر: تسجيل دخول باستخدام UserName وليس الإيميل
        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home"); // يوديه للي فيه Layout
        }

        ModelState.AddModelError("", "خطأ في اسم المستخدم أو كلمة المرور");
        return View(model);
    }
}