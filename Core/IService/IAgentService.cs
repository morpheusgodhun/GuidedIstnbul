using Core.Entities;
using Dto.ApiPanelDtos.AgentDtos;
using Dto.ApiPanelDtos.ProductDtos.GeneralDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IAgentService : IGenericService<Agent>
    {
        void AddAgent(ApplyAgentPostDto addAgentDto);
        void ApproveAgent(AgentApprove model);
        void RejectAgent(AgentApprove model);
        CustomResponseDto<List<ProductNameAndIdDto>> GetProductAdvanceDiscount();
       

    }
}
