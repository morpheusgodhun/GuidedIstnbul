using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.CancellationPolicyDtos
{
    public class LanguageEditCancellationPolicyDto
    {
        public Guid CancellationPolicyLanguageItemID { get; set; }
        public string? LanguageName { get; set; }
        public string Content { get; set; }
        [ValidateNever]
        public string? Name { get; set; }
    }
}
