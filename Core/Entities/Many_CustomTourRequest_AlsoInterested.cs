using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_CustomTourRequest_AlsoInterested : BaseEntity
    {

        public Guid CustomTourRequestId { get; set; }
        public CustomTourRequest CustomTourRequest { get; set; }
        public Guid SystemOptionId { get; set; }
        public SystemOption SystemOption { get; set; }

    }
}
