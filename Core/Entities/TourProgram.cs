using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TourProgram : BaseEntity
    {
        
        public int Day { get; set; }


        //Tour

        [ForeignKey("Tour")]
        public Guid TourID { get; set; }
        public Tour Tour { get; set; }

        //TourProgramLanguageItem
        public ICollection<TourProgramLanguageItem> TourProgramLanguageItems { get; set; }
    }
}
