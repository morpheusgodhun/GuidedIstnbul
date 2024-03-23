using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Guide
{
    public class GetTourServedDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<TourServedTourDto> Tours { get; set; }
    }
    public class TourServedTourDto
    {
        public string TourID { get; set; }
        public DateTime Date { get; set; }
        public string ProgramDetail { get; set; }
        public string TourTitle { get; set; }
        public string City { get; set; }
    }
    public class TourServedCustomerDto
    {
        public string UserID { get; set; }
        public string Username { get; set; }
    }
}
