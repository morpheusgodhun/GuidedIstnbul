using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ImageManagementDtos
{
    public class LanguageEditImageDto
    {
        public Guid ImageLanguageItemID { get; set; }
        public string? LanguageName { get; set; }
        public int LanguageID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
