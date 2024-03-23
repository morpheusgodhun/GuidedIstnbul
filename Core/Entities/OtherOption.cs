using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class OtherOption : BaseEntity
    {
        public string Name { get; set; }
        public int Order { get; set; }


        //OtherOptionCategory

        [ForeignKey("OtherOptionCategory")]
        public Guid OtherOptionCategoryID { get; set; }
        public OtherOptionCategory OtherOptionCategory { get; set; }
    }
}
