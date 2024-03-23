using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Survey : BaseEntity
    {
        public int Order { get; set; }


        //SurveyLanguageItem
        public ICollection<SurveyLanguageItem> SurveyLanguageItems { get; set; }
    }
}
