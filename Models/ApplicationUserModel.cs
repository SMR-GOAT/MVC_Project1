using Microsoft.AspNetCore.Identity;

namespace MVCCourse.Models
{
    public class ApplicationUserModel : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public decimal Salary { get; set; }
        public string? Address { get; set; }
       
    }
}