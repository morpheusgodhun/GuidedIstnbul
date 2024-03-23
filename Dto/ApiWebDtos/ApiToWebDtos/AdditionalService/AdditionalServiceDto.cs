using Dto.ApiWebDtos.ApiToWebDtos.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.AdditionalService
{
    public class AdditionalServiceDto
    {
        public Guid AdditionalServiceID { get; set; }
        public string AdditionalServiceName { get; set; }
        public List<AdditionalServiceOptionDto> Options { get; set; }
        public bool? IsPerPerson { get; set; }
        public bool? IsPerDay { get; set; }
        public bool Isrequired { get; set; }
        public bool IsSpecialNumber { get; set; }
    }
    public class AdditionalServiceOptionDto
    {
        public Guid OptionID { get; set; }
        public string OptionName { get; set; }
        public decimal OptionPrice { get; set; }
        public int Order { get; set; }
        public List<AdditionalServiceOptionInputDto> Inputs { get; set; }
        public List<AdditionalServicePriceDto> OptionAllPrices { get; set; } //bu optionun alakalı bütün fiyatlar gelsin
    }
    public class AdditionalServiceOptionInputDto
    {
        public Guid InputID { get; set; }
        public string InputName { get; set; }
        public int InputType { get; set; }
        public int Order { get; set; }
    }
}
