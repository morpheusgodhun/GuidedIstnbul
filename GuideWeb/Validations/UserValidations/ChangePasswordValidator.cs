using Dto.ApiWebDtos.WebToApiDtos;
using FluentValidation;

namespace GuideWeb.Validations.UserValidations
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordPostDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.UserID).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x).Must(AreNewPasswordsEqual);
        }
        bool AreNewPasswordsEqual(ChangePasswordPostDto dto)
        {
            return dto.NewPassword == dto.NewPasswordAgain;
        }
    }
}
