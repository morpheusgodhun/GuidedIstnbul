using Dto.ApiWebDtos.WebToApiDtos;
using FluentValidation;

namespace GuideWeb.Validations.ApplyGuideValidations
{
    public class ApplyGuideValidator:AbstractValidator<ApplyGuidePostDto>
    {
        public ApplyGuideValidator()
        {
            RuleFor(x => x.LanguageKnowIDs).NotEmpty().WithMessage("You Should Speak At Least 1 Language");
            RuleFor(x => x.SpecializeCityIDs).NotEmpty().WithMessage("You Should Specialize At Least 1 City");
        }
    }
}
