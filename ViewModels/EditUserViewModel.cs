namespace MVCCourse.ViewModels;

public class EditUserViewModel
{
    public required string Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public required string Role { get; set; }
    public decimal Salary { get; set; }
    public string? Password { get; set; } 
}