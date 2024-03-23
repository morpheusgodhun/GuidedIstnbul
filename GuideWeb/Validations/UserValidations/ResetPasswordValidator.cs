using Dto.ApiWebDtos.WebToApiDtos;
using FluentValidation;

namespace GuideWeb.Validations.UserValidations
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordPostDto>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("{PropertyName} can't be empty");
            RuleFor(x => x.NewPasswordAgain).NotEmpty().WithMessage("{PropertyName} can't be empty");
            RuleFor(x => x).Must(AreNewPasswordsEqual).WithMessage("Passwords are not matching!");
        }
        bool AreNewPasswordsEqual(ResetPasswordPostDto dto)
        {
            return dto.NewPassword == dto.NewPasswordAgain;
        }
    }
}
