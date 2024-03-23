using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticValues
{
    public class CustomTourOfferStatus
    {
        public List<SelectListOption> Status = new List<SelectListOption>()
        {
            new SelectListOption() { ID = 1, Value = "Waiting Answer" },
            new SelectListOption() { ID = 2, Value = "Rejected" },
            new SelectListOption() { ID = 3, Value = "Confirmed" },
            new SelectListOption() { ID = 4, Value = "Update Request" },
        };

        public string GetValue(int id)
        {
            return new CustomTourOfferStatus().Status.FirstOrDefault(x => x.ID == id).Value;
        }
        
        public enum OfferStatus
        {
            WaitingAnswer = 1,
            Rejected = 2,
            Confirmed = 3,
            UpdateRequest = 4,
        }
        
    }
}
