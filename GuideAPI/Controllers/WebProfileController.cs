using Core;
using Core.IService;
using Core.StaticValues;
using Dto.ApiPanelDtos.AgentDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Profile;
using Dto.ApiWebDtos.GeneralDtos;
using Dto.ApiWebDtos.GeneralDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using GuideAPI.Models.GeneralDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebProfileController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IGuideService _guideService;
        private readonly IAgentService _agentService;
        private readonly ISystemOptionService _systemOptionService;

        public WebProfileController(IUserService userService, IGuideService guideService, IAgentService agentService, ISystemOptionService systemOptionService)
        {
            _userService = userService;
            _guideService = guideService;
            _agentService = agentService;
            _systemOptionService = systemOptionService;
        }

        [HttpGet("{memberId}/{LanguageID}")]

        public CustomResponseDto<GetProfileUserDto> GetProfileMember(Guid memberId, string LanguageID)
        {

            var member = _userService.GetById(memberId);

            var getProfileUserDto = new GetProfileUserDto()
            {
                ConstantValues = new List<ConstantValueDto>()
                {
                    new ConstantValueDto()
                    {
                        Key = "ProfileUserTitle",
                        Value = "User Profile"
                    },

                    new ConstantValueDto()
                    {
                        Key = "ProfileUserNamePlaceholder",
                        Value = "Name"
                    },

                     new ConstantValueDto()
                    {
                        Key = "ProfileUserSurnamePlaceholder",
                        Value = "Surname"
                    },

                     new ConstantValueDto()
                    {
                        Key = "ProfileUserEmailPlaceholder",
                        Value = "Your E-mail Address"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileUserSaveButton",
                        Value = "Save"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileUserChangePasswordButton",
                        Value = "Change Password"
                    }
                },
                UserID = member.Id.ToString(),
                Name = member.Name,
                Surname = member.Surname,
                Email = member.Email,
            };

            return CustomResponseDto<GetProfileUserDto>.Success(200, getProfileUserDto);
        }

        [HttpPost]
        public CustomResponseNullDto PostProfileMember(ProfileUserPostDto profileUserPostDto)
        {

            var member = _userService.GetById(new Guid(profileUserPostDto.UserID));

            if (member == null)
                return CustomResponseNullDto.Success(404);

            member.Name = profileUserPostDto.Name;
            member.Surname = profileUserPostDto.Surname;
            //member.Email = profileUserPostDto.Email; //TODO: email değiştirebilme varmı yokmu 
            _userService.Update(member);

            return CustomResponseNullDto.Success(200);
        }

        [HttpGet("{memberId}/{LanguageID}")]
        public CustomResponseDto<GetProfileGuideDto> GetProfileGuide(Guid memberId, string LanguageID)
        {

            List<ConstantValueDto> ConstantValues = new List<ConstantValueDto>()
                {
                    new ConstantValueDto()
                    {
                        Key = "ProfileGuideTitle",
                        Value = "Profile Guide"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileGuideNamePlaceholder",
                        Value = "Name"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileGuideSurnameLabelPlaceholder",
                        Value = "Surname"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileGuideEmailPlaceholder",
                        Value = "E-mail"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileGuidePhonePlaceholder",
                        Value = "05xx xxx xx xx"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileGuideLanguageKnow",
                        Value = "Languages ​​You Know"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileGuideSpecializeCity",
                        Value = "Cities Where You Specialize"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileGuideProfilPhotoLabel",
                        Value = "Profil Photo"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileGuideLicenseFrontLabel",
                        Value = "License Front Side"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileGuideLicenseBackLabel",
                        Value = "License Back Side"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileGuideBirthDateLabel",
                        Value = "Date of Birth"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileGuideTurebLicensePlaceholder",
                        Value = "Tureb License Number"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileGuideTcPlaceholder",
                        Value = "Turkish Identity Number"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileGuideRegisteredDirectoryRoomPlaceholder",
                        Value = "Registered Directory Room"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileGuideSaveButton",
                        Value = "Save"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileGuideChangePasswordButton",
                        Value = "ChangePassword"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileGuideApplyAgentButton",
                        Value = "Apply as an Agent"
                    },
                };
            var guide = _guideService.GetById(memberId);
            var getProfileGuideDto = new GetProfileGuideDto()
            {
                Name = guide.Name,
                Surname = guide.Surname,
                Email = guide.Email,
                Phone = guide.Phone,
                DateBirth = guide.BirthDate,
                LicenseBackImagePath = guide.LicenseBackImagePath,
                LicenseFrontImagePath = guide.LicenseFrontImagePath,
                Tc = guide.Tc,
                RegisteredDirectoryRoom = guide.RegisteredDirectoryRoom,
                TurebLicense = guide.TurebLicenseNumber,
                ProfilPhotoPath = guide.ProfilPhotoPath,
                ConstantValues = ConstantValues
            };

            return CustomResponseDto<GetProfileGuideDto>.Success(200, getProfileGuideDto);
        }
        [HttpPost]
        public CustomResponseNullDto PostProfileGuide(ProfileGuidePostDto profileGuidePostDto)
        {
            //Güncelleme işlemi
            //var test = _guideService.GetAll();
            //var test2 = _guideService.GetAll().Select(x => x.Id).ToList();
            //var test3 = test2.Select(x=>x.ToString()).ToList();
            var guide = _guideService.GetAll().Where(x => x.Id.ToString() == profileGuidePostDto.UserID).FirstOrDefault();  //// todo: bunu böyle yapmak zorunda kaldım düzelticem
            guide.Name = profileGuidePostDto.Name;
            guide.Surname = profileGuidePostDto.Surname;
            guide.BirthDate = profileGuidePostDto.BirthDate;
            guide.Email = profileGuidePostDto.Email;
            guide.LicenseBackImagePath = profileGuidePostDto.LicenseBackImagePath == null ? guide.LicenseBackImagePath : profileGuidePostDto.LicenseBackImagePath;
            guide.LicenseFrontImagePath = profileGuidePostDto.LicenseFrontImagePath == null ? guide.LicenseFrontImagePath : profileGuidePostDto.LicenseFrontImagePath;
            guide.ProfilPhotoPath = profileGuidePostDto.ProfilPhotoPath == null ? guide.ProfilPhotoPath : profileGuidePostDto.ProfilPhotoPath;
            guide.Tc = profileGuidePostDto.Tc;
            guide.UpdateDate = DateTime.Now;
            _guideService.Update(guide);

            return CustomResponseNullDto.Success(200);
        }

        [HttpGet("{memberId}/{LanguageID}")]
        public CustomResponseDto<GetProfileAgentDto> GetAgentInformation(Guid memberId, string LanguageID)
        {
            var ConstantValues = new List<ConstantValueDto>()
                {
                    new ConstantValueDto()
                    {
                        Key = "ProfileAgentTitle",
                        Value = "Agent Information"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileAgentIDLabel",
                        Value = "AgentID"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileAgentTitlePlaceholder",
                        Value = "Agent Title"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileAgentEmailPlaceholder",
                        Value = "Agent E-mail Address"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileAgentPhonePlaceholder",
                        Value = "Agent Phone Number"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileAgentContactPersonPlaceholder",
                        Value = "Contact Person Name Surname"
                    },

                    new ConstantValueDto()
                    {
                        Key = "ProfileAgentCountryPlaceholder",
                        Value = "Select Country"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileAgentCityPlaceholder",
                        Value = "Select City"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileAgentTaxAdministrationPlaceholder",
                        Value = "Tax Administration"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileAgentTaxNumberPlaceholder",
                        Value = "Tax Number"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileAgentCompanyManagerPlaceholder",
                        Value = "Company Manager Name Surname"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileAgentTradeRegistryNumberPlaceholder",
                        Value = "Trade Registry Number"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileAgentWebsiteLinkLabelPlaceholder",
                        Value = "Website Link"
                    },

                    new ConstantValueDto()
                    {
                        Key = "ProfileAgentAddressPlaceholder",
                        Value = "Address"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileAgentLogoLabel",
                        Value = "Choose Logo"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ProfileAgentSaveButton",
                        Value = "Save"
                    },
                };
            var agent = _agentService.GetById(memberId);
            var getProfileAgentDto = new GetProfileAgentDto()
            {
                AgentID = agent.Id.ToString(),
                AgentTitle = agent.AgentTitle,
                Email = agent.Email,
                Phone = agent.Phone,
                ContactPerson = agent.ContactPerson,
                CountryOptions = CountryList.Countries,
                CountryID = agent.CountryID.ToString(),
                CityOptions = _systemOptionService.GetSystemOptionByCategoryId(9, Convert.ToInt32(LanguageID)),
                CityID = agent.CityID.ToString(),
                TaxAdministration = agent.TaxAdministration,
                TaxNumber = agent.TaxNumber,
                CompanyManager = agent.CompanyManager,
                TradeRegistryNumber = agent.TradeRegistryNumber,
                WebsiteLink = agent.WebsiteLink,
                Address = agent.Address,
                LogoPath = agent.LogoPath,
                ConstantValues = ConstantValues,
            };

            return CustomResponseDto<GetProfileAgentDto>.Success(200, getProfileAgentDto);
        }

        [HttpPost]
        public CustomResponseNullDto PostAgentInformation(ProfileAgentPostDto profileAgentPostDto)
        {
            //Güncelleme işlemi
            var agent = _agentService.GetById(new Guid(profileAgentPostDto.AgentID));
            agent.AgentTitle = profileAgentPostDto.AgentTitle;
            agent.Email = profileAgentPostDto.Email;
            agent.Phone = profileAgentPostDto.Phone;
            agent.CountryID = Convert.ToInt32(profileAgentPostDto.CountryID);
            agent.Address = profileAgentPostDto.Address;
            agent.CompanyManager = profileAgentPostDto.CompanyManager;
            agent.WebsiteLink = profileAgentPostDto.WebsiteLink;
            agent.TaxAdministration = profileAgentPostDto.TaxAdministration;
            agent.TaxNumber = profileAgentPostDto.TaxNumber;
            agent.ContactPerson = profileAgentPostDto.ContactPerson;
            agent.Phone = profileAgentPostDto.Phone;
            agent.LogoPath = profileAgentPostDto.LogoPath == null ? agent.LogoPath : profileAgentPostDto.LogoPath;
            agent.TradeRegistryNumber = profileAgentPostDto.TradeRegistryNumber;
            _agentService.Update(agent);
            return CustomResponseNullDto.Success(200);
        }
        [HttpGet("{userId}/{LanguageID}")]
        public CustomResponseDto<GetChangePasswordDto> GetChangePassword(string userId, string LanguageID)
        {
            var user = _userService.GetById(Guid.Parse(userId));
            var getChangePasswordDto = new GetChangePasswordDto()
            {
                ConstantValues = new List<ConstantValueDto>()
                {
                    new ConstantValueDto()
                    {
                        Key = "ChangePasswordTitle",
                        Value = "Change Password"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ChangePasswordPasswordPlaceholder",
                        Value = "Password"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ChangePasswordNewPasswordPlaceholder",
                        Value = "New Password"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ChangePasswordNewPasswordAgainPlaceholder",
                        Value = "New Password Again"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ChangePasswordChangeButton",
                        Value = "Change My Password"
                    },
                },
                UserID = user.Id.ToString()

            };

            return CustomResponseDto<GetChangePasswordDto>.Success(200, getChangePasswordDto);
        }

        [HttpPost]
        public CustomResponseNullDto PostChangePassword(ChangePasswordPostDto changePasswordPostDto)
        {
            var isSuccess = _userService.ChangePassword(changePasswordPostDto);
            //if(isSuccess)
            return CustomResponseNullDto.Success(200);
            //else
            //    return CustomResponseNullDto.Fail(400, "Wrong password");
        }
    }
}
