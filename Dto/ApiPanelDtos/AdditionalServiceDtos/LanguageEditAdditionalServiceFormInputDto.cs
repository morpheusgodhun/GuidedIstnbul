using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.AdditionalServiceDtos
{
    public class LanguageEditAdditionalServiceFormInputDto
    {
        public Guid AdditionalServiceInputLanguageItemID { get; set; }
        public string? LanguageName { get; set; }
        public string PropertyName { get; set; }
        [ValidateNever]
        public string? AdditionalServiceInputId { get; set; }
    }
}
