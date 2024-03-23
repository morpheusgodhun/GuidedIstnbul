using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Offer : BaseEntity
    {
        public Decimal Price { get; set; }
        public string TourProgram { get; set; }
        public string AdminAnswer { get; set; }
        public string? CustomerAnswer { get; set; }
        public string Answered { get; set; }

        [ForeignKey("CustomTourRequest")]
        public Guid CustomTourRequestId { get; set; }
        public CustomTourRequest CustomTourRequest { get; set; }
    }
}
