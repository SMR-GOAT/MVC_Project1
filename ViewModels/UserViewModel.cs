
namespace MVCCourse.ViewModels
{
    public class UserViewModel
{
    public required string Id { get; set; }
    public required string FullName { get; set; } // ندمج FirstName مع LastName
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
}
}
