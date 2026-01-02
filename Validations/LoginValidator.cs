using FluentValidation;
using MVCCourse.ViewModels;

namespace MVCCourse.Validations
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
{
    public LoginValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("يا بطل، اسم المستخدم مطلوب!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("وين الباسورد؟")
            .MinimumLength(6).WithMessage("الباسورد لازم لا يقل عن 6 خانات");
    }
}
}
