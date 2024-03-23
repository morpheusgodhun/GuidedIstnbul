using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_Product_AdditionalServiceOption : BaseEntity
    {
        [ForeignKey("Many_Product_AdditionalService")]
        public Guid Many_Product_AdditionalServiceID { get; set; }
        public Many_Product_AdditionalService Many_Product_AdditionalService { get; set; }

        [ForeignKey("AdditionalServiceOption")]
        public Guid AdditionalServiceOptionID { get; set; }
        public AdditionalServiceOption AdditionalServiceOption { get; set; }

    }
}
