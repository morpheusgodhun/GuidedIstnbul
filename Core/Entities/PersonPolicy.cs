using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PersonPolicy : BaseEntity
    {
        public int FromPerson { get; set; }
        public int ToPerson { get; set; }
        public int Order { get; set; }


        //AdditionalServiceOption Many-To-Many
        public ICollection<AdditionalServiceOptionPrice> AdditionalServiceOptionPrices { get; set; }
    }
}
