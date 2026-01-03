using Microsoft.AspNetCore.Mvc;

namespace MVCCourse.Controllers
{
    public class ClientsController : Controller
    {
        // قائمة وهمية ثابتة لعرض التصميم فقط
        private static List<ClientViewModel> _clients = new List<ClientViewModel>
        {
            new ClientViewModel { Id = 1, Name = "Sami Al-Harbi", Email = "sami@client.com", Company = "Al-Majd Group", Status = "Active" },
            new ClientViewModel { Id = 2, Name = "Noura Mansour", Email = "noura@client.com", Company = "Creative Solutions", Status = "Pending" },
            new ClientViewModel { Id = 3, Name = "John Wick", Email = "wick@killer.com", Company = "Continental Inc", Status = "Active" }
        };

        // عرض قائمة العملاء
        public ActionResult Index()
        {
            return View(_clients);
        }

        public ActionResult Details(int id) => View();

        public ActionResult Create() => View();

        public ActionResult Edit(int id) => View();

        public ActionResult Delete(int id) => View();
    }

    // كلاس تجريبي للبيانات
    public class ClientViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Company { get; set; }
        public required string Status { get; set; } // Active or Pending
    }
}