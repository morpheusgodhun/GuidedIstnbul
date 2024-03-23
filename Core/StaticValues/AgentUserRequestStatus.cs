using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticValues
{
    public class AgentUserRequestStatus
    {
        public List<SelectListOption> Status = new()
        {
            new() { ID = 1, Value = "Approved" },
            new() { ID = 2, Value = "Rejected" },
            new() { ID = 3, Value = "Waiting" },
        };
        public enum AgentUserRequestApproveStatus
        {
            Approved = 1,
            Rejected = 2,
            Waiting = 3,
        }
    }
}
