using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Tour
{
    public class GetCustomMadeTourFormDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<SelectListOptionDto> Destinations { get; set; }
        public CustomMadeTourFormTourInfo TourInfoForm { get; set; }
        public CustomMadeContactDetail ContactDetailForm { get; set; }
        public CustomMadeInformation InformationForm { get; set; }
    }

    public class CustomMadeTourFormTourInfo
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<TourInfoFormChildPolicyPlaceholder> ChildPolicyPlaceholder { get; set; }  //Yaş kısmına ne yazacağını belirlemek ve kaç tane Child Alanının açılacağını belirlemek için
    }
    public class TourInfoFormChildPolicyPlaceholder
    {
        public string ChildPolicyID { get; set; }
        public int FromAge { get; set; }
        public int ToAge { get; set; }

    }
    public class CustomMadeContactDetail
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<SelectListOptionDto> CountryOptions { get; set; }

    }

    public class CustomMadeInformation
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<SelectListOptionDto> HowFindUsOptions { get; set; }
    }
}
