using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.SurveyDtos
{
    public class LanguageEditSurveyDto
    {
        public Guid SurveyLanguageItemID { get; set; }
        public string? LanguageName { get; set; }
        public string Question { get; set; }
    }
}
