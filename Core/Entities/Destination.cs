using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Destination : BaseEntity
    {
        public int Order { get; set; }
        public bool ShowOnCustomMade { get; set; }

        //DestinationLanguageItem
        public ICollection<DestinationLanguageItem> DestinationLanguageItems { get; set; }


        public ICollection<Many_Tour_Destination> Many_Tour_Destinations { get; set; }
        public ICollection<Many_CustomTourRequest_Destination> Many_CustomTourRequest_Destinations { get; set; }


    }
}
