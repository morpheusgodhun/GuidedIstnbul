using Dto.ApiPanelDtos.CertificateManagementDtos;
using FluentValidation;

namespace GuidePanel.FluentValidation.Certificate
{
    public class AddCertificateValidator : AbstractValidator<AddCertificateDto>
    {
        public AddCertificateValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is Required");
        }
    }
}
