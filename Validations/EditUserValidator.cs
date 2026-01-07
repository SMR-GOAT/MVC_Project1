using FluentValidation;
using MVCCourse.ViewModels;

namespace MVCCourse.Validations;

public class EditUserValidator : AbstractValidator<EditUserViewModel>
{
    public EditUserValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First Name is required.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last Name is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        // Password validation logic: Only validate if the user types something
        RuleFor(x => x.Password)
            .MinimumLength(6).WithMessage("New Password must be at least 6 characters.")
            .When(x => !string.IsNullOrEmpty(x.Password));
            
        RuleFor(x => x.Salary)
            .GreaterThanOrEqualTo(0).WithMessage("Salary cannot be negative.");
    }
}