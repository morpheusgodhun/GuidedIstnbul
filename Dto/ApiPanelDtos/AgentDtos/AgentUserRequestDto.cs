using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.AgentDtos
{
    public class AgentUserRequestListDto
    {
        public AgentUserRequestListDto(string requestId, string userName, string email, string role, string approveStatus, DateTime createdDate)
        {
            RequestId = requestId;
            UserName = userName;
            Email = email;
            Role = role;
            ApproveStatus = approveStatus;
            CreatedDate = createdDate;
        }
        public AgentUserRequestListDto()
        {

        }
        public string RequestId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string ApproveStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
