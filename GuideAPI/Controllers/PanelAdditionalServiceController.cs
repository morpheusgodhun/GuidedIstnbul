using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Dto.ApiPanelDtos.AdditionalServiceDtos;
using Dto.ApiPanelDtos.CancellationPolicyDtos;
using Dto.ApiPanelDtos.FaqCategoryDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service.Services;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelAdditionalServiceController : ControllerBase
    {
        private readonly IAdditionalServiceService _additionalServiceService;
        private readonly IAdditionalServiceLanguageService _additionalServiceLanguageService;
        private readonly IAdditionalServiceInputService _additionalInputService;
        private readonly IAdditionalServiceInputLanguageService _additionalInputLanguageService;
        private readonly IAdditionalServiceOptionService _additionalOptionService;
        private readonly IAdditionalServiceOptionLanguageService _additionalOptionLanguageService;
        private readonly IAdditionalServiceOptionPriceDateService _additionalOptionPriceDateService;
        private readonly IAdditionalServiceOptionPriceService _additionalOptionPriceService;
        private readonly IMany_AdditionalServiceOption_AdditionalServiceInputService _manyService;

        public PanelAdditionalServiceController(IAdditionalServiceService additionalServiceService, IAdditionalServiceLanguageService additionalServiceLanguageService, IAdditionalServiceInputService additionalInputService, IAdditionalServiceInputLanguageService additionalInputLanguageService, IAdditionalServiceOptionService additionalOptionService, IAdditionalServiceOptionLanguageService additionalOptionLanguageService, IAdditionalServiceOptionPriceDateService additionalOptionPriceDateService, IAdditionalServiceOptionPriceService additionalOptionPriceService, IMany_AdditionalServiceOption_AdditionalServiceInputService manyService)
        {
            _additionalServiceService = additionalServiceService;
            _additionalServiceLanguageService = additionalServiceLanguageService;
            _additionalInputService = additionalInputService;
            _additionalInputLanguageService = additionalInputLanguageService;
            _additionalOptionService = additionalOptionService;
            _additionalOptionLanguageService = additionalOptionLanguageService;
            _additionalOptionPriceDateService = additionalOptionPriceDateService;
            _additionalOptionPriceService = additionalOptionPriceService;
            _manyService = manyService;
        }

        [HttpGet]
        public CustomResponseDto<List<AdditionalServiceListDto>> AdditionalServiceList()
        {

            var additionalServiceListDtos = (from additionalService in _additionalServiceService.GetAll(x => !x.IsDeleted)
                                             select new AdditionalServiceListDto
                                             {
                                                 AdditionalServiceID = additionalService.Id,
                                                 Name = additionalService.Name,
                                                 Status = additionalService.Status,
                                                 IsPerDay = additionalService.IsPerDay,
                                                 IsPerPerson = additionalService.IsPerPerson,
                                                 IsSpecialNumber = additionalService.IsSpecialNumber,
                                             }).ToList();


            return CustomResponseDto<List<AdditionalServiceListDto>>.Success(200, additionalServiceListDtos);

        }

        [HttpPost]
        public CustomResponseNullDto AddAdditionalService(AddAdditionalServiceDto addAdditionalServiceDto)
        {
            AdditionalService additionalService = new AdditionalService()
            {
                Name = addAdditionalServiceDto.Name,
                IsPerDay = addAdditionalServiceDto.IsPerDay,
                IsPerPerson = addAdditionalServiceDto.IsPerPerson,
                IsSpecialNumber = addAdditionalServiceDto.IsSpecialNumber,
            };
            _additionalServiceService.Add(additionalService);

            foreach (var language in LanguageList.AllLanguages)
            {
                AdditionalServiceLanguageItem additionalServiceLanguageItem = new AdditionalServiceLanguageItem()
                {
                    AdditionalServiceID = additionalService.Id,
                    LanguageID = language.Id,
                    DisplayName = addAdditionalServiceDto.DisplayName
                };

                _additionalServiceLanguageService.Add(additionalServiceLanguageItem);
            }

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditAdditionalServiceDto> EditAdditionalService(Guid id)
        {

            AdditionalService additionalService = _additionalServiceService.GetById(id);

            EditAdditionalServiceDto editAdditionalServiceDto = new EditAdditionalServiceDto()
            {
                AdditionalServiceID = additionalService.Id,
                Name = additionalService.Name,
                IsPerDay = additionalService.IsPerDay,
                IsPerPerson = additionalService.IsPerPerson,
                IsSpecialNumber = additionalService.IsSpecialNumber
            };

            return CustomResponseDto<EditAdditionalServiceDto>.Success(200, editAdditionalServiceDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditAdditionalService(EditAdditionalServiceDto editAdditionalServiceDto)
        {
            AdditionalService additionalService = _additionalServiceService.GetById(editAdditionalServiceDto.AdditionalServiceID);

            additionalService.IsPerDay = editAdditionalServiceDto.IsPerDay;
            additionalService.IsPerPerson = editAdditionalServiceDto.IsPerPerson;
            additionalService.IsSpecialNumber = editAdditionalServiceDto.IsSpecialNumber;

            _additionalServiceService.Update(additionalService);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditAdditionalServiceDto> LanguageEditAdditionalService(Guid id, int languageId)
        {


            LanguageEditAdditionalServiceDto languageEditAdditionalServiceDto =
                (from additionalService in _additionalServiceService.GetAll(x => !x.IsDeleted)
                 where additionalService.Id == id
                 join additionalServiceLanguageItem in _additionalServiceLanguageService.GetAll(x => !x.IsDeleted)
                 on additionalService.Id equals additionalServiceLanguageItem.AdditionalServiceID
                 where additionalServiceLanguageItem.LanguageID == languageId
                 select new LanguageEditAdditionalServiceDto
                 {
                     AdditionalServiceLanguageItemID = additionalServiceLanguageItem.Id,
                     Name = additionalService.Name,
                     DisplayName = additionalServiceLanguageItem.DisplayName,
                     LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name

                 }).FirstOrDefault();



            return CustomResponseDto<LanguageEditAdditionalServiceDto>.Success(200, languageEditAdditionalServiceDto);
        }

        [HttpPost]
        public CustomResponseNullDto LanguageEditAdditionalService(LanguageEditAdditionalServiceDto languageEditAdditionalServiceDto)
        {
            AdditionalServiceLanguageItem additionalServiceLanguageItem = _additionalServiceLanguageService.GetById(languageEditAdditionalServiceDto.AdditionalServiceLanguageItemID);

            additionalServiceLanguageItem.DisplayName = languageEditAdditionalServiceDto.DisplayName;

            _additionalServiceLanguageService.Update(additionalServiceLanguageItem);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleAdditionalServiceStatus(Guid id)
        {
            _additionalServiceService.ToggleStatus(id);

            return CustomResponseNullDto.Success(204);
        }


        //Form Input

        [HttpGet("{additionalServiceId}")]
        public CustomResponseDto<List<AdditionalServiceFormInputListDto>> FormInputList(Guid additionalServiceId)
        {
            var additionalServiceName = _additionalServiceLanguageService.Where(x => x.AdditionalServiceID == additionalServiceId && x.LanguageID == 1).FirstOrDefault().DisplayName;

            List<AdditionalServiceFormInputListDto> additionalServiceFormInputListDtos =
                (from additionalServiceInput in _additionalInputService.GetAll(x => !x.IsDeleted)
                 where additionalServiceInput.AdditionalServiceID == additionalServiceId
                 join additionalServiceInputLanguageItem in _additionalInputLanguageService.GetAll(x => !x.IsDeleted)
                 on additionalServiceInput.Id equals additionalServiceInputLanguageItem.AdditionalServiceInputID
                 where additionalServiceInputLanguageItem.LanguageID == LanguageList.BaseLanguage.Id
                 select new AdditionalServiceFormInputListDto
                 {
                     AdditionalServiceID = additionalServiceId,
                     AdditionalServiceName = additionalServiceName,
                     FormInputID = additionalServiceInput.Id,
                     Status = additionalServiceInput.Status,
                     Order = additionalServiceInput.Order,
                     IsRequired = additionalServiceInput.IsRequired,
                     Type = InputTypeList.Types.FirstOrDefault(x => x.ID == additionalServiceInput.InputTypeID).Value,
                     PropertyName = additionalServiceInputLanguageItem.DisplayName

                 }).ToList();



            return CustomResponseDto<List<AdditionalServiceFormInputListDto>>.Success(200, additionalServiceFormInputListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddAdditionalServiceFormInput(AddAdditionalServiceFormInputDto addAdditionalServiceFormInputDto)
        {

            AdditionalServiceInput additionalServiceInput = new AdditionalServiceInput()
            {
                AdditionalServiceID = addAdditionalServiceFormInputDto.AdditionalServiceID,
                InputTypeID = addAdditionalServiceFormInputDto.Type,
                IsRequired = addAdditionalServiceFormInputDto.IsRequired,
                Order = addAdditionalServiceFormInputDto.Order
            };

            _additionalInputService.Update(additionalServiceInput);

            foreach (var language in LanguageList.AllLanguages)
            {
                AdditionalServiceInputLanguageItem additionalServiceInputLanguageItem = new AdditionalServiceInputLanguageItem()
                {
                    AdditionalServiceInputID = additionalServiceInput.Id,
                    LanguageID = language.Id,
                    DisplayName = addAdditionalServiceFormInputDto.PropertyName
                };

                _additionalInputLanguageService.Add(additionalServiceInputLanguageItem);
            }


            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditAdditionalServiceFormInputDto> EditAdditionalServiceFormInput(Guid id)
        {

            EditAdditionalServiceFormInputDto editAdditionalServiceFormInputDto =
           (from additionalServiceInput in _additionalInputService.GetAll(x => !x.IsDeleted)
            where additionalServiceInput.Id == id
            join additionalServiceInputLanguageItem in _additionalInputLanguageService.GetAll(x => !x.IsDeleted)
            on additionalServiceInput.Id equals additionalServiceInputLanguageItem.AdditionalServiceInputID
            where additionalServiceInputLanguageItem.LanguageID == LanguageList.BaseLanguage.Id
            join additionalService in _additionalServiceService.GetAll(x => !x.IsDeleted)
            on additionalServiceInput.AdditionalServiceID equals additionalService.Id
            select new EditAdditionalServiceFormInputDto
            {
                AdditionalServiceName = additionalService.Name,
                AdditionalServiceId = additionalService.Id.ToString(),
                FormInputID = id,
                IsRequired = additionalServiceInput.IsRequired,
                Order = additionalServiceInput.Order,
                Type = additionalServiceInput.InputTypeID,
                PropertyName = additionalServiceInputLanguageItem.DisplayName,
            }).FirstOrDefault();




            return CustomResponseDto<EditAdditionalServiceFormInputDto>.Success(200, editAdditionalServiceFormInputDto);
        }

        [HttpPost]
        public CustomResponseDto<Guid> EditAdditionalServiceFormInput(EditAdditionalServiceFormInputDto editAdditionalServiceFormInputDto)
        {

            AdditionalServiceInput additionalServiceInput = _additionalInputService.GetById(editAdditionalServiceFormInputDto.FormInputID);

            additionalServiceInput.InputTypeID = editAdditionalServiceFormInputDto.Type;
            additionalServiceInput.Order = editAdditionalServiceFormInputDto.Order;
            additionalServiceInput.IsRequired = editAdditionalServiceFormInputDto.IsRequired;

            _additionalInputService.Update(additionalServiceInput);


            return CustomResponseDto<Guid>.Success(204, additionalServiceInput.AdditionalServiceID);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditAdditionalServiceFormInputDto> LanguageEditAdditionalServiceFormInput(Guid id, int languageId)
        {
            LanguageEditAdditionalServiceFormInputDto languageEditAdditionalServiceFormInputDto =
                           (from additionalServiceInputLanguageItem in _additionalInputLanguageService.GetAll(x => !x.IsDeleted)
                            where additionalServiceInputLanguageItem.AdditionalServiceInputID == id && additionalServiceInputLanguageItem.LanguageID == languageId
                            select new LanguageEditAdditionalServiceFormInputDto
                            {
                                AdditionalServiceInputLanguageItemID = additionalServiceInputLanguageItem.Id,
                                PropertyName = additionalServiceInputLanguageItem.DisplayName,
                                LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name,
                                AdditionalServiceInputId = additionalServiceInputLanguageItem.AdditionalServiceInputID.ToString(),

                            }).FirstOrDefault();


            return CustomResponseDto<LanguageEditAdditionalServiceFormInputDto>.Success(200, languageEditAdditionalServiceFormInputDto);
        }

        [HttpPost]
        public CustomResponseDto<Guid> LanguageEditAdditionalServiceFormInput(LanguageEditAdditionalServiceFormInputDto languageEditAdditionalServiceFormInputDto)
        {

            AdditionalServiceInputLanguageItem additionalServiceInputLanguageItem = _additionalInputLanguageService.GetById(languageEditAdditionalServiceFormInputDto.AdditionalServiceInputLanguageItemID);

            additionalServiceInputLanguageItem.DisplayName = languageEditAdditionalServiceFormInputDto.PropertyName;
            _additionalInputLanguageService.Update(additionalServiceInputLanguageItem);

            var additionalServiceID = _additionalInputService.GetById(additionalServiceInputLanguageItem.AdditionalServiceInputID).AdditionalServiceID;


            return CustomResponseDto<Guid>.Success(204, additionalServiceID);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<Guid> ToggleAdditionalServiceFormInputStatus(Guid id)
        {
            AdditionalServiceInput additionalServiceInput = _additionalInputService.GetById(id);

            additionalServiceInput.Status = !additionalServiceInput.Status;
            _additionalInputService.Update(additionalServiceInput);

            return CustomResponseDto<Guid>.Success(204, additionalServiceInput.AdditionalServiceID);
        }

        //Form Input Select List
        [HttpGet("{additionalServiceId}")]
        public CustomResponseDto<List<SelectListOptionDto>> FormInputSelectList(Guid additionalServiceId)
        {
            //id = AdditionalServiceId

            List<SelectListOptionDto> selectListOptionDtos =
                (from additionalServiceInput in _additionalInputService.GetAll(x => !x.IsDeleted)
                 where additionalServiceInput.AdditionalServiceID == additionalServiceId
                 join additionalServiceInputLanguageItem in _additionalInputLanguageService.GetAll(x => !x.IsDeleted)
                 on additionalServiceInput.Id equals additionalServiceInputLanguageItem.AdditionalServiceInputID
                 where additionalServiceInputLanguageItem.LanguageID == LanguageList.BaseLanguage.Id
                 select new SelectListOptionDto
                 {
                     OptionID = additionalServiceInput.Id,
                     Option = additionalServiceInputLanguageItem.DisplayName

                 }).ToList();

            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, selectListOptionDtos);
        }

        //Input Type Select List

        [HttpGet]
        public CustomResponseDto<List<SelectListOption>> InputTypeSelectList()
        {
            List<SelectListOption> selectListOptions = InputTypeList.Types;

            return CustomResponseDto<List<SelectListOption>>.Success(200, selectListOptions);
        }


        //Option

        [HttpGet("{additionalServiceID}")]
        public CustomResponseDto<List<AdditionalServiceOptionListDto>> AdditionalServiceOptionList(Guid additionalServiceID)
        {

            var additionalServiceOptlionListDtos =
                (from option in _additionalOptionService.GetAll(x => !x.IsDeleted)
                 where option.AdditionalServiceID == additionalServiceID
                 join optionLanguageItem in _additionalOptionLanguageService.GetAll(x => !x.IsDeleted)
                 on option.Id equals optionLanguageItem.AdditionalServiceOptionID
                 where optionLanguageItem.LanguageID == LanguageList.BaseLanguage.Id
                 select new AdditionalServiceOptionListDto
                 {
                     OptionID = option.Id,
                     OptionName = optionLanguageItem.DisplayName,
                     Status = option.Status,
                     Order = option.Order,
                     FormInputs = (from many in _manyService.GetAll(x => !x.IsDeleted)
                                   where many.AdditionalServiceOptionID == option.Id
                                   join input in _additionalInputService.GetAll(x => !x.IsDeleted)
                                   on many.AdditionalServiceInputID equals input.Id
                                   join inputLanguageItem in _additionalInputLanguageService.GetAll(x => !x.IsDeleted)
                                   on input.Id equals inputLanguageItem.AdditionalServiceInputID
                                   where inputLanguageItem.LanguageID == LanguageList.BaseLanguage.Id
                                   select new AdditionalServiceOptionListFormInputDto()
                                   {
                                       Order = input.Order,
                                       Type = InputTypeList.Types.FirstOrDefault(x => x.ID == input.InputTypeID).Value,
                                       PropertyName = inputLanguageItem.DisplayName
                                   }).ToList()
                 }).ToList();

            return CustomResponseDto<List<AdditionalServiceOptionListDto>>.Success(200, additionalServiceOptlionListDtos);

        }

        [HttpPost]
        public async Task<CustomResponseDto<Guid>> AddAdditionalServiceOption(AddAdditionalServiceOptionDto addAdditionalServiceOptionDto)
        {
            AdditionalServiceOption additionalServiceOption = new AdditionalServiceOption()
            {
                AdditionalServiceID = addAdditionalServiceOptionDto.AdditionalServiceID,
                Order = addAdditionalServiceOptionDto.Order,
            };

            await _additionalOptionService.AddAsync(additionalServiceOption);
            if (addAdditionalServiceOptionDto.FormInputIDs is not null)
                foreach (var inputID in addAdditionalServiceOptionDto.FormInputIDs)
                {
                    Many_AdditionalServiceOption_AdditionalServiceInput many = new Many_AdditionalServiceOption_AdditionalServiceInput()
                    {
                        AdditionalServiceInputID = inputID,
                        AdditionalServiceOptionID = additionalServiceOption.Id
                    };

                    await _manyService.AddAsync(many);
                }

            foreach (var language in LanguageList.AllLanguages)
            {
                AdditionalServiceOptionLanguageItem additionalServiceOptionLanguageItem = new AdditionalServiceOptionLanguageItem()
                {
                    AdditionalServiceOptionID = additionalServiceOption.Id,
                    LanguageID = language.Id,
                    DisplayName = addAdditionalServiceOptionDto.OptionName
                };

                await _additionalOptionLanguageService.AddAsync(additionalServiceOptionLanguageItem);
            }


            return CustomResponseDto<Guid>.Success(204, additionalServiceOption.AdditionalServiceID);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditAdditionalServiceOptionDto> EditAdditionalServiceOption(Guid id)
        {

            AdditionalServiceOption option = _additionalOptionService.GetById(id);
            AdditionalServiceOptionLanguageItem optionLanguageItem = _additionalOptionLanguageService.Where(x => x.LanguageID == LanguageList.BaseLanguage.Id && x.AdditionalServiceOptionID == id).FirstOrDefault();

            EditAdditionalServiceOptionDto editAdditionalServiceOptionDto = new EditAdditionalServiceOptionDto()
            {
                OptionID = option.Id,
                OptionName = optionLanguageItem.DisplayName,
                FormInputIDs = (from many in _manyService.GetAll(x => !x.IsDeleted)
                                where many.AdditionalServiceOptionID == id
                                select many.AdditionalServiceInputID
                               ).ToList(),
                AdditionalServiceId = option.AdditionalServiceID.ToString()
            };


            return CustomResponseDto<EditAdditionalServiceOptionDto>.Success(200, editAdditionalServiceOptionDto);
        }


        [HttpPost]
        public CustomResponseDto<Guid> EditAdditionalServiceOption(EditAdditionalServiceOptionDto editAdditionalServiceOptionDto)
        {

            AdditionalServiceOption option = _additionalOptionService.GetById(editAdditionalServiceOptionDto.OptionID);
            if (editAdditionalServiceOptionDto.Order != option.Order)
            {
                option.Order = editAdditionalServiceOptionDto.Order;
                _additionalOptionService.Update(option);
            }

            var many = _manyService.Where(x => x.AdditionalServiceOptionID == option.Id);

            foreach (var item in many)
            {
                _manyService.Remove(item);
            }


            foreach (var formInput in editAdditionalServiceOptionDto.FormInputIDs)
            {
                Many_AdditionalServiceOption_AdditionalServiceInput newMany = new Many_AdditionalServiceOption_AdditionalServiceInput()
                {
                    AdditionalServiceInputID = formInput,
                    AdditionalServiceOptionID = option.Id
                };

                _manyService.Add(newMany);
            }


            return CustomResponseDto<Guid>.Success(204, option.AdditionalServiceID);
        }


        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditAdditionalServiceOptionDto> LanguageEditAdditionalServiceOption(Guid id, int languageId)
        {

            AdditionalServiceOptionLanguageItem optionLanguageItem = _additionalOptionLanguageService.Where(x => x.AdditionalServiceOptionID == id && x.LanguageID == languageId).FirstOrDefault();

            var additionalServiceID = _additionalOptionService.GetById(id).AdditionalServiceID;

            LanguageEditAdditionalServiceOptionDto languageEditAdditionalServiceOptionDto = new LanguageEditAdditionalServiceOptionDto()
            {
                OptionLanguageItemID = optionLanguageItem.Id,
                LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name,
                OptionName = optionLanguageItem.DisplayName,
                AdditionalServiceID = additionalServiceID
            };


            return CustomResponseDto<LanguageEditAdditionalServiceOptionDto>.Success(200, languageEditAdditionalServiceOptionDto);
        }

        [HttpPost]
        public CustomResponseNullDto LanguageEditAdditionalServiceOption(LanguageEditAdditionalServiceOptionDto languageEditAdditionalServiceOptionDto)
        {
            var languageItem = _additionalOptionLanguageService.GetById(languageEditAdditionalServiceOptionDto.OptionLanguageItemID);

            languageItem.DisplayName = languageEditAdditionalServiceOptionDto.OptionName;
            _additionalOptionLanguageService.Update(languageItem);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleAdditionalServiceOptionStatus(Guid id)
        {

            _additionalOptionService.ToggleStatus(id);



            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<bool> IsOptionPerPerson(Guid id)
        {
            bool isPerPerson = _additionalServiceService.GetById(_additionalOptionService.GetById(id).AdditionalServiceID).IsPerPerson;


            return CustomResponseDto<bool>.Success(200, isPerPerson);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<List<AdditionalServiceOptionPriceListDto>> AdditionalServiceOptionPriceList(Guid id)
        {
            var dates = _additionalOptionPriceDateService.Where(x => x.AdditionalServiceOptionID == id && !x.IsDeleted);

            List<AdditionalServiceOptionPriceListDto> additionalServiceOptionPriceListDtos = new List<AdditionalServiceOptionPriceListDto>();

            foreach (var date in dates)
            {

                var additionalServiceOptionPriceListDto = new AdditionalServiceOptionPriceListDto
                {
                    PriceID = date.Id,
                    FromDate = date.FromDate,
                    ToDate = date.ToDate,
                    Priority = date.Priority,
                    Prices = new List<AdditionalServiceOptionPriceItemDto>()
                };

                var prices = _additionalOptionPriceService.Where(x => x.AdditionalServiceOptionPriceDateID == date.Id);

                foreach (var price in prices)
                {
                    var additionalServiceOptionPriceItemDto = new AdditionalServiceOptionPriceItemDto()
                    {
                        PersonPolicyID = price.PersonPolicyID,
                        Price = price.Price
                    };

                    additionalServiceOptionPriceListDto.Prices.Add(additionalServiceOptionPriceItemDto);
                }

                additionalServiceOptionPriceListDtos.Add(additionalServiceOptionPriceListDto);

            }

            return CustomResponseDto<List<AdditionalServiceOptionPriceListDto>>.Success(200, additionalServiceOptionPriceListDtos);
        }

        [HttpPost]
        public CustomResponseDto<Guid> AddAdditionalServiceOptionPrice(AddAdditionalServiceOptionPriceDto addAdditionalServiceOptionPriceDto)
        {
            AdditionalServiceOptionPriceDate date = new AdditionalServiceOptionPriceDate()
            {
                AdditionalServiceOptionID = addAdditionalServiceOptionPriceDto.OptionID,
                FromDate = addAdditionalServiceOptionPriceDto.FromDate,
                ToDate = addAdditionalServiceOptionPriceDto.ToDate,
                Priority = addAdditionalServiceOptionPriceDto.Priority
            };

            _additionalOptionPriceDateService.Add(date);

            foreach (var price in addAdditionalServiceOptionPriceDto.Prices)
            {
                AdditionalServiceOptionPrice priceEntity = new AdditionalServiceOptionPrice()
                {
                    Price = price.Price,
                    PersonPolicyID = price.PersonPolicyID,
                    AdditionalServiceOptionPriceDateID = date.Id
                };

                _additionalOptionPriceService.Add(priceEntity);
            }


            return CustomResponseDto<Guid>.Success(204, date.AdditionalServiceOptionID);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto DeleteAdditionalServiceOptionPrice(Guid id)
        {

            var date = _additionalOptionPriceDateService.GetById(id);
            date.IsDeleted = true;
            _additionalOptionPriceDateService.Update(date);

            return CustomResponseNullDto.Success(204);

        }

        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> AdditionalServiceSelectList()
        {
            List<SelectListOptionDto> additionalServiceSelectList =
                (from additionalService in _additionalServiceService.GetAll(x => !x.IsDeleted)
                 join additionalServiceLanguageItem in _additionalServiceLanguageService.GetAll(x => !x.IsDeleted)
                 on additionalService.Id equals additionalServiceLanguageItem.AdditionalServiceID
                 where additionalServiceLanguageItem.LanguageID == LanguageList.BaseLanguage.Id
                 select new SelectListOptionDto()
                 {
                     OptionID = additionalService.Id,
                     Option = additionalServiceLanguageItem.DisplayName
                 }).ToList();


            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, additionalServiceSelectList);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<List<SelectListOptionDto>> AdditionalServiceOptionSelectList(Guid id)
        {

            List<SelectListOptionDto> additionalServiceOptionSelectList =
                (from option in _additionalOptionService.GetAll(x => !x.IsDeleted)
                 where option.AdditionalServiceID == id
                 join languageItem in _additionalOptionLanguageService.GetAll(x => !x.IsDeleted)
                 on option.Id equals languageItem.AdditionalServiceOptionID
                 where languageItem.LanguageID == LanguageList.BaseLanguage.Id
                 select new SelectListOptionDto()
                 {

                     OptionID = option.Id,
                     Option = languageItem.DisplayName
                 }).ToList();


            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, additionalServiceOptionSelectList);
        }

        //[HttpGet("{id}")]
        //public CustomResponseDto<EditAdditionalServiceOptionPriceDto> EditAdditionalServiceOptionPrice(string id)
        //{
        //    EditAdditionalServiceOptionPriceDto editAdditionalServiceOptionPriceDto = new EditAdditionalServiceOptionPriceDto();

        //    if (id == "1")
        //    {
        //        editAdditionalServiceOptionPriceDto = new EditAdditionalServiceOptionPriceDto()
        //        {
        //            OptionID = "1",
        //            PriceID = id,
        //            FromDate = DateTime.Now.AddDays(131),
        //            ToDate = DateTime.Now.AddDays(154),
        //            Priority = 1,
        //            Prices = new List<AdditionalServiceOptionPriceItemDto>()
        //            {
        //                new AdditionalServiceOptionPriceItemDto()
        //                {
        //                    PersonPolicyID = "1",
        //                    Price = 150
        //                },
        //                new AdditionalServiceOptionPriceItemDto()
        //                {
        //                    PersonPolicyID = "3",
        //                    Price = 300
        //                },
        //                new AdditionalServiceOptionPriceItemDto()
        //                {
        //                    PersonPolicyID = "2",
        //                    Price = 275
        //                }
        //            }
        //        };
        //    }
        //    if (id == "2")
        //    {
        //        editAdditionalServiceOptionPriceDto = new EditAdditionalServiceOptionPriceDto()
        //        {
        //            OptionID = "1",
        //            PriceID = id,
        //            Priority = 2,
        //            FromDate = DateTime.Now.AddDays(131),
        //            ToDate = DateTime.Now.AddDays(154),
        //            Prices = new List<AdditionalServiceOptionPriceItemDto>()
        //            {
        //                new AdditionalServiceOptionPriceItemDto()
        //                {
        //                    PersonPolicyID = "1",
        //                    Price = 150
        //                },
        //                new AdditionalServiceOptionPriceItemDto()
        //                {
        //                    PersonPolicyID = "3",
        //                    Price = 300
        //                },
        //                new AdditionalServiceOptionPriceItemDto()
        //                {
        //                    PersonPolicyID = "2",
        //                    Price = 275
        //                }
        //            }
        //        };
        //    }
        //    if (id == "3")
        //    {
        //        editAdditionalServiceOptionPriceDto = new EditAdditionalServiceOptionPriceDto()
        //        {
        //            OptionID = "2",
        //            PriceID = id,
        //            Priority = 1,
        //            FromDate = DateTime.Now,
        //            ToDate = DateTime.Now.AddDays(300),
        //            Prices = new List<AdditionalServiceOptionPriceItemDto>()
        //            {
        //                new AdditionalServiceOptionPriceItemDto()
        //                {
        //                    PersonPolicyID = "1",
        //                    Price = 250
        //                },
        //                new AdditionalServiceOptionPriceItemDto()
        //                {
        //                    PersonPolicyID = "3",
        //                    Price = 350
        //                },
        //                new AdditionalServiceOptionPriceItemDto()
        //                {
        //                    PersonPolicyID = "2",
        //                    Price = 300
        //                }
        //            }
        //        };
        //    }
        //    if (id == "4")
        //    {
        //        editAdditionalServiceOptionPriceDto = new EditAdditionalServiceOptionPriceDto()
        //        {
        //            OptionID = "3",
        //            PriceID = id,
        //            Priority = 1,
        //            FromDate = DateTime.Now,
        //            ToDate = DateTime.Now.AddDays(300),
        //            Prices = new List<AdditionalServiceOptionPriceItemDto>()
        //            {
        //                new AdditionalServiceOptionPriceItemDto()
        //                {
        //                    PersonPolicyID = "1",
        //                    Price = 250
        //                },
        //                new AdditionalServiceOptionPriceItemDto()
        //                {
        //                    PersonPolicyID = "3",
        //                    Price = 350
        //                },
        //                new AdditionalServiceOptionPriceItemDto()
        //                {
        //                    PersonPolicyID = "2",
        //                    Price = 300
        //                }
        //            }
        //        };
        //    }
        //    if (id == "5")
        //    {
        //        editAdditionalServiceOptionPriceDto = new EditAdditionalServiceOptionPriceDto()
        //        {
        //            OptionID = "7",
        //            PriceID = id,
        //            Priority = 1,
        //            FromDate = DateTime.Now,
        //            ToDate = DateTime.Now.AddDays(200),
        //            Prices = new List<AdditionalServiceOptionPriceItemDto>()
        //            {
        //                new AdditionalServiceOptionPriceItemDto()
        //                {
        //                    PersonPolicyID = null,
        //                    Price = 70
        //                },
        //            }
        //        };
        //    }
        //    if (id == "6")
        //    {
        //        editAdditionalServiceOptionPriceDto = new EditAdditionalServiceOptionPriceDto()
        //        {
        //            OptionID = "7",
        //            PriceID = id,
        //            Priority = 2,
        //            FromDate = DateTime.Now.AddDays(30),
        //            ToDate = DateTime.Now.AddDays(37),
        //            Prices = new List<AdditionalServiceOptionPriceItemDto>()
        //            {
        //                new AdditionalServiceOptionPriceItemDto()
        //                {
        //                    PersonPolicyID = null,
        //                    Price = 40
        //                },
        //            }
        //        };
        //    }
        //    if (id == "7")
        //    {
        //        editAdditionalServiceOptionPriceDto = new EditAdditionalServiceOptionPriceDto()
        //        {
        //            OptionID = "8",
        //            PriceID = id,
        //            Priority = 1,
        //            FromDate = DateTime.Now,
        //            ToDate = DateTime.Now.AddDays(200),
        //            Prices = new List<AdditionalServiceOptionPriceItemDto>()
        //            {
        //                new AdditionalServiceOptionPriceItemDto()
        //                {
        //                    PersonPolicyID = null,
        //                    Price = 40
        //                },
        //            }
        //        };
        //    }
        //    if (id == "8")
        //    {
        //        editAdditionalServiceOptionPriceDto = new EditAdditionalServiceOptionPriceDto()
        //        {
        //            OptionID = "8",
        //            PriceID = id,
        //            Priority = 2,
        //            FromDate = DateTime.Now.AddDays(30),
        //            ToDate = DateTime.Now.AddDays(37),
        //            Prices = new List<AdditionalServiceOptionPriceItemDto>()
        //            {
        //                new AdditionalServiceOptionPriceItemDto()
        //                {
        //                    PersonPolicyID = null,
        //                    Price = 30
        //                },

        //            }
        //        };
        //    }

        //    return CustomResponseDto<EditAdditionalServiceOptionPriceDto>.Success(200, editAdditionalServiceOptionPriceDto);
        //}

        //[HttpPost]
        //public CustomResponseNullDto EditAdditionalServiceOptionPrice(EditAdditionalServiceOptionPriceDto editAdditionalServiceOptionPriceDto)
        //{
        //    return CustomResponseNullDto.Success(204);
        //}



    }
}
