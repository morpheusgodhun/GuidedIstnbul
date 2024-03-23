using Core.Entities;
using Core.IService;
using Core.StaticValues;
using Dto.ApiWebDtos.ApiToWebDtos.Contact;
using Dto.ApiWebDtos.GeneralDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Mail;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebContactController : ControllerBase
    {

        private readonly IContactMessageService _contactMessageService;
        private readonly IPageService _pageService;
        private readonly IPageLanguageService _pageLanguageService;
        private readonly IConstantValueService _constantValueService;
        private readonly IConstantValueLanguageService _constantValueLanguageService;
        private readonly IContactInfoLanguageService _contactInfoLanguageService;
        private readonly IContactInfoService _contactInfoService;
        private readonly ISystemOptionLanguageService _systemOptionLanguageService;
        private readonly ISystemOptionService _systemOptionService;
        private readonly IMailSenderService _mailSenderService;
        private readonly ISendMailTemplateService _mailTemplateService;
        public WebContactController(IContactMessageService contactMessageService, IPageService pageService, IPageLanguageService pageLanguageService, IConstantValueService constantValueService, IConstantValueLanguageService constantValueLanguageService, IContactInfoLanguageService contactInfoLanguageService, IContactInfoService contactInfoService, ISystemOptionService systemOptionService, ISystemOptionLanguageService systemOptionLanguageService, IMailSenderService mailSenderService, ISendMailTemplateService mailTemplateService)
        {
            _contactMessageService = contactMessageService;
            _pageService = pageService;
            _pageLanguageService = pageLanguageService;
            _constantValueService = constantValueService;
            _constantValueLanguageService = constantValueLanguageService;
            _contactInfoLanguageService = contactInfoLanguageService;
            _contactInfoService = contactInfoService;
            _systemOptionService = systemOptionService;
            _systemOptionLanguageService = systemOptionLanguageService;
            _mailSenderService = mailSenderService;
            _mailTemplateService = mailTemplateService;
        }

        [HttpGet("{LanguageID}")]
        [ResponseCache(CacheProfileName = AppConstants.Cache120)]
        public async Task<CustomResponseDto<GetContactDto>> GetContact(int LanguageID)
        {
            var constantValues = await _constantValueService.GetConstantValueByPageNameAsync("Contact", LanguageID);
            Guid pageID = new Guid("168e00e5-a586-4486-826d-c5e47b3f375c");
            var getContactDto = new GetContactDto()
            {
                ConstantValues = constantValues,
                ContactInfos = (from info in _contactInfoService.GetAll(x => !x.IsDeleted)
                                join infoLanguageItem in _contactInfoLanguageService.GetAll(x => !x.IsDeleted)
                                on info.Id equals infoLanguageItem.ContactInfoID
                                where infoLanguageItem.LanguageID == LanguageID
                                select new ContactInfoDto()
                                {
                                    Type = info.Type,
                                    Info = infoLanguageItem.Value
                                }).ToList(),
                ContactPage = _pageLanguageService.Where(x => x.PageID == pageID && LanguageID == x.LanguageId)
                              .Select(x => new PageDto()
                              {
                                  BannerImagePath = _pageService.GetById(x.PageID).ImagePath,
                                  Title = x.Title,
                                  Subtitle = x.Subtitle,
                                  Content = x.Content
                              }).FirstOrDefault(),
                //CountryOptions = new CountryList().Countries,
                //FindUsOptions = (from systemOption in _systemOptionService.GetAll(x => !x.IsDeleted)
                //                 where systemOption.SystemOptionCategoryID == 5
                //                 join languageItem in _systemOptionLanguageService.GetAll(x => !x.IsDeleted)
                //                 on systemOption.Id equals languageItem.SystemOptionId
                //                 where languageItem.LanguageID == LanguageList.BaseLanguage.Id
                //                 select new SelectListOptionDto()
                //                 {
                //                     OptionID = systemOption.Id,
                //                     Option = languageItem.Name
                //                 }).ToList()
            };

            return CustomResponseDto<GetContactDto>.Success(200, getContactDto);
        }


        [HttpPost]
        public CustomResponseNullDto PostContact(ContactPostDto contactPostDto)
        {
            ContactMessage contactMessage = new()
            {
                CountryID = contactPostDto.CountryID,
                SendingDate = DateTime.Now,
                Message = contactPostDto.Message,
                Subject = contactPostDto.Subject,
                SenderName = contactPostDto.Name,
                Email = contactPostDto.Email,
                SystemOptionID = contactPostDto.FindUsID,
            };
            _contactMessageService.Add(contactMessage);
            _contactMessageService.SendContactMails(contactPostDto);

            return CustomResponseNullDto.Success(200);

        }
    }
}
