namespace MVCCourse.ViewModels;

public class UserListViewModel
{
    public required string Id { get; set; }
    public required string FullName { get; set; } // ندمج FirstName مع LastName
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public required string Role { get; set; } // المنصب لإظهار اللون الفخم
    public decimal Salary { get; set; }
}