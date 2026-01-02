namespace MVCCourse.ViewModels
{
    public class LoginViewModel
{
    public required string UserName { get; set; } // بندخل بـ SMR أو الإيميل
    public required string Password { get; set; }
    public bool RememberMe { get; set; }
}
}

