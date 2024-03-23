

using Dto.ApiWebDtos.GeneralDtos;
using System.Security.Cryptography.X509Certificates;

namespace Core.StaticClass
{
    public static class SystemOptionCategoryList
    {
        public static List<SelectListOption> CategoryList = new List<SelectListOption>()
        {
            new SelectListOption()
            {
                ID = 1,
                Value = "Segment", // daily tour accomendation tour vs.
            },
            new SelectListOption()
            {
                ID = 2,
                Value = "Tour Type", // best seller mi, private mi (vustomda burada ama kaldırlıacak)
            },
            new SelectListOption()
            {
                ID = 3,
                Value = "Inclusion Exclusion", //filternin yanıda dahil yada değil transportation dahis gibi "2 farklı tablomany de tutuluyor
            },
            new SelectListOption()
            {
                ID = 4,
                Value = "Sight To See", // sultan ahmet vb nereleri gezecekelri yazıyor
            },
            new SelectListOption()
            {
                ID = 5,
                Value = "How Did You Find Us?",
            },
            new SelectListOption()
            {
                ID = 6,
                Value = "Also Interested",
            },
            new SelectListOption()
            {
                ID = 7,
                Value = "Month List",
            },
            new SelectListOption()
            {
                ID = 8,
                Value = "TourLanguages",
            },
            new SelectListOption()
            {
                ID = 9,
                Value = "TourCities",
            }, new SelectListOption()
            {
                ID = 10,
                Value = "Cancellation Reason",
            },

            //cancel reaseon ekleneceği zaman buradan eklenecek
        };
        public enum SystemOperations
        {
            Segment = 1,
            TourType = 2,
            InclusionExclusion = 3,
            SightToSee = 4,
            HowDidYouFindUs = 5,
            AlsoInterested = 6,
            MonthList = 7,
            TourLanguages = 8,
            TourCities = 9,
            CancellationReason = 10,
        }

    }


}
