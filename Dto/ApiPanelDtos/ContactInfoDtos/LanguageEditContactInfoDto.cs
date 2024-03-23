using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ContactInfoDtos
{
    public class LanguageEditContactInfoDto
    {
        public Guid ContactInfoLanguageItemID { get; set; }
        public string Value { get; set; }
        public string? LanguageName { get; set; }
    }
}
