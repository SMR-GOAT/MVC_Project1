namespace MVCCourse.ViewModels;

public class EditUserViewModel
{
    public required string Id { get; set; }
    public required string FirstName { get; set; } 
    public required string LastName { get; set; } 
    public required string Email { get; set; }
    public string? Address { get; set; }
    public required string UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Role { get; set; }
    public decimal Salary { get; set; }
    public string? NewPassword { get; set; } 
}