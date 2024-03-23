using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticValues
{
    //update request ve cancel request için kullanılacak olan statusler
    public class ReservationEditRequestStatus
    {
        public List<SelectListOption> Status = new()
        {
            new() { ID = 1, Value = "Approved" },
            new() { ID = 2, Value = "Rejected" },
            new() { ID = 3, Value = "Waiting" },
        };

        public static string GetValue(int id)
        {
            return new ReservationEditRequestStatus().Status.FirstOrDefault(x => x.ID == id).Value;
        }

        public enum ReservationEditRequestStatusEnum
        {
            Onaylandi = 1,
            Reddedildi = 2,
            Beklemede = 3,
        }

    }
}
