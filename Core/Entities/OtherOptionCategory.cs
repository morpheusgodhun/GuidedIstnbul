using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class OtherOptionCategory : BaseEntity
    {
        public string Name { get; set; }


        //OtherOption
        public ICollection<OtherOption> OtherOptions { get; set; }
    }
}
