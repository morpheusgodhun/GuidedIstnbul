using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Agent
{
    public class GetUserManagementDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<AgentUserDto> Users { get; set; }
        public List<SelectListOptionDto> Roles { get; set; }
        public List<SelectListOptionDto> LanguageKnowOptions { get; set; }
        public List<SelectListOptionDto> SpecializeCityOptions { get; set; }

    }
    public class AgentUserDto
    {
        public string UserID { get; set; }
        public bool Status { get; set; }
        public List<string> Roles { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsWaitingForApprove { get; set; } = false;
    }

    public class AgentNewUserDto
    {
        public Guid? Agent { get; set; }
        public string Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public Guid? Role { get; set; }

    }
    public class PassiveUserPostDto
    {
        public Guid Agent { get; set; }
        public string Email { get; set; }
        public bool IsWaitingForApprove { get; set; } = false;

    }

}
