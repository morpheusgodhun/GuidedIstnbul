using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.TagManagementDtos
{
    public class LanguageEditTagDto
    {
        public string? LanguageName { get; set; }
        public Guid TagLanguageItemID { get; set; }
        public string DisplayName { get; set; }
        public string Slug { get; set; }
    }
}
