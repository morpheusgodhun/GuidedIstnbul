using Azure.Core;
using Core;
using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticValues;
using Data.Migrations;
using Dto.ApiPanelDtos.AgentDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Agent;
using Dto.ApiWebDtos.GeneralDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using Service.Mail;

namespace Service.Services
{
    public class AgentUserRequestService : GenericService<AgentUserRequest>, IAgentUserRequestService
    {
        private readonly IAgentUserRequestRepository _agentUserRequestRepository;
        readonly IRoleTemplateService _roleTemplateService;
        readonly IUserService _userService;
        readonly IMailSenderService _mailSender;
        readonly IGuideCityService _guideCityService;
        readonly IGuideLanguageService _guideLanguageService;
        readonly IGuideRepository _guideRepository;
        public AgentUserRequestService(IGenericRepository<AgentUserRequest> repository, IUnitOfWork unitOfWork, IAgentUserRequestRepository agentUserRequestRepository, IRoleTemplateService roleTemplateService, IUserService userService, IMailSenderService mailSender, IGuideLanguageService guideLanguageService, IGuideCityService guideCityService, IGuideRepository guideRepository) : base(repository, unitOfWork)
        {
            _agentUserRequestRepository = agentUserRequestRepository;
            _roleTemplateService = roleTemplateService;
            _userService = userService;
            _mailSender = mailSender;
            _guideLanguageService = guideLanguageService;
            _guideCityService = guideCityService;
            _guideRepository = guideRepository;
        }

        // user roleTemplate i onaylanana kadar eski rol template inde kalmaya devam ediyor. onaylanırsa rol template i güncelleniyor
        public void ApproveUserRequest(string requestId)
        {
            AgentUserRequest userRequest = _agentUserRequestRepository.GetAgentUserRequestById(new Guid(requestId));
            User requestUser = userRequest.User;

            Guid agentGuideRoleTemplateId = ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.AgentGuide).OptionID;
            if (userRequest.RoleTemplateId == agentGuideRoleTemplateId)
            {
                Guide waitingGuide = _guideRepository.GetById((Guid)requestUser.GuideId);
                waitingGuide.ApproveStatus = (int)Enums.ApproveStatus.Onaylandi;
                _guideRepository.Update(waitingGuide);
            }

            _userService.UpdateUserRoleTemplate(requestUser.Id.ToString(), requestUser.RoleTemplateId.ToString());
            requestUser.AgentId = userRequest.AgentId;
            requestUser.RoleTemplateId = (Guid)userRequest.RoleTemplateId;

            userRequest.ApproveStatus = (int)AgentUserRequestStatus.AgentUserRequestApproveStatus.Approved;
            _agentUserRequestRepository.Update(userRequest);
            _unitOfWork.saveChanges();

            //TODO : schedule mail and send
        }
        public void RejectUserRequest(string requestId)
        {
            var userRequest = _agentUserRequestRepository.GetAgentUserRequestById(new Guid(requestId));
            userRequest.ApproveStatus = (int)AgentUserRequestStatus.AgentUserRequestApproveStatus.Rejected;
            _agentUserRequestRepository.Update(userRequest);

            Guid agentGuideRoleTemplateId = ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.AgentGuide).OptionID;
            if (userRequest.RoleTemplateId == agentGuideRoleTemplateId)
            {
                User requestUser = userRequest.User;
                Guide waitingGuide = _guideRepository.GetById((Guid)requestUser.GuideId);

                waitingGuide.ApproveStatus = (int)Enums.ApproveStatus.Reddedildi;

                requestUser.GuideId = null;
                //_guideRepository.Delete(waitingGuide.Id); //isDeleted false yapıyor
                _guideRepository.Remove(waitingGuide); //dbden siliyor
            }

