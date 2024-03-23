using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Tour : BaseEntity
    {
        [ForeignKey("TourType")]
        public Guid? TourTypeID { get; set; } //Enum
        public SystemOption? TourType { get; set; }


        [ForeignKey("Segment")]
        public Guid? SegmentID { get; set; }
        public SystemOption? Segment { get; set; }

        public bool AskForPrice { get; set; }
        public int? StartCityID { get; set; }
        public string? StartTimeIDs { get; set; }
        public int? SuggestedStartTimeID { get; set; }
        public int Duration { get; set; }
        public string? SelectableDurations { get; set; }

        [ForeignKey("CustomTourRequest")]
        public Guid? CustomTourRequestId { get; set; }
        public CustomTourRequest? CustomTourRequest { get; set; }

        public bool IsPerPerson { get; set; }  // If 0 then its per group
        public bool IsPerDay { get; set; }  // If 0 then its per Tour



        public ICollection<Many_Tour_Inclusion> Many_Tour_Inclusions { get; set; }
        public ICollection<Many_Tour_Exclusion> Many_Tour_Exclusions { get; set; }
        public ICollection<Many_Tour_SightToSee> Many_Tour_SightToSees { get; set; }

        //Region Many-To-Many
        public ICollection<Many_Tour_Destination> Many_Tour_Destinations { get; set; }

        //TourCategory Many-To-Many
        public ICollection<Many_Tour_TourCategory> Many_Tour_TourCategories { get; set; }

        //TourLanguageItem
        public ICollection<TourLanguageItem> TourLanguageItems { get; set; }

        //TourProgram
        public ICollection<TourProgram> TourPrograms { get; set; }


        //Product

        [ForeignKey("Product")]
        public Guid ProductID { get; set; }
        public Product Product { get; set; }


    }
}
