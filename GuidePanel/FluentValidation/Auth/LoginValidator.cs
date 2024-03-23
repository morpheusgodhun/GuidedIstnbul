using Dto.ApiWebDtos.WebToApiDtos;
using FluentValidation;

namespace GuidePanel.FluentValidation.Auth
{
    public class LoginValidator : AbstractValidator<LoginPostDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Email can't be empty");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password can't be empty");
        }
    }
}
