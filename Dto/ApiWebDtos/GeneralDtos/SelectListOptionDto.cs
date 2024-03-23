using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.GeneralDtos
{
    public class SelectListOptionDto
    {
        public SelectListOptionDto()
        {

        }
        public SelectListOptionDto(Guid optionID, string option)
        {
            OptionID = optionID;
            Option = option;
        }

        public Guid OptionID { get; set; } //veritabanından çekerken bu select kullanılır
        public string Option { get; set; }
    }
    public class OrderedSelectListOptionDto
    {
        public OrderedSelectListOptionDto()
        {

        }
        public OrderedSelectListOptionDto(Guid optionID, string option, int order)
        {
            OptionID = optionID;
            Option = option;
            Order = order;
        }

        public Guid OptionID { get; set; }
        public string Option { get; set; }
        public int Order { get; set; }
    }
}
