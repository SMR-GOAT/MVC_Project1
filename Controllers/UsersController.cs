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

        // 1. عرض جدول المستخدمين
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
    }
}