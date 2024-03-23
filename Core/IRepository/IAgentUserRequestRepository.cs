using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IAgentUserRequestRepository : IGenericRepository<AgentUserRequest>
    {
        List<AgentUserRequest> GetAgentUserRequests();
        List<AgentUserRequest> GetAgentUserRequests(string agentId);
        AgentUserRequest GetAgentUserRequestById(Guid requestId);
        AgentUserRequest GetAgentUserRequestByUserEmail(string userMail);
    }
}
