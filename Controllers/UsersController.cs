using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCCourse.Services.Interfaces;
using MVCCourse.ViewModels;

namespace MVCCourse.Controllers
{
    [Authorize(Roles = "SuperAdmin")] 
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        
[HttpPost]
public async Task<IActionResult> Delete(string id)
{
    try 
    {
        await _userService.DeleteUserAsync(id);
        
        // Success message in English
        return Json(new { success = true, message = "User has been deleted successfully." });
    }
    catch (Exception ex)
    {
        // The message here will be the English exception from the Service 
        // (e.g., "User not found" or "SuperAdmin cannot be deleted")
        return Json(new { success = false, message = ex.Message });
    }
}      // 1. عرض جدول المستخدمين
        public async Task<IActionResult> Index()
        {
            var userList = await _userService.GetAllUsersWithRolesAsync();
            return View(userList);
        }

        // 2. عرض صفحة إضافة مستخدم جديد (GET)
        [HttpGet]
        public IActionResult Create()
        {
            // نرجع الفيو فاضي عشان اليوزر يعبي البيانات
            return View();
        }

        // 3. استقبال بيانات المستخدم الجديد ومعالجتها (POST)
        [HttpPost]
        [ValidateAntiForgeryToken] // حماية من هجمات CSRF
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            // إذا الفاليديشن (FluentValidation) فيه أخطاء
            if (!ModelState.IsValid)
            {
                // نرجعه لنفس الصفحة (صفحة Create) مع عرض الأخطاء
                // الـ Tag Helpers (asp-for) بتمسك الأخطاء تلقائياً
                return View(model);
            }

            try 
            {
                // حفظ المستخدم في قاعدة البيانات
                await _userService.CreateUserAsync(model);
                
                // بعد النجاح نرجعه للجدول الرئيسي
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // في حال حدث خطأ تقني غير متوقع
                ModelState.AddModelError("", "Something went wrong: " + ex.Message);
                return View(model);
            }
        }

[HttpGet]
public async Task<IActionResult> Edit(string id)
{
    if (id == null) return NotFound();

    var model = await _userService.GetUserForEditAsync(id);
    
    if (model == null) return NotFound();

    return View(model);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(EditUserViewModel model)
{
    if (!ModelState.IsValid) return View(model);

    try {
        await _userService.UpdateUserAsync(model);
        return RedirectToAction(nameof(Index));
    } catch (Exception ex) {
        ModelState.AddModelError("", ex.Message);
        return View(model);
    }
}
    }

    

    
}