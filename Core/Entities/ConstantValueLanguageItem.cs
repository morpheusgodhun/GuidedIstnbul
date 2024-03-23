using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ConstantValueLanguageItem : BaseEntity
    {
        public string Value { get; set; }
        public int LanguageID { get; set; }

        //ConstantValue
        [ForeignKey("ConstantValue")]
        public Guid ConstantValueID { get; set; }
        public ConstantValue ConstantValue { get; set; }




    }
}
