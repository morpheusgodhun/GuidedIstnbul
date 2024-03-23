using Dto.ApiPanelDtos.CertificateManagementDtos;
using FluentValidation;
using FluentValidation.Results;
using GuidePanel.FluentValidation.Certificate;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Policy;

namespace GuidePanel.FluentValidation
{
    public static class  FluentValidator<T> where T : class 
    {
        public static bool Validator(AbstractValidator<T> validator, T model, ModelStateDictionary modelState)
        {

            ValidationResult result = validator.Validate(model);
            if (result.IsValid)
            {
                return true;
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    modelState[error.PropertyName].Errors.Clear();
                    modelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            return false;
        }
    }
}
