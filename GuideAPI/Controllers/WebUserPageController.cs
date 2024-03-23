using Core;
using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiPanelDtos.AgentDtos;
using Dto.ApiPanelDtos.ProductDtos.GeneralDtos;
using Dto.ApiWebDtos.ApiToWebDtos.GeneralPages;
using Dto.ApiWebDtos.ApiToWebDtos.Reservation;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.GeneralDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using Dto.ApiWebDtos.WebToApiDtos.Reservation;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebUserPageController : ControllerBase
    {
        private readonly IConstantValueLanguageService _constantValueLanguageService;
        private readonly IConstantValueService _constantValueService;
        private readonly IPageService _pageService;
        private readonly IReservationService _reservationService;
        private readonly ICustomTourRequestService _customTourRequestService;
        private readonly ISystemOptionService _systemOptionService;
        private readonly IAgentService _agentService;
        private readonly IGuideService _guideService;
        private readonly IGuideCityService _guideCityService;
        private readonly IGuideLanguageService _guideLanguageService;
        private readonly IMany_Agent_ProductService _many_agent_productService;
        private readonly IUserService _userService;
        private readonly IUserExtensionMailService _userExtensionMailService;
        private readonly IFaqCategoryService _faqCategoryService;
        public WebUserPageController(IConstantValueLanguageService constantValueLanguageService, IConstantValueService constantValueService, IPageService pageService, IReservationService reservationService, ICustomTourRequestService customTourRequestService, ISystemOptionService systemOptionService, IGuideService guideService, IGuideCityService guideCityService, IGuideLanguageService guideLanguageService, IAgentService agentService, IMany_Agent_ProductService many_agent_productService, IUserService userService, IUserExtensionMailService extensionMailService, IFaqCategoryService faqCategoryService)
        {
            _constantValueLanguageService = constantValueLanguageService;
            _constantValueService = constantValueService;
            _pageService = pageService;
            _reservationService = reservationService;
            _customTourRequestService = customTourRequestService;
            _systemOptionService = systemOptionService;
            _agentService = agentService;
            _guideService = guideService;
            _guideCityService = guideCityService;
            _guideLanguageService = guideLanguageService;
            _many_agent_productService = many_agent_productService;
            _userService = userService;
            _userExtensionMailService = extensionMailService;
            _faqCategoryService = faqCategoryService;
        }

        [HttpGet("{LanguageID}")] //Done
        [ResponseCache(CacheProfileName = AppConstants.Cache120)]

        public async Task<CustomResponseDto<GetLoginDto>> GetLogin(int LanguageID)
        {
            GetLoginDto getLoginDto = new GetLoginDto()
            {
                ConstantValues = await _constantValueService.GetConstantValueByPageNameAsync("Login", LanguageID),
            };

            return CustomResponseDto<GetLoginDto>.Success(200, getLoginDto);
        }

        [HttpPost]
        public CustomResponseNullDto PostLogin(LoginPostDto loginPostDto)
        {

            return CustomResponseNullDto.Success(200);
        }

        [HttpGet("{LanguageID}")] //Done
        [ResponseCache(CacheProfileName = AppConstants.Cache120)]

        public async Task<CustomResponseDto<GetRegisterDto>> GetRegister(int LanguageID)
        {
            var getRegisterDto = new GetRegisterDto()
            {
                ConstantValues = await _constantValueService.GetConstantValueByPageNameAsync("Register", LanguageID),
            };

            return CustomResponseDto<GetRegisterDto>.Success(200, getRegisterDto);
        }

        [HttpPost]
        public CustomResponseNullDto PostRegister(RegisterPostDto registerPostDto)
        {
            return CustomResponseNullDto.Success(200);
        }



        [HttpGet("{LanguageID}")] //Done
        [ResponseCache(CacheProfileName = AppConstants.Cache120)]

        public CustomResponseDto<GetForgotPasswordDto> GetForgotPassword(string LanguageID)
        {
            var getForgotPasswordDto = new GetForgotPasswordDto()
            {
                ConstantValues = new List<ConstantValueDto>()
                {
                    new ConstantValueDto()
                    {
                        Key = "ForgotPasswordTitle",
                        Value = "Forgot Password"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ForgotPasswordEmailPlaceholder",
                        Value = "E-mail"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ForgotPasswordButton",
                        Value = "Send Code To My E-Mail"
                    },

                }
            };

            return CustomResponseDto<GetForgotPasswordDto>.Success(200, getForgotPasswordDto);
        }

        [HttpPost] //Done
        public CustomResponseNullDto PostForgotPassword(ForgotPasswordPostDto forgotPasswordPostDto)
        {
            if (!_userService.IsEmailExist(forgotPasswordPostDto.Email))
            {
                return CustomResponseNullDto.Fail(404, "User not found!");
            }
            _userService.SendForgotPasswordMail(forgotPasswordPostDto);
            return CustomResponseNullDto.Success(200);
        }

        [HttpGet("{uniqueCode}")] //Done
        public CustomResponseNullDto GetResetPasswordValidation(string uniqueCode)
        {
            if (!_userExtensionMailService.IsUrlCodeValid(uniqueCode, SendMailTemplateName.No.SifremiUnuttum))
                return CustomResponseNullDto.Fail(404, "Invalid URL");

            return CustomResponseNullDto.Success(200);
        }
        [HttpPost]
        public CustomResponseNullDto PostResetPasswordValidation(ResetPasswordValidationPostDto resetPasswordValidationPostDto)
        {
            var response = _userService.ForgotPasswordCheckValidation(resetPasswordValidationPostDto);
            return response;
        }

        [HttpGet("{LanguageID}/{urlCode}")] //Done
        public CustomResponseDto<GetResetPasswordDto> GetResetPassword(string LanguageID, string urlCode)
        {
            var userId = _userExtensionMailService.GetByUrlCode(urlCode).UserId;
            var getResetPasswordDto = new GetResetPasswordDto()
            {
                ConstantValues = new List<ConstantValueDto>()
                {
                    new ConstantValueDto()
                    {
                        Key = "ResetPasswordTitle",
                        Value = "Reset Password"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ResetPasswordCodePlaceholder",
                        Value = "Code"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ResetPasswordPasswordPlaceholder",
                        Value = "Password"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ResetPasswordPasswordAgainPlaceholder",
                        Value = "Password Again"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ResetPasswordButton",
                        Value = "Save"
                    },
                },
                UserId = userId,
                UrlCode = urlCode
            };

            return CustomResponseDto<GetResetPasswordDto>.Success(200, getResetPasswordDto);
        }

        [HttpPost]
        public CustomResponseNullDto PostResetPassword(ResetPasswordPostDto resetPasswordDto)
        {
            if (!_userExtensionMailService.IsUrlCodeValid(resetPasswordDto.UrlCode, SendMailTemplateName.No.SifremiUnuttum))
                return CustomResponseNullDto.Fail(404, "Invalid URL");

            _userService.ResetPassword(resetPasswordDto);
            return CustomResponseNullDto.Success(200);
        }



        [HttpGet("{agentId}")]
        public CustomResponseDto<List<Many_Agent_ProductDto>> GetAdvanceDiscountInfos(string agentId)
        {
            ////Service'e istek at
            var agentProducts = _many_agent_productService.Where(x => x.Status && !x.IsDeleted && x.AgentId == new Guid(agentId)).Select(x => new Many_Agent_ProductDto { AgentId = x.AgentId.ToString(), DiscountRate = x.DiscountRate, ProductId = x.ProductId.ToString() }).ToList();
            return CustomResponseDto<List<Many_Agent_ProductDto>>.Success(200, agentProducts);
        }
        [HttpPost]
        public CustomResponseNullDto PostAdvanceDiscountInfos(List<Many_Agent_ProductDto> agentProducts)
        {
            ////Service'e istek at
            foreach (var agentProduct in agentProducts)
            {
                var selectedAgentProduct = _many_agent_productService.Where(x => x.AgentId == new Guid(agentProduct.AgentId) && x.ProductId == new Guid(agentProduct.ProductId)).FirstOrDefault();
                if (selectedAgentProduct is null)
                {
                    if (agentProduct.DiscountRate != -1)
                    {
                        Many_Agent_Product many_Agent_Product = new Many_Agent_Product
                        {
                            AgentId = new Guid(agentProduct.AgentId),
                            DiscountRate = agentProduct.DiscountRate,
                            ProductId = new Guid(agentProduct.ProductId),
                            CreateDate = DateTime.Now,
                            IsDeleted = false,
                            Status = true,
                        };
                        _many_agent_productService.Add(many_Agent_Product);
                    }
                }
                else
                {
                    if (agentProduct.DiscountRate != -1)
                    {
                        selectedAgentProduct.DiscountRate = agentProduct.DiscountRate;
                        selectedAgentProduct.UpdateDate = DateTime.Now;
                        _many_agent_productService.Update(selectedAgentProduct);
                    }
                    else
                    {
                        selectedAgentProduct.IsDeleted = true;
                        selectedAgentProduct.UpdateDate = DateTime.Now;
                        _many_agent_productService.Update(selectedAgentProduct);
                    }

                }

            }
            return CustomResponseNullDto.Success(200);
        }

        [HttpGet("{LanguageID}")] //Done

        public CustomResponseDto<List<AgentDto>> GetAgents(int LanguageID)
        {

            var entities = _agentService.GetAll();
            List<AgentDto> getApplyAgentDto = entities.Select(x => new AgentDto
            {
                Id = x.Id,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate,
                Status = x.Status,
                IsDeleted = x.IsDeleted,
                AgentTitle = x.AgentTitle,
                Email = x.Email,
                Phone = x.Phone,
                ContactPerson = x.ContactPerson,
                CountryID = x.CountryID,
                //CityID = x.CityID,
                //CountryCity = ((new CountryList().Countries.First(y => x.Id.Equals(x.CountryID)).Value) + (_systemOptionService.GetCityById(9, LanguageID, x.CityID).Option)),
                TaxAdministration = x.TaxAdministration,
                TaxNumber = x.TaxNumber,
                CompanyManager = x.CompanyManager,
                TradeRegistryNumber = x.TradeRegistryNumber,
                WebsiteLink = x.WebsiteLink,
                Address = x.Address,
                LogoPath = x.LogoPath,
                ApproveStatus = x.ApproveStatus,
                ServicesDiscountRate = x.ServicesDiscountRate,
                DefaultTourDiscount = x.DefaultTourDiscount,
                WithoutPay = x.WithoutPay
            }).ToList();

            return CustomResponseDto<List<AgentDto>>.Success(200, getApplyAgentDto);
        }

        [HttpGet("{LanguageID}")] //Done
        public async Task<CustomResponseDto<GetApplyAgentDto>> GetApplyAgent(int languageId)
        {
            GetApplyAgentDto? getApplyAgentDto = new()
            {
                ConstantValues = await _constantValueService.GetConstantValueByPageNameAsync("Apply As Agent", languageId),
                CountryOptions = CountryList.Countries,
                CityOptions = _systemOptionService.GetSystemOptionByCategoryId(9, languageId),
                ApplyAgentFaqSlug = _faqCategoryService.GetPageFaqCategorySlug("0915B7C1-9ED3-46C9-8A6F-E08AD2FD8E4C", languageId) //TODO elle verilen guidleri kaldır
            };

            return CustomResponseDto<GetApplyAgentDto>.Success(200, getApplyAgentDto);
        }
        [HttpPost]
        public CustomResponseNullDto PostApplyAgent(ApplyAgentPostDto applyAgentPostDto)
        {
            _agentService.AddAgent(applyAgentPostDto);
            return CustomResponseNullDto.Success(200);
        }
        [HttpGet]
        public CustomResponseDto<List<ProductNameAndIdDto>> GetProductAdvanceDiscount()
        {
            var response = _agentService.GetProductAdvanceDiscount();
            return response;
        }
        [HttpPost]
        public CustomResponseNullDto ApproveAgent(AgentApprove model)
        {
            _agentService.ApproveAgent(model);
            return CustomResponseNullDto.Success(200);
        }
        [HttpPost]
        public CustomResponseNullDto RejectAgent(AgentApprove model)
        {

            _agentService.RejectAgent(model);
            return CustomResponseNullDto.Success(200);
        }
        [HttpPost]
        public CustomResponseNullDto ChangeStatus(AgentApprove model)
        {

            var entity = _agentService.GetById(model.AgentId);
            entity.Status = !entity.Status;
            _agentService.Update(entity);
            return CustomResponseNullDto.Success(200);
        }
        [HttpGet("{LanguageID}")] //Done
        public async Task<CustomResponseDto<GetApplyGuideDto>> GetApplyGuide(int languageID)
        {
            var getApplyGuideDto = new GetApplyGuideDto()
            {
                ConstantValues = await _constantValueService.GetConstantValueByPageNameAsync("Apply As Guide", languageID),
                LanguageKnowOptions = _systemOptionService.GetSystemOptionByCategoryId((int)SystemOptionCategoryList.SystemOperations.TourLanguages, languageID),
                SpecializeCityOptions = _systemOptionService.GetSystemOptionByCategoryId((int)SystemOptionCategoryList.SystemOperations.TourCities, languageID),
                ApplyGuideFaqSlug = _faqCategoryService.GetPageFaqCategorySlug("C007C2FE-A39C-421B-B80E-132555308DC0", languageID)
                //LanguageKnowOptions = new List<SelectListOptionDto>()
                //{
                //    new SelectListOptionDto()
                //    {
                //        OptionID = "1",
                //        Option = "English"
                //    },
                //    new SelectListOptionDto()
                //    {
                //        OptionID = "2",
                //        Option = "Turkish"
                //    },
                //    new SelectListOptionDto()
                //    {
                //        OptionID = "3",
                //        Option = "Spanish"
                //    },
                //    new SelectListOptionDto()
                //    {
                //        OptionID = "4",
                //        Option = "German "
                //    },
                //    new SelectListOptionDto()
                //    {
                //        OptionID = "5",
                //        Option = "Italian"
                //    },
                //},
                //SpecializeCityOptions = new List<SelectListOptionDto>()
                //{
                //    //new SelectListOptionDto()
                //    //{
                //    //    OptionID = "1",
                //    //    Option = "Istanbul"
                //    //},
                //    //new SelectListOptionDto()
                //    //{
                //    //    OptionID = "2",
                //    //    Option = "Nevşehir"
                //    //},
                //    //new SelectListOptionDto()
                //    //{
                //    //    OptionID = "3",
                //    //    Option = "Trabzon"
                //    //},
                //    //new SelectListOptionDto()
                //    //{
                //    //    OptionID = "4",
                //    //    Option = "Muğla "
                //    //},
                //    //new SelectListOptionDto()
                //    //{
                //    //    OptionID = "5",
                //    //    Option = "Hatay"
                //    //},
                //},
            };

            return CustomResponseDto<GetApplyGuideDto>.Success(200, getApplyGuideDto);
        }

        [HttpPost]
        public CustomResponseNullDto PostApplyGuide(ApplyGuidePostDto applyGuidePostDto)
        {
            Guide guide = new Guide
            {
                Name = applyGuidePostDto.Name,
                Surname = applyGuidePostDto.Surname,
                Email = applyGuidePostDto.Email,
                BirthDate = applyGuidePostDto.BirthDate,
                LicenseBackImagePath = applyGuidePostDto.LicenseBackImagePath,
                TurebLicenseNumber = applyGuidePostDto.TurebLicenseNumber,
                LicenseFrontImagePath = applyGuidePostDto.LicenseFrontImagePath,
                Tc = applyGuidePostDto.Tc,
                ApproveConstent = applyGuidePostDto.ApproveConstent,
                Phone = applyGuidePostDto.Phone,
                ProfilPhotoPath = applyGuidePostDto.ProfilPhotoPath,
                RegisteredDirectoryRoom = applyGuidePostDto.RegisteredDirectoryRoom,
                Status = false,
                IsDeleted = false,
                CreateDate = DateTime.Now,
                ApproveStatus = (int)Enums.ApproveStatus.OnayBekliyor,
            };
            var addedGuide = _guideService.Add(guide); ////// @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            foreach (var item in applyGuidePostDto.SpecializeCityIDs)
            {
                _guideCityService.Add(new GuideCity { GuideId = addedGuide.Id, LanguageId = item });
            }
            foreach (var item in applyGuidePostDto.LanguageKnowIDs)
            {
                var test = _guideLanguageService.Add(new GuideLanguage { GuideId = addedGuide.Id, LanguageId = item });
            }
            return CustomResponseNullDto.Success(200);
        }

        [HttpGet("{LanguageID}")] //Done
        public CustomResponseDto<GetReservationInquiryDto> GetReservationInquery(string LanguageID)
        {
            var getReservationInquiryDto = new GetReservationInquiryDto()
            {
                ConstantValues = new List<ConstantValueDto>()
                {
                    new ConstantValueDto()
                    {
                        Key = "ReservationInquiry.ReservationCodePlaceholder",
                        Value = "Your Reservation Code"
                    },
                    new ConstantValueDto()
                    {
                        Key = "ReservationInquiry.SearchButton",
                        Value = "Search"
                    },

                },

            };

            return CustomResponseDto<GetReservationInquiryDto>.Success(200, getReservationInquiryDto);
        }

        [HttpPost]
        public CustomResponseDto<WebReservationListDto> PostReservationInquery(ReservationInquiryPostDto reservationInquiryPostDto)
        {
            WebReservationListDto dto = _reservationService.ReservationInquiry(reservationInquiryPostDto.ReservationCode, reservationInquiryPostDto.LanguageId);
            return CustomResponseDto<WebReservationListDto>.Success(200, dto);
        }


        [HttpGet("{requestId}/{languageId}")]
        public CustomResponseDto<GetCustomTourRequestDetailDto> CustomTourRequestDetail(Guid requestId, int languageId)
        {

            GetCustomTourRequestDetailDto dto = _customTourRequestService.CustomTourRequestDetail(requestId, languageId);

            /*
            var getApplyAgentDto = new GetCustomTourRequestDetailDto()
            {
                //ConstantValues = null,

                CustomTourRequestDetail = new CountryList().Countries,

                OfferList = new CityList().Cities,

            };
            */
            return CustomResponseDto<GetCustomTourRequestDetailDto>.Success(200, dto);
        }

    }
}
