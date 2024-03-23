using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SystemOption : BaseEntity
    {
        public int Order { get; set; }
        public int SystemOptionCategoryID { get; set; }


        //SystemOptionLanguageItem
        public ICollection<SystemOptionLanguageItem> SystemOptionLanguageItems { get; set; }
        public ICollection<Many_Tour_Inclusion> Many_Tour_Inclusions { get; set; }
        public ICollection<Many_Tour_Exclusion> Many_Tour_Exclusions { get; set; }
        public ICollection<Many_Tour_SightToSee> Many_Tour_SightToSees { get; set; }
        public ICollection<Many_ReservationForm_AlsoInterested> Many_ReservationForm_AlsoInteresteds { get; set; }
        public ICollection<CustomTourRequest> CustomTourRequests { get; set; }
        public ICollection<Many_CustomTourRequest_AlsoInterested> Many_CustomTourRequest_AlsoInteresteds { get; set; }

    }
}
