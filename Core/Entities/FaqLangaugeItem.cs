using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class FaqLangaugeItem:BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int LangaugeID { get; set; }

        //Faq

        [ForeignKey("Faq")]
        public Guid FaqID { get; set; }
        public Faq Faq { get; set; }

        
    }
}
