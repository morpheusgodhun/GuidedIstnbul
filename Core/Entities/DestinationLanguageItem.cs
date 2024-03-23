using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class DestinationLanguageItem : BaseEntity
    {
        public string DisplayName { get; set; }
        public int LangaugeID { get; set; }

        //Destination
        [ForeignKey("Destination")]
        public Guid DestinationID { get; set; }
        public Destination Destination { get; set; }



    }
}
