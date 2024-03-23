using Core.Entities;
using Dto.ApiPanelDtos.AgentDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Agent;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IAgentUserRequestService : IGenericService<AgentUserRequest>
    {
        List<AgentUserRequestListDto> GetAgentUserRequestList(string agentId); //acenteye ait kullanıcı istekleri
        /// <summary>
        ///  acente kullanıcı onayı için beklemede olan kullanıcılar
        ///  user management sayfasındaki users datasına eklemek için yazdım 
        ///  Mehmet Ali
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns></returns>
        List<AgentUserDto> GetWaitingUsers(string agentId);
        /// <summary>
        /// web acente user management -> daha sonuçlanmamış bir agentUserRequesti bulunan bir user için remove işlemi gerçekleştirildiğinde
        /// </summary>
        /// <param name="email"></param>
        void WaitingUserRemoved(string email);
        bool IsRequestExistForUser(string email);
        void ApproveUserRequest(string requestId);
        void RejectUserRequest(string requestId);
        CustomResponseDto<bool> CreateUserRequest(AgentNewUserDto request);
        CustomResponseDto<bool> CreateAgentGuideUserRequest(AddAgentGuideUserDto request);

    }
}
