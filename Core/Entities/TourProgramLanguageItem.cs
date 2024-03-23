using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TourProgramLanguageItem : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int LanguageID { get; set; }

        //TourProgram

        [ForeignKey("TourProgram")]
        public Guid TourProgramID { get; set; }
        public TourProgram TourProgram { get; set; }

    }
}
