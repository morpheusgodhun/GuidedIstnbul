using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class FaqCategoryLanguageItem : BaseEntity
    {
        public string DisplayName { get; set; }
        public int LangaugeID { get; set; }

        //FaqCategory

        [ForeignKey("FaqCategory")]
        public Guid FaqCategoryID { get; set; }
        public FaqCategory FaqCategory { get; set; }


    }
}
