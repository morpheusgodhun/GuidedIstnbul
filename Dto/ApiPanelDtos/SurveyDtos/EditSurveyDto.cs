using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.SurveyDtos
{
    public class EditSurveyDto
    {
        public Guid SurveyID { get; set; }
        public int Order { get; set; }
    }
}
