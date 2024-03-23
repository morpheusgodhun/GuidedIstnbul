using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Tour
{
    public class GetTourFilterDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<SelectListOptionDto> Categories { get; set; } = new List<SelectListOptionDto>();
        //public List<SelectListOptionDto> Cities { get; set; }
        public List<SelectListOptionDto> Destinations { get; set; } //Durations dı değiştirdim
        public List<SelectListOptionDto> Specials { get; set; } = new List<SelectListOptionDto>();
        public List<SelectListOptionDto> Types { get; set; }
        public List<FilterTourDto> Tours { get; set; }
        public PageDto Page { get; set; }
        public List<SelectListOptionDto> Months { get; set; }
    }

    public class GetTourFilterFormDto
    {
        public List<Guid>? ConstantValues { get; set; }
        public List<Guid>? Categories { get; set; }
        public List<Guid>? Destinations { get; set; }
        public List<Guid>? Specials { get; set; }
        public List<Guid?>? Types { get; set; }
        public List<Guid>? Months { get; set; }
    }

    public class FilterTourDto
    {
        public Guid ProductId { get; set; }
        public string CardImagePath { get; set; }
        public decimal Price { get; set; }
        public decimal Rate { get; set; }
        public string TourName { get; set; }
        public bool IsPerPerson { get; set; }
        public int Order { get; set; }
        public bool AskForPrice { get; set; }
        public List<string> CategoryNames { get; set; }
        public List<string> TagNames { get; set; }
        public List<Guid> CategoryIDs { get; set; }
        public List<Guid> DurationIDs { get; set; }
        public List<Guid> InclusionIDs { get; set; }
        public Guid? TourTypeId { get; set; }
        public Guid TourId { get; set; }
    }
}
