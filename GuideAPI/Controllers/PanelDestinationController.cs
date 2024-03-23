using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Dto.ApiPanelDtos.DestinationDtos;
using Dto.ApiPanelDtos.FaqCategoryDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System.Collections.Generic;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelDestinationController : ControllerBase
    {
        private readonly IDestinationService _destinationService;
        private readonly IDestinationLanguageService _destinationLanguageService;

        public PanelDestinationController(IDestinationService destinationService, IDestinationLanguageService destinationLanguageService)
        {
            _destinationService = destinationService;
            _destinationLanguageService = destinationLanguageService;
        }

        [HttpGet]
        public CustomResponseDto<List<DestinationListDto>> DestinationList()
        {
            List<DestinationListDto> destinationListDtos =
                (from destination in _destinationService.GetAll(x => !x.IsDeleted)
                 join destinationLanguageItem in _destinationLanguageService.GetAll(x => !x.IsDeleted)
                 on destination.Id equals destinationLanguageItem.DestinationID
                 where destinationLanguageItem.LangaugeID == LanguageList.BaseLanguage.Id
                 select new DestinationListDto
                 {
                     DestinationID = destination.Id,
                     DestinationName = destinationLanguageItem.DisplayName,
                     Order = destination.Order,
                     Status = destination.Status,
                     ShowOnCustomMade = destination.ShowOnCustomMade
                 }).ToList();


            return CustomResponseDto<List<DestinationListDto>>.Success(200, destinationListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddDestination(AddDestinationDto addDestinationDto)
        {
            Destination destination = new Destination()
            {
                Order = addDestinationDto.Order,
                ShowOnCustomMade = false
            };

            _destinationService.Add(destination);


            foreach (var language in LanguageList.AllLanguages)
            {
                DestinationLanguageItem destinationLanguageItem = new DestinationLanguageItem()
                {
                    DestinationID = destination.Id,
                    LangaugeID = language.Id,
                    DisplayName = addDestinationDto.DestinationName,
                };

                _destinationLanguageService.Add(destinationLanguageItem);
            }

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditDestinationDto> EditDestination(Guid id)
        {

            Destination destination = _destinationService.GetById(id);
            var destinationName = _destinationLanguageService.Where(x => x.DestinationID == id && x.LangaugeID == 1).FirstOrDefault().DisplayName;
            EditDestinationDto editDestinationDto = new EditDestinationDto()
            {
                DestinationID = destination.Id,
                Order = destination.Order,
                DestinationName = destinationName,
            };

            return CustomResponseDto<EditDestinationDto>.Success(200, editDestinationDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditDestination(EditDestinationDto editDestinationDto)
        {
            Destination destination = _destinationService.GetById(editDestinationDto.DestinationID);
            destination.Order = editDestinationDto.Order;
            _destinationService.Update(destination);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditDestinationDto> LanguageEditDestination(Guid id, int languageId)
        {

            DestinationLanguageItem destinationLanguageItem = _destinationLanguageService.Where(x => x.DestinationID == id && x.LangaugeID == languageId).FirstOrDefault();

            LanguageEditDestinationDto languageEditDestinationDto = new LanguageEditDestinationDto()
            {
                DestinationLanguageItemID = destinationLanguageItem.Id,
                DestinationName = destinationLanguageItem.DisplayName,
                LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name
            };


            return CustomResponseDto<LanguageEditDestinationDto>.Success(200, languageEditDestinationDto);
        }

        [HttpPost]
        public CustomResponseNullDto LanguageEditDestination(LanguageEditDestinationDto languageEditDestinationDto)
        {
            DestinationLanguageItem destinationLanguageItem = _destinationLanguageService.GetById(languageEditDestinationDto.DestinationLanguageItemID);

            destinationLanguageItem.DisplayName = languageEditDestinationDto.DestinationName;
            _destinationLanguageService.Update(destinationLanguageItem);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleDestinationStatus(Guid id)
        {
            _destinationService.ToggleStatus(id);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleShowOnCustomMade(Guid id)
        {
            Destination destination = _destinationService.GetById(id);
            destination.ShowOnCustomMade = !destination.ShowOnCustomMade;
            _destinationService.Update(destination);

            return CustomResponseNullDto.Success(204);
        }

        //Destination Select List
        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> DestinationSelectList()
        {
            List<SelectListOptionDto> destinationSelectList =
                (from destination in _destinationService.GetAll(x => !x.IsDeleted)
                 join destinationLanguageItem in _destinationLanguageService.GetAll(x => !x.IsDeleted)
                 on destination.Id equals destinationLanguageItem.DestinationID
                 where destinationLanguageItem.LangaugeID == LanguageList.BaseLanguage.Id
                 select new SelectListOptionDto
                 {
                     OptionID = destination.Id,
                     Option = destinationLanguageItem.DisplayName

                 }).ToList();


            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, destinationSelectList);
        }
    }
}
