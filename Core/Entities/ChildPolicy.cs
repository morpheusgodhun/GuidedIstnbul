using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ChildPolicy : BaseEntity
    {
        public int FromAge { get; set; }
        public int ToAge { get; set; }
        public int Percent { get; set; }
        public int Order { get; set; }
    }
}
