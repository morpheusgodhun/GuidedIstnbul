
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dto.ApiWebDtos.ApiToWebDtos.Contact
{
    public class GetContactDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public PageDto ContactPage { get; set; }
        //public List<SelectListOptionDto> FindUsOptions { get; set; }
        //public List<SelectListOption> CountryOptions { get; set; }
        public List<ContactInfoDto> ContactInfos { get; set; }

    }

    public class ContactInfoDto
    {
        public int Type { get; set; }
        public string Info { get; set; }
    }


}
