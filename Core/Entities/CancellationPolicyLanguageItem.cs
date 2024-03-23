using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CancellationPolicyLanguageItem : BaseEntity
    {
        public string Content { get; set; }
        public int LangaugeID { get; set; }

        [ForeignKey("CancellationPolicy")]
        public Guid CancellationPolicyID { get; set; }
        public CancellationPolicy CancellationPolicy { get; set; }
        


    }
}
