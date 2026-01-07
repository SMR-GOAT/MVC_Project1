using FluentValidation;
using MVCCourse.ViewModels;

namespace MVCCourse.Validations;

public class CreateUserValidator : AbstractValidator<CreateUserViewModel>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First Name is required.")
            .MaximumLength(50).WithMessage("Max 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last Name is required.")
            .MaximumLength(50).WithMessage("Max 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Salary)
            .GreaterThanOrEqualTo(0).WithMessage("Salary must be positive.");

      RuleFor(x => x.Password)
    .NotEmpty().WithMessage("Password is required.")
    .MinimumLength(8).WithMessage("Minimum 8 characters.")
    // التعديل هنا: استخدام نمط أكثر دقة
    .Matches(@"^(?=.*[A-Z]).*$").WithMessage("Must contain at least one uppercase letter.")
    .Matches(@"^(?=.*[a-z]).*$").WithMessage("Must contain at least one lowercase letter.")
    .Matches(@"^(?=.*[0-9]).*$").WithMessage("Must contain at least one number.")
    .Matches(@"^(?=.*[\!\?\*\.]).*$").WithMessage("Must contain at least one special character (!?*.).");
    }
}