using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Many_Tour_TourCategory : BaseEntity
    {
        [ForeignKey("Tour")]
        public Guid TourID { get; set; }
        public Tour Tour { get; set; }

        [ForeignKey("TourCategory")]
        public Guid TourCategoryID { get; set; }
        public TourCategory TourCategory { get; set; }
    }
}
