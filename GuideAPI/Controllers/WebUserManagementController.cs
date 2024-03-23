using Azure.Core;
using Core.Entities;
using Core.IService;
using Core.StaticValues;
using Dto.ApiWebDtos.ApiToWebDtos.Agent;
using Dto.ApiWebDtos.ApiToWebDtos.Faq;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebUserManagementController : ControllerBase
    {
        private readonly IUserService _userService;
        readonly IAgentUserRequestService _agentUserRequestService;

        public WebUserManagementController(IUserService userService, IAgentUserRequestService agentUserRequestService)
        {
            _userService = userService;
            _agentUserRequestService = agentUserRequestService;
        }


        [HttpGet("{memberAgentId}/{LanguageID}")]
        public CustomResponseDto<GetUserManagementDto> GetUserManagement(Guid memberAgentId, string LanguageID)
        {
            /*
             var getHomeDto = new GetHomeDto()
            {
                ConstantValues = _constantValueService.GetConstantValueByPageName("Home", languageId),
                BestDealTours = _tourService.BestDealTourList(languageId),
                PrivateTours = _tourService.PrivateTourList(languageId),
                TurkeyTourPackages = _tourService.TurkeyTourList(languageId),
                Blogs = _blogService.HomeBlogList(languageId),
                AboutPage = _pageService.GetByPageName("About", languageId)
            };
             */

            GetUserManagementDto getUserManagementDto = new GetUserManagementDto()
            {
                ConstantValues = new List<ConstantValueDto>()
                {
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.Title",
                        Value = "User Managament"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.EmailPlaceholder",
                        Value = "Your E-mail Address"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.AddButton",
                        Value = "Add"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.Table.UpdateLabel",
                        Value = "Update"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.Table.StatusLabel",
                        Value = "Status"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.Table.RoleLabel",
                        Value = "Roles"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.Table.FullnameLabel",
                        Value = "Name Surname"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.Table.MailLabel",
                        Value = "E-mail"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.Table.PhoneLabel",
                        Value = "Phone"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.Table.UpdateButton",
                        Value = "Update"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.Table.WaitingForApprove",
                        Value = "Waiting For Approve"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.Table.ActiveButton",
                        Value = "Active"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.Table.PassiveButton",
                        Value = "Passive"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.AddModal.Title",
                        Value = "Add User"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.AddModal.FullnameLabel",
                        Value = "Fullname"
                    },

                    new ConstantValueDto()
                    {
                        Key = "UserManagement.AddModal.RoleLabel",
                        Value = "Role"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.AddModal.RolePlaceholder",
                        Value = "SelectRole"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.AddModal.AddButton",
                        Value = "Add"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.AddModal.CancelButton",
                        Value = "Cancel"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.UpdateModal.Title",
                        Value = "Update User"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.UpdateModal.FullnameLabel",
                        Value = "Fullname"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.UpdateModal.FullnamePlaceholder",
                        Value = "Name Surname"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.UpdateModal.RoleLabel",
                        Value = "Role"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.UpdateModal.RolePlaceholder",
                        Value = "SelectRole"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.UpdateModal.UpdateButton",
                        Value = "Update"
                    },
                    new ConstantValueDto()
                    {
                        Key = "UserManagement.UpdateModal.CancelButton",
                        Value = "Cancel"
                    },

                },
                Roles = new List<SelectListOptionDto>()
                {
                    //new ConstantRoles().GetRoleTemplate( (int)ConstantRoles.No.Agent ),  yeni agent admin eklenebilsin mi 
                     ConstantRoles.GetRoleTemplate( (int)ConstantRoles.No.AgentGuide ),
                     ConstantRoles.GetRoleTemplate( (int)ConstantRoles.No.AgentAccounter ),
                     ConstantRoles.GetRoleTemplate( (int)ConstantRoles.No.AgentDriver ),

                    //new SelectListOptionDto()
                    //{
                    //    OptionID = "1",
                    //    Option = "Admin"
                    //},
                    //new SelectListOptionDto()
                    //{
                    //    OptionID = "2",
                    //    Option = "Guide"
                    //},
                    //new SelectListOptionDto()
                    //{
                    //    OptionID = "3",
                    //    Option = "Accounter"
                    //},
                    //new SelectListOptionDto()
                    //{
                    //    OptionID = "4",
                    //    Option = "Driver"
                    //},
                    //new SelectListOptionDto()
                    //{
                    //    OptionID = "5",
                    //    Option = "Operator"
                    //},
                    //new SelectListOptionDto()
                    //{
                    //    OptionID = "6",
                    //    Option = "Content Creater"
                    //},
                },
                Users = _userService.Where(u => u.AgentId == memberAgentId && u.Status && !u.IsDeleted).Select(z => new AgentUserDto()
                {
                    UserID = z.Id.ToString(),
                    Fullname = z.Name + " " + z.Surname,
                    Email = z.Email,
                    Status = z.Status,
                    Roles = ConstantRoles.SpecialRoleTemplatesGuid.Where(rp => rp.OptionID == z.RoleTemplateId).Select(c => c.Option).ToList(),
                    Phone = z.Phone
                }).ToList()

                /*
                new List<AgentUserDto>()
                {
                    new AgentUserDto()
                    {
                        UserID = "1",
                        Fullname = "Ecmel SADIKOĞLU",
                        Email = "ecmel@gmail.com",
                        Phone = "0569 345 3134",
                        Status = true,
                        Roles = new List<string>(){"Admin"}
                    },
                    new AgentUserDto()
                    {
                        UserID = "2",
                        Fullname = "Metin ÖNAL",
                        Email = "metin@gmail.com",
                        Phone = "0539 348 5013",
                        Status = false,
                        Roles = new List<string>(){ "IT,Muhasebe" }
                    },
                    new AgentUserDto()
                    {
                        UserID = "3",
                        Fullname = "Hasan YILMAZ",
                        Email = "hasan@gmail.com",
                        Phone = "0534 213 4291",
                        Status = true,
                        Roles = new List<string>(){ "Driver " }
                    },
                }*/
            };
            var usersWaitingApprove = _agentUserRequestService.GetWaitingUsers(memberAgentId.ToString());
            getUserManagementDto.Users.AddRange(usersWaitingApprove);
            return CustomResponseDto<GetUserManagementDto>.Success(200, getUserManagementDto);
        }

        [HttpPost]
        public CustomResponseDto<AgentNewUserDto> GetNewUserInfo(AgentNewUserDto searchMember)
        {
            //aynı kullanıcı için ikinci kez istek açılamasın

            var isRequestExistForUser = _agentUserRequestService.IsRequestExistForUser(searchMember.Email);
            if (isRequestExistForUser)
                return CustomResponseDto<AgentNewUserDto>.Fail(429, "You already have request for this user!");

            // bir agente en fazla 10 kullanıcı eklenebilsin diye ayarlayacağım
            var agentUserCount = _userService.Where(u => u.AgentId == searchMember.Agent).Count();

            if (agentUserCount < 10)
            {
                //sadece bizde zaten member olanlar ve hiçbir agente bağlı olmayan kişileri getiricem
                var memberRoleTemplateId = ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Member).OptionID;

                var findUser = _userService.Where(u => u.Email == searchMember.Email && u.AgentId == null && u.RoleTemplateId == memberRoleTemplateId).Select(z => new AgentNewUserDto()
                {
                    Name = z.Name,
                    Surname = z.Surname,
                    Email = z.Email,
                    Phone = z.Phone,
                }).FirstOrDefault();

                if (findUser != null)
                    return CustomResponseDto<AgentNewUserDto>.Success(200, findUser);

                return CustomResponseDto<AgentNewUserDto>.Fail(404, "User not found"); // kullanıcı bulunamadı
            }

            return CustomResponseDto<AgentNewUserDto>.Fail(429, "Max user count : 10"); //toomany request
        }

        [HttpPost]
        public CustomResponseDto<bool> AddNewUser(AgentNewUserDto agentNewUserDto)
        {
            return _agentUserRequestService.CreateUserRequest(agentNewUserDto);
        }

        [HttpPost]
        public CustomResponseDto<bool> AddNewAgentGuideUser(AddAgentGuideUserDto dto)
        {
            return _agentUserRequestService.CreateAgentGuideUserRequest(dto);
        }

        [HttpPost]
        public CustomResponseDto<AgentNewUserDto> UpdateUser(AgentNewUserDto UpdateUser)
        {

            var memberRoleTemplateId = ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Member).OptionID;
            var AgentRoleTemplateId = ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Member).OptionID;

            var findUser = _userService.Where(u =>
                                                u.Email == UpdateUser.Email
                                                && u.AgentId == UpdateUser.Agent // bizim firmada çalışsın
                                                && u.RoleTemplateId != AgentRoleTemplateId // agent admin olmasın -> birden fazla agent admin varsa buraya bakmak lazım
                                                && u.RoleTemplateId != memberRoleTemplateId // birde member olmasın zaten bizim yetkili rollerden biri olsun
                                                ).FirstOrDefault();

            if (findUser == null)
                return CustomResponseDto<AgentNewUserDto>.Success(200, UpdateUser);

            findUser.RoleTemplateId = (Guid)UpdateUser.Role;

            _userService.Update(findUser);

            return CustomResponseDto<AgentNewUserDto>.Success(200, UpdateUser);
        }


        [HttpPost]
        public CustomResponseDto<PassiveUserPostDto> PassiveUser(PassiveUserPostDto passiveUser)
        {
            var memberRoleTemplateId = ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Member).OptionID;
            var AgentRoleTemplateId = ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Agent).OptionID;
            User findUser;
            if (passiveUser.IsWaitingForApprove)
            {
                var isRequestExist = _agentUserRequestService.IsRequestExistForUser(passiveUser.Email);
                if (isRequestExist)
                {
                    findUser = _userService.Where(u => u.Email == passiveUser.Email
                    && u.RoleTemplateId != AgentRoleTemplateId).FirstOrDefault();
                }
                else
                    findUser = null;
            }
            else
            {
                findUser = _userService.Where(u =>
                                                   u.Email == passiveUser.Email
                                                   && u.AgentId == passiveUser.Agent // bizim firmada çalışsın
                                                                                     //&& u.RoleTemplateId == passiveUser.Role
                                                   && u.RoleTemplateId != AgentRoleTemplateId //ana kullanıcıyı pasifleme olmasın
                                                   ).FirstOrDefault();

            }

            if (findUser == null)
                return CustomResponseDto<PassiveUserPostDto>.Success(200, passiveUser);

            findUser.AgentId = null;
            findUser.RoleTemplateId = memberRoleTemplateId;

            _userService.Update(findUser);

            if (passiveUser.IsWaitingForApprove)
            {
                _agentUserRequestService.WaitingUserRemoved(passiveUser.Email);
            }

            return CustomResponseDto<PassiveUserPostDto>.Success(200, passiveUser);

        }

    }
}