            _unitOfWork.saveChanges();
            //TODO : schedule mail and send
        }
        public List<AgentUserRequestListDto> GetAgentUserRequestList(string agentId)
        {
            var statusList = new AgentUserRequestStatus().Status;
            var repoResponse = _agentUserRequestRepository.GetAgentUserRequests(agentId);
            var list = repoResponse.Select(x => new AgentUserRequestListDto
            {
                RequestId = x.Id.ToString(),
                UserName = $"{x.User.Name} {x.User.Surname}",
                Email = x.User.Email,
                Role = _roleTemplateService.GetRoleTemplateName(x.RoleTemplateId.ToString()),
                ApproveStatus = statusList.Where(status => status.ID == x.ApproveStatus).FirstOrDefault().Value,
                CreatedDate = x.CreateDate
            }).OrderByDescending(req => req.CreatedDate).ToList();
            return list;
        }

        public List<AgentUserDto> GetWaitingUsers(string agentId)
        {
            var roleList = ConstantRoles.SpecialRoleTemplatesGuid;
            var waitingRequests = _agentUserRequestRepository.GetAgentUserRequests(agentId).Where(req => req.ApproveStatus == (int)AgentUserRequestStatus.AgentUserRequestApproveStatus.Waiting);

            if (waitingRequests.Any())
            {
                var returnList = waitingRequests.Select(x => new AgentUserDto
                {
                    UserID = x.User.Id.ToString(),
                    Email = x.User.Email,
                    Fullname = $"{x.User.Name} {x.User.Surname}",
                    IsWaitingForApprove = true,
                    Phone = x.User.Phone,
                    Roles = new List<string>
                {
                    roleList.Where(role => role.OptionID == x.RoleTemplateId).FirstOrDefault().Option
                },
                    Status = x.User.Status,
                }).ToList();
                return returnList;
            }
            return new();
        }
        /// <summary>
        /// web acente user management -> daha sonuçlanmamış bir agentUserRequesti bulunan bir user için remove işlemi gerçekleştirildiğinde
        /// </summary>
        /// <param name="email"></param>
        public void WaitingUserRemoved(string email)
        {
            AgentUserRequest removedUserRequest = _agentUserRequestRepository.GetAgentUserRequestByUserEmail(email);
            removedUserRequest.Status = false;
            removedUserRequest.IsDeleted = true;
            _agentUserRequestRepository.Update(removedUserRequest);
            _unitOfWork.saveChanges();
        }
        public bool IsRequestExistForUser(string email)
        {
            var agentUserRequest = _agentUserRequestRepository.GetAgentUserRequestByUserEmail(email);
            return agentUserRequest != null;
        }

        public CustomResponseDto<bool> CreateUserRequest(AgentNewUserDto request)
        {
            return CreateRequest(request);
        }

        public CustomResponseDto<bool> CreateAgentGuideUserRequest(AddAgentGuideUserDto request)
        {
            Guid agentGuideRoleTemplateId = ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.AgentGuide).OptionID;

            CustomResponseDto<bool> requestCreateResponse = CreateRequest(new AgentNewUserDto
            {
                Agent = request.Agent,
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                Phone = request.Phone,
                Role = agentGuideRoleTemplateId,
            });
            if (requestCreateResponse.Data)
            {
                Guid memberRoleTemplateId = ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Member).OptionID;
                User findUser = _userService.Where(u => u.Email == request.Email && u.AgentId == null && u.RoleTemplateId == memberRoleTemplateId).FirstOrDefault();

                Guide guide = new()
                {
                    Email = request.Email,
                    Name = request.Name,
                    Surname = request.Surname,
                    Tc = request.Tc,
                    TurebLicenseNumber = request.TursebLicenseNumber,
                    ApproveConstent = true,
                    Phone = request.Phone,
                    BirthDate = request.BirthDate,
                    ApproveStatus = (int)Enums.ApproveStatus.OnayBekliyor,
                    LicenseBackImagePath = request.LicenseBackImagePath,
                    LicenseFrontImagePath = request.LicenseFrontImagePath,
                    ProfilPhotoPath = request.ProfilPhotoPath,
                    RegisteredDirectoryRoom = request.RegisteredDirectoryRoom,
                };
                _guideRepository.Add(guide);
                _unitOfWork.saveChanges();

                findUser.GuideId = guide.Id;

                foreach (var item in request.SpecializeCityIDs)
                {
                    _guideCityService.Add(new GuideCity { GuideId = guide.Id, LanguageId = item });
                }
                foreach (var item in request.LanguageKnowIDs)
                {
                    _guideLanguageService.Add(new GuideLanguage { GuideId = guide.Id, LanguageId = item });
                }
                return CustomResponseDto<bool>.Success(200, true);
            }
            return CustomResponseDto<bool>.Fail(500, "Bir hata oluştu", false);
        }
        private CustomResponseDto<bool> CreateRequest(AgentNewUserDto request)
        {
            var agentUserCount = _userService.Where(u => u.AgentId == request.Agent).Count();
            // bir agente en fazla 10 kullanıcı eklenebilsin 
            if (agentUserCount < 10)
            {
                Guid memberRoleTemplateId = ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Member).OptionID;

                User findUser = _userService.Where(u => u.Email == request.Email && u.AgentId == null && u.RoleTemplateId == memberRoleTemplateId).FirstOrDefault();

                if (findUser == null)
                    return CustomResponseDto<bool>.Fail(404, "Kullanıcı Bulunamadı", false);

                _agentUserRequestRepository.Add(new AgentUserRequest
                {
                    AgentId = (Guid)request.Agent,
                    UserId = findUser.Id,
                    RoleTemplateId = request.Role,
                    ApproveStatus = (int)AgentUserRequestStatus.AgentUserRequestApproveStatus.Waiting
                });
                _unitOfWork.saveChanges();
                return CustomResponseDto<bool>.Success(200, true);
            }
            return CustomResponseDto<bool>.Fail(429, "Max user count : 10", false); //too many request
        }
    }
}
