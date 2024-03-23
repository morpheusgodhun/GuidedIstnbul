using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TourLanguageItem : BaseEntity
    {
        public string? Content { get; set; }
        public string? DurationText { get; set; }
        public string? TourStartPoint { get; set; }
        public string? TourEndPoint { get; set; }
        public int LanguageID { get; set; }

        //Tour

        [ForeignKey("Tour")]
        public Guid TourID { get; set; }
        public Tour Tour { get; set; }


    }
}
