using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCourse.Models;
using MVCCourse.ViewModels;

namespace MVCCourse.Controllers
{
    // [Authorize(Roles = "Admin")] // فك التعليق لاحقاً لتأمين الصفحة
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            // جلب كل المستخدمين من قاعدة البيانات
            var users = await _userManager.Users.ToListAsync();
            
            // تحويل القائمة من ApplicationUser إلى UserViewModel
            var userList = _mapper.Map<List<UserViewModel>>(users);
            
            return View(userList);
        }
    }
}