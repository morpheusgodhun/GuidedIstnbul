
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Profile
{
    public class GetProfileAgentDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public string AgentID { get; set; }
        public string AgentTitle { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public List<SelectListOption> CountryOptions { get; set; }
        public string CountryID { get; set; }
        public List<SelectListOptionDto> CityOptions { get; set; }
        public string CityID { get; set; }
        public string TaxAdministration { get; set; }
        public string TaxNumber { get; set; }
        public string CompanyManager { get; set; }
        public string TradeRegistryNumber { get; set; }
        public string WebsiteLink { get; set; }
        public string Address { get; set; }
        public string LogoPath { get; set; }
    }
}
