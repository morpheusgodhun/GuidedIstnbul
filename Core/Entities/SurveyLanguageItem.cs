using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SurveyLanguageItem : BaseEntity
    {
        public string Question { get; set; }
        public int LanguageID { get; set; }

        //Survey

        [ForeignKey("Survey")]
        public Guid SurveyID { get; set; }
        public Survey Survey { get; set; }

       
    }
}
