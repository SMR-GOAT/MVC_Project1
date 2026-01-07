namespace MVCCourse.ViewModels;

public class CreateUserViewModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }

public required string UserName { get; set; }

public string? Address { get; set; }
     public required string Password { get; set; }
    public string? PhoneNumber { get; set; }
    public required string Role { get; set; } = "Admin";
    public decimal Salary { get; set; }
}