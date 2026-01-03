using Microsoft.AspNetCore.Mvc;
using MVCCourse.Models; // تأكد أن كلاس Employee موجود هنا

namespace MVCCourse.Controllers
{
    // حذفنا الـ [ApiController] والـ [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        // بيانات وهمية ثابتة للعرض فقط
        private static List<EmployeeViewModel> employees = new List<EmployeeViewModel>
        {
            new EmployeeViewModel { Id = 1, Name = "John Doe", Position = "Full Stack Developer", Email = "john@smr.com" },
            new EmployeeViewModel { Id = 2, Name = "Jane Smith", Position = "HR Manager", Email = "jane@smr.com" },
            new EmployeeViewModel { Id = 3, Name = "Ali Ahmed", Position = "System Admin", Email = "ali@smr.com" }
        };

        // الدالة الأساسية التي تفتح الصفحة داخل الـ Layout
        public IActionResult Index()
        {
            return View(employees); // يرسل القائمة إلى الـ View
        }

        public IActionResult Create() => View();

        public IActionResult Edit(int id) => View();
    }

    // كلاس بسيط للبيانات الوهمية إذا لم يكن لديك Model جاهز
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Position { get; set; }
        public required string Email { get; set; }
    }
}