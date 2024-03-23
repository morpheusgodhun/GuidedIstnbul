using Dto.ApiPanelDtos.AdditionalServiceDtos;
using Dto.ApiPanelDtos.LanguageDtos;
using Dto.ApiPanelDtos.PersonPolicyDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.AdditionalServiceManagement)]
    public class AdditionalServiceController : CustomBaseController
    {
        public AdditionalServiceController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {

            string url = _url + "PanelAdditionalService/AdditionalServiceList";
            CustomResponseDto<List<AdditionalServiceListDto>> additionalServiceListDto = _apiHandler.GetApi<CustomResponseDto<List<AdditionalServiceListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);
            if (additionalServiceListDto is null)
            {
                return View();
            }
            else
            {
                var model = (additionalServiceListDto.Data, languageListDto.Data);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddAdditionalService()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAdditionalService(AddAdditionalServiceDto additionalService)
        {
            string url = _url + "PanelAdditionalService/AddAdditionalService";
            _apiHandler.PostApi<CustomResponseNullDto>(additionalService, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditAdditionalService(string id)
        {
            string url = _url + "PanelAdditionalService/EditAdditionalService/" + id;
            CustomResponseDto<EditAdditionalServiceDto> editAdditionalServiceDto = _apiHandler.GetApi<CustomResponseDto<EditAdditionalServiceDto>>(url);

            if (editAdditionalServiceDto is null)
            {
                return View();
            }
            else
            {
                return View(editAdditionalServiceDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditAdditionalService(EditAdditionalServiceDto editAdditionalServiceDto)
        {
            string url = _url + "PanelAdditionalService/EditAdditionalService";
            _apiHandler.PostApi<CustomResponseNullDto>(editAdditionalServiceDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LanguageEditAdditionalService(string id, string languageId)
        {
            string url = _url + "PanelAdditionalService/LanguageEditAdditionalService/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditAdditionalServiceDto> languageEditAdditionalServiceDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditAdditionalServiceDto>>(url);

            if (languageEditAdditionalServiceDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditAdditionalServiceDto.Data);
            }
        }

        [HttpPost]
        public IActionResult LanguageEditAdditionalService(LanguageEditAdditionalServiceDto languageEditAdditionalServiceDto)
        {
            string url = _url + "PanelAdditionalService/LanguageEditAdditionalService";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditAdditionalServiceDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleAdditionalServiceStatus(string id)
        {
            string url = _url + "PanelAdditionalService/ToggleAdditionalServiceStatus/" + id;
            _apiHandler.GetApi<CustomResponseDto<LanguageEditAdditionalServiceDto>>(url);
            return RedirectToAction("Index");
        }

        //Form Input

        public IActionResult FormInputList(string id)
        {
            ViewBag.AdditionalServiceID = id;

            string url = _url + "PanelAdditionalService/FormInputList/" + id;
            CustomResponseDto<List<AdditionalServiceFormInputListDto>> additionalServiceFormInputListDto = _apiHandler.GetApi<CustomResponseDto<List<AdditionalServiceFormInputListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);
            if (additionalServiceFormInputListDto is null)
            {
                return View();
            }
            else
            {
                var model = (additionalServiceFormInputListDto.Data, languageListDto.Data);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddFormInput(string id)
        {
            ViewBag.AdditionalServiceID = id;

            string url = _url + "PanelAdditionalService/InputTypeSelectList";
            CustomResponseDto<List<SelectListOption>> inputTypeSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOption>>>(url);

            ViewBag.TypeList = inputTypeSelectList.Data;

            return View();
        }

        [HttpPost]
        public IActionResult AddFormInput(AddAdditionalServiceFormInputDto addAdditionalServiceFormInputDto)
        {
            string url = _url + "PanelAdditionalService/AddAdditionalServiceFormInput";
            _apiHandler.PostApi<CustomResponseNullDto>(addAdditionalServiceFormInputDto, url);
            return RedirectToAction("FormInputList", new { id = addAdditionalServiceFormInputDto.AdditionalServiceID });
        }

        [HttpGet]
        public IActionResult EditFormInput(string id)
        {
            string url2 = _url + "PanelAdditionalService/InputTypeSelectList";
            CustomResponseDto<List<SelectListOption>> inputTypeSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOption>>>(url2);

            ViewBag.TypeList = inputTypeSelectList.Data;

            string url = _url + "PanelAdditionalService/EditAdditionalServiceFormInput/" + id;
            CustomResponseDto<EditAdditionalServiceFormInputDto> editAdditionalServiceFormInputDto = _apiHandler.GetApi<CustomResponseDto<EditAdditionalServiceFormInputDto>>(url);

            if (editAdditionalServiceFormInputDto is null)
            {
                return View();
            }
            else
            {
                return View(editAdditionalServiceFormInputDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditFormInput(EditAdditionalServiceFormInputDto editAdditionalServiceFormInputDto)
        {
            string url = _url + "PanelAdditionalService/EditAdditionalServiceFormInput";
            var additionalServiceID = _apiHandler.PostApi<CustomResponseDto<Guid>>(editAdditionalServiceFormInputDto, url);
            //return RedirectToAction("FormInputList", new { id = editAdditionalServiceFormInputDto.AdditionalServiceID });
            return RedirectToAction("FormInputList", new { id = additionalServiceID.Data });
        }

        [HttpGet]
        public IActionResult LanguageEditFormInput(string id, string languageId)
        {

            string url = _url + "PanelAdditionalService/LanguageEditAdditionalServiceFormInput/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditAdditionalServiceFormInputDto> languageEditAdditionalServiceFormInputDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditAdditionalServiceFormInputDto>>(url);

            if (languageEditAdditionalServiceFormInputDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditAdditionalServiceFormInputDto.Data);
            }
        }

        [HttpPost]
        public IActionResult LanguageEditFormInput(LanguageEditAdditionalServiceFormInputDto languageEditAdditionalServiceFormInputDto)
        {
            string url = _url + "PanelAdditionalService/LanguageEditAdditionalServiceFormInput";
            var additionalServiceID = _apiHandler.PostApi<CustomResponseDto<Guid>>(languageEditAdditionalServiceFormInputDto, url);
            //return RedirectToAction("FormInputList", new { id = editAdditionalServiceFormInputDto.AdditionalServiceID });
            return RedirectToAction("FormInputList", new { id = additionalServiceID.Data });
        }

        public IActionResult ToggleFormInputStatus(string id)
        {
            string url = _url + "PanelAdditionalService/ToggleAdditionalServiceFormInputStatus/" + id;
            var additionalServiceID = _apiHandler.GetApi<CustomResponseDto<Guid>>(url);
            return RedirectToAction("FormInputList", new { id = additionalServiceID.Data });
        }

        //Option
        public IActionResult OptionList(string id)
        {
            ViewBag.AdditionalServiceID = id;

            string url = _url + "PanelAdditionalService/AdditionalServiceOptionList/" + id;

            CustomResponseDto<List<AdditionalServiceOptionListDto>> additionalServiceOptionListDto = _apiHandler.GetApi<CustomResponseDto<List<AdditionalServiceOptionListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);
            if (additionalServiceOptionListDto is null)
            {
                return View();
            }
            else
            {
                var model = (additionalServiceOptionListDto.Data, languageListDto.Data);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddOption(string id)
        {
            ViewBag.AdditionalServiceID = id;

            string url = _url + "PanelAdditionalService/FormInputSelectList/" + id;
            CustomResponseDto<List<SelectListOptionDto>> formInputSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url);

            ViewBag.FormInputs = formInputSelectList.Data;

            return View();
        }

        [HttpPost]
        public IActionResult AddOption(AddAdditionalServiceOptionDto addAdditionalServiceOptionDto)
        {
            string url = _url + "PanelAdditionalService/AddAdditionalServiceOption";
            var additionalServiceID = _apiHandler.PostApi<CustomResponseDto<Guid>>(addAdditionalServiceOptionDto, url);

            return RedirectToAction("OptionList", new { id = additionalServiceID.Data });
        }

        [HttpGet]
        [Route("[controller]/[action]/{id}/{AdditionalServiceId}")]
        public IActionResult EditOption(string id, string AdditionalServiceId)
        {
            //id = option id

            string url2 = _url + "PanelAdditionalService/FormInputSelectList/" + AdditionalServiceId;
            CustomResponseDto<List<SelectListOptionDto>> formInputSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url2);

            ViewBag.FormInputs = formInputSelectList.Data;

            string url = _url + "PanelAdditionalService/EditAdditionalServiceOption/" + id;
            CustomResponseDto<EditAdditionalServiceOptionDto> editAdditionalServiceOptionDto = _apiHandler.GetApi<CustomResponseDto<EditAdditionalServiceOptionDto>>(url);

            if (editAdditionalServiceOptionDto is null)
            {
                return View();
            }
            else
            {
                return View(editAdditionalServiceOptionDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditOption(EditAdditionalServiceOptionDto editAdditionalServiceOptionDto)
        {
            if (editAdditionalServiceOptionDto.FormInputIDs == null)
            {
                editAdditionalServiceOptionDto.FormInputIDs = new List<Guid>();
            }
            string url = _url + "PanelAdditionalService/EditAdditionalServiceOption";
            var additionalServiceID = _apiHandler.PostApi<CustomResponseDto<Guid>>(editAdditionalServiceOptionDto, url);

            return RedirectToAction("OptionList", new { id = additionalServiceID.Data });
        }

        [HttpGet]
        public IActionResult LanguageEditOption(string id, string languageId)
        {
            string url = _url + "PanelAdditionalService/LanguageEditAdditionalServiceOption/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditAdditionalServiceOptionDto> languageEditAdditionalServiceOptionDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditAdditionalServiceOptionDto>>(url);

            if (languageEditAdditionalServiceOptionDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditAdditionalServiceOptionDto.Data);
            }
        }

        [HttpPost]
        public IActionResult LanguageEditOption(LanguageEditAdditionalServiceOptionDto languageEditAdditionalServiceOptionDto)
        {
            string url = _url + "PanelAdditionalService/LanguageEditAdditionalServiceOption";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditAdditionalServiceOptionDto, url);

            return RedirectToAction("OptionList", new { id = languageEditAdditionalServiceOptionDto.AdditionalServiceID });
        }

        [Route("[controller]/[action]/{id}/{AdditionalServiceId}")]
        public IActionResult ToggleOptionStatus(string id, string AdditionalServiceId)
        {
            string url = _url + "PanelAdditionalService/ToggleAdditionalServiceOptionStatus/" + id;
            _apiHandler.GetApi<CustomResponseDto<LanguageEditAdditionalServiceDto>>(url);

            return RedirectToAction("OptionList", new { id = AdditionalServiceId });
        }

        public IActionResult OptionPriceList(string id)
        {
            ViewBag.OptionID = id;

            string url = _url + "PanelAdditionalService/AdditionalServiceOptionPriceList/" + id;
            CustomResponseDto<List<AdditionalServiceOptionPriceListDto>> additionalServiceOptionPriceListDto = _apiHandler.GetApi<CustomResponseDto<List<AdditionalServiceOptionPriceListDto>>>(url);

            string url2 = _url + "PanelAdditionalService/IsOptionPerPerson/" + id;
            CustomResponseDto<bool> isPerPerson = _apiHandler.GetApi<CustomResponseDto<bool>>(url2);
            ViewBag.IsPerPerson = isPerPerson.Data;

            string url3 = _url + "PanelPersonPolicy/PersonPolicyDtoList";
            CustomResponseDto<List<PersonPolicyDto>> personPolicyList = _apiHandler.GetApi<CustomResponseDto<List<PersonPolicyDto>>>(url3);
            ViewBag.PersonPolicyList = personPolicyList.Data;


            if (additionalServiceOptionPriceListDto is null)
            {
                return View();
            }
            else
            {
                return View(additionalServiceOptionPriceListDto.Data);
            }
        }

        [HttpGet]
        public IActionResult AddOptionPrice(string id)
        {
            ViewBag.OptionID = id;

            string url2 = _url + "PanelAdditionalService/IsOptionPerPerson/" + id;
            CustomResponseDto<bool> isPerPerson = _apiHandler.GetApi<CustomResponseDto<bool>>(url2);
            ViewBag.IsPerPerson = isPerPerson.Data;

            string url3 = _url + "PanelPersonPolicy/PersonPolicyDtoList";
            CustomResponseDto<List<PersonPolicyDto>> personPolicyList = _apiHandler.GetApi<CustomResponseDto<List<PersonPolicyDto>>>(url3);
            ViewBag.PersonPolicyList = personPolicyList.Data;


            return View();
        }

        [HttpPost]
        public IActionResult AddOptionPrice(AddAdditionalServiceOptionPriceDto addAdditionalServiceOptionPriceDto)
        {
            string url = _url + "PanelAdditionalService/AddAdditionalServiceOptionPrice";
            var optionID = _apiHandler.PostApi<CustomResponseDto<Guid>>(addAdditionalServiceOptionPriceDto, url);
            return RedirectToAction("OptionPriceList", new { id = optionID.Data });
        }

        [Route("[controller]/[action]/{id}/{OptionID}")]
        public IActionResult DeleteOptionPrice(string id, string OptionID)
        {
            string url = _url + "PanelAdditionalService/DeleteAdditionalServiceOptionPrice/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("OptionPriceList", new { id = OptionID });
        }

        //[HttpGet]
        //public IActionResult EditOptionPrice(string id)
        //{
        //    string url = _url + "PanelAdditionalService/EditAdditionalServiceOptionPrice/" + id;
        //    CustomResponseDto<EditAdditionalServiceOptionPriceDto> editAdditionalServiceOptionPriceDto = _apiHandler.GetApi<CustomResponseDto<EditAdditionalServiceOptionPriceDto>>(url);

        //    if (editAdditionalServiceOptionPriceDto is null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return View(editAdditionalServiceOptionPriceDto.Data);
        //    }
        //}

        //[HttpPost]
        //public IActionResult EditOptionPrice(EditAdditionalServiceOptionPriceDto editAdditionalServiceOptionPriceDto)
        //{
        //    string url = _url + "PanelAdditionalService/AddAdditionalServiceOptionPrice";
        //    _apiHandler.PostApi<CustomResponseNullDto>(editAdditionalServiceOptionPriceDto, url);
        //    return RedirectToAction("", new { id = editAdditionalServiceOptionPriceDto.OptionID });
        //}

    }
}
