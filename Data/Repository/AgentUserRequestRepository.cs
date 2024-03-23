using Core.Entities;
using Core.IRepository;
using Core.StaticValues;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AgentUserRequestRepository : GenericRepository<AgentUserRequest>, IAgentUserRequestRepository
    {
        public AgentUserRequestRepository(Context context) : base(context)
        {
        }
        IQueryable<AgentUserRequest> RequestQueryable => _context.AgentUserRequests.Include(x => x.User).Include(x => x.Agent).Where(x => x.Status && !x.IsDeleted);

        public AgentUserRequest GetAgentUserRequestById(Guid requestId)
        {
            return RequestQueryable.FirstOrDefault(x => x.Id == requestId);
        }

        public AgentUserRequest GetAgentUserRequestByUserEmail(string userMail)
        {
            return RequestQueryable.FirstOrDefault(x => x.User.Email == userMail && x.ApproveStatus == (int)AgentUserRequestStatus.AgentUserRequestApproveStatus.Waiting);
        }

        public List<AgentUserRequest> GetAgentUserRequests()
        {
            //all
            return RequestQueryable.ToList();
        }

        public List<AgentUserRequest> GetAgentUserRequests(string agentId)
        {
            return RequestQueryable.Where(x => x.AgentId == new Guid(agentId)).ToList();
        }
    }
}
