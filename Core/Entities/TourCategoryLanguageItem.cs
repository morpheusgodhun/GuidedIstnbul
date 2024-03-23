using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TourCategoryLanguageItem : BaseEntity
    {
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public int LanguageID { get; set; }

        //TourCategory

        [ForeignKey("TourCategory")]
        public Guid TourCategoryID { get; set; }
        public TourCategory TourCategory { get; set; }

    }
}
