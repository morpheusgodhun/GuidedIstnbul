using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TourCategory : BaseEntity
    {
        public string IconPath { get; set; }
        public string BannerImagePath { get; set; }

        //TourCategoryLanguageItem
        public ICollection<TourCategoryLanguageItem> TourCategoryLanguageItems { get; set; }

        //Tour Many-To-Many
        public ICollection<Many_Tour_TourCategory> Many_Tour_TourCategories { get; set; }
        //Tour Many-To-Many
        public ICollection<Many_Discount_TourCategory> Many_Discount_TourCategories { get; set; }
    }
}
