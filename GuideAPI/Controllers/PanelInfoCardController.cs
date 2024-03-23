using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiPanelDtos.InfoCardDtos;

using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System.Text.Json;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelInfoCardController : ControllerBase
    {
        private readonly IInfoCardService _infoCardService;
        private readonly IInfoCardLanguageService _infoCardLanguageService;

        public PanelInfoCardController(IInfoCardService infoCardService, IInfoCardLanguageService infoCardLanguageService)
        {
            _infoCardService = infoCardService;
            _infoCardLanguageService = infoCardLanguageService;
        }

        [HttpGet]
        public CustomResponseDto<List<InfoCardListDto>> InfoCardList()
        {

            List<InfoCardListDto> infoCardListDto =
                (from card in _infoCardService.GetAll(x => !x.IsDeleted)
                 join languageItem in _infoCardLanguageService.GetAll(x => !x.IsDeleted)
                 on card.Id equals languageItem.InfoCardID
                 where languageItem.LanguageID == LanguageList.BaseLanguage.Id
                 select new InfoCardListDto
                 {
                     InfoCardId = card.Id,
                     IconPath = card.IconPath,
                     Status = card.Status,
                     Title = languageItem.Title
                 }).ToList();


            return CustomResponseDto<List<InfoCardListDto>>.Success(200, infoCardListDto);
        }

        [HttpPost]
        public CustomResponseNullDto AddInfoCard(AddInfoCardDto addInfoCardDto)
        {


            InfoCard infoCard = new InfoCard()
            {
                IconPath = addInfoCardDto.IconPath,
            };
            _infoCardService.Add(infoCard);

            foreach (var language in LanguageList.AllLanguages)
            {
                InfoCardLanguageItem infoCardLanguageItem = new InfoCardLanguageItem()
                {
                    InfoCardID = infoCard.Id,
                    Title = addInfoCardDto.Title,
                    Content = addInfoCardDto.Content,
                    LanguageID = language.Id
                };

                _infoCardLanguageService.Add(infoCardLanguageItem);
            }
            return CustomResponseNullDto.Success(200);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditInfoCardDto> EditInfoCard(Guid id)
        {
            InfoCard infoCard = _infoCardService.GetById(id);
            string infoTitle = _infoCardLanguageService.Where(x => x.InfoCardID == id && x.LanguageID == 1).FirstOrDefault().Title;
            EditInfoCardDto editInfoCardDto = new EditInfoCardDto()
            {
                InfoCardID = infoCard.Id,
                IconPath = infoCard.IconPath,
                Title = infoTitle,
            };
            return CustomResponseDto<EditInfoCardDto>.Success(200, editInfoCardDto);
        }
        [HttpGet("{id}")]
        public CustomResponseDto<string> GetInfoCardTitle(Guid id)
        {
            return CustomResponseDto<string>.Success(200, "");
        }

        [HttpPost]
        public CustomResponseNullDto EditInfoCard(EditInfoCardDto editInfoCardDto)
        {
            InfoCard infoCard = _infoCardService.GetById(editInfoCardDto.InfoCardID);

            infoCard.IconPath = editInfoCardDto.IconPath;
            _infoCardService.Update(infoCard);

            return CustomResponseNullDto.Success(204);
        }


        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditInfoCardDto> LanguageEditInfoCard(Guid id, int languageId)
        {

            InfoCardLanguageItem infoCardLanguageItem = _infoCardLanguageService.Where(x => x.LanguageID == languageId && x.InfoCardID == id).FirstOrDefault();

            LanguageEditInfoCardDto languageEditInfoCardDto = new LanguageEditInfoCardDto()
            {
                LanguageInfoCardID = infoCardLanguageItem.Id,
                Title = infoCardLanguageItem.Title,
                Content = infoCardLanguageItem.Content,
                LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name
            };

            return CustomResponseDto<LanguageEditInfoCardDto>.Success(200, languageEditInfoCardDto);
        }

        [HttpPost]
        public CustomResponseNullDto LanguageEditInfoCard(LanguageEditInfoCardDto languageEditInfoCardDto)
        {
            InfoCardLanguageItem infoCardLanguageItem = _infoCardLanguageService.GetById(languageEditInfoCardDto.LanguageInfoCardID);

            infoCardLanguageItem.Title = languageEditInfoCardDto.Title;
            infoCardLanguageItem.Content = languageEditInfoCardDto.Content;

            _infoCardLanguageService.Update(infoCardLanguageItem);


            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleInfoCardStatus(Guid id)
        {
            _infoCardService.ToggleStatus(id);


            return CustomResponseNullDto.Success(204);
        }
    }
}
