using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_AdditionalServiceOption_AdditionalServiceInput : BaseEntity
    {
        [ForeignKey("AdditionalServiceOption")]
        public Guid AdditionalServiceOptionID { get; set; }
        public AdditionalServiceOption AdditionalServiceOption { get; set; }

        [ForeignKey("AdditionalServiceInput")]
        public Guid AdditionalServiceInputID { get; set; }
        public AdditionalServiceInput AdditionalServiceInput { get; set; }
    }
}
