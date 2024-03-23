using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TourPrice : BaseEntity
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? Priority { get; set; }
        public ICollection<TourPriceItem> TourPriceItems { get; set; }

        [ForeignKey("Tour")]
        public Guid TourID { get; set; }
        public Tour Tour { get; set; }



    }
}
