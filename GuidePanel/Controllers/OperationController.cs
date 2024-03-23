using Dto.ApiPanelDtos.OperationDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.OperationManagement)]
    public class OperationController : CustomBaseController
    {
        public OperationController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            string guideSelectListUrl = _url + "PanelOtherOption/GuideSelectList";
            CustomResponseDto<List<SelectListOptionDto>> guideSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(guideSelectListUrl);
            ViewBag.GuideSelectList = guideSelectList.Data;

            string shopStatusSelectListUrl = _url + "PanelOtherOption/ShopStatusSelectList";
            CustomResponseDto<List<SelectListOption>> shopStatusSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOption>>>(shopStatusSelectListUrl);
            ViewBag.ShopStatusSelectList = shopStatusSelectList.Data;

            string supplierSelectListUrl = $"{_url}PanelOtherOption/SupplierSelectListForVehicle";
            CustomResponseDto<List<SelectListOptionDto>> supplierSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(supplierSelectListUrl);
            ViewBag.SupplierSelectList = supplierSelectList.Data;


            string apiUrl = _url + "PanelOperation/OperationList";
            CustomResponseDto<List<OperationListDto>> response = _apiHandler.GetApi<CustomResponseDto<List<OperationListDto>>>(apiUrl);

            List<OperationVM> operationVMList = new();
            DateTime date = DateTime.Now.Date;
            //var operationEnumarable = response.Data.Where(x => x.OperationStartDate.Date >= date).AsEnumerable();
            var operationEnumarable = response.Data; //üstteki satırı açacam
            //date filtrelemeyi api a taşı - hjazır data gelsin

            foreach (var operationItem in operationEnumarable)
            {

                int operationTotalDay = operationItem.OperationGuides.OrderByDescending(x => x.Day).FirstOrDefault().Day;
                for (int day = 0; day < operationTotalDay; day++)
                {
                    //total durationu bilirsem operasyon başlama tarihine göre sıralanmış datayı döngüye sokarak gününü belirteiblirim.
                    //total durationu operationGuide veya operationVehicle dan elde
                    //operationGuide vehicle vs. crosstablelarda day bilgim var?? işime yaramaz - yaradı !

                    OperationVM operationVM = new()
                    {
                        ReservationCode = operationItem.ReservationCode,
                        ReservationId = operationItem.ReservationId,
                        CustomerName = operationItem.CustomerName,
                        Date = day == 1 ? operationItem.OperationStartDate : operationItem.OperationStartDate.AddDays(day),
                        Hotel = operationItem.Hotel,
                        NumberOfParticipants = operationItem.NumberOfParticipants,
                        PickUpPoint = operationItem.PickUpPoint,
                        ProductName = operationItem.ProductName,
                        Day = day + 1,
                        ProductImagePath = operationItem.ProductImagePath //todo
                    };

                    operationVM.OperationGuide = operationItem.OperationGuides.FirstOrDefault(x => x.Day == operationVM.Day);
                    operationVM.OperationNotes = operationItem.OperationNotes.FirstOrDefault(x => x.Day == operationVM.Day);
                    operationVM.OperationAdditionalService = operationItem.OperationAdditionalServices.FirstOrDefault(x => x.Day == operationVM.Day);
                    operationVM.OperationVehicle = operationItem.OperationVehicles.FirstOrDefault(x => x.Day == operationVM.Day);
                    operationVM.OperationShop = operationItem.OperationShops.FirstOrDefault(x => x.Day == operationVM.Day);
                    operationVM.OperationTicket = operationItem.OperationTickets.FirstOrDefault(x => x.Day == operationVM.Day);


                    operationVMList.Add(operationVM);
                    date.AddDays(day);
                }
            }

            if (response is null)
                return View();
            else
                return View(operationVMList);
        }
        [HttpGet]
        public IActionResult VehicleSelectList(string id)
        {
            string apiUrl = $"{_url}PanelOtherOption/VehicleSelectList/{id}";
            CustomResponseDto<List<SelectListOptionDto>> vehicleSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(apiUrl);
            return Json(vehicleSelectList);
        }
        //assign etme ve update için de aynı nesneleri ve actionları kullanıyorum.
        [HttpPost]
        public IActionResult AssignVehicle(AssignVehicleToOperationDto assignVehicle)
        {
            string apiUrl = _url + "PanelOperation/AssignVehicle";
            CustomResponseNullDto response = _apiHandler.PostApi<CustomResponseNullDto>(assignVehicle, apiUrl);
            return Json(response);
        }
        //assign etme ve update için de aynı nesneleri ve actionları kullanıyorum.
        [HttpPost]
        public IActionResult AssignGuide(AssignGuideToOperationDto assignGuide)
        {
            string apiUrl = _url + "PanelOperation/AssignGuide";
            CustomResponseNullDto response = _apiHandler.PostApi<CustomResponseNullDto>(assignGuide, apiUrl);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult GetOperationGuide(string id)
        {
            string apiUrl = _url + $"PanelOperation/GetOperationGuide/{id}";
            CustomResponseDto<AssignGuideToOperationDto> response = _apiHandler.GetApi<CustomResponseDto<AssignGuideToOperationDto>>(apiUrl);
            return Json(response);
        }

        [HttpGet]
        public IActionResult GetOperationVehicle(string id)
        {
            string apiUrl = _url + $"PanelOperation/GetOperationVehicle/{id}";
            CustomResponseDto<AssignVehicleToOperationDto> response = _apiHandler.GetApi<CustomResponseDto<AssignVehicleToOperationDto>>(apiUrl);
            return Json(response);
        }

        [HttpPost]
        public IActionResult SaveDailyOperationNote(SaveDailyOperationNoteDto saveDailyOperationNoteDto)
        {
            if (string.IsNullOrEmpty(saveDailyOperationNoteDto.OperationNote) || string.IsNullOrWhiteSpace(saveDailyOperationNoteDto.OperationNote))
                return RedirectToAction("Index");

            string apiUrl = _url + "PanelOperation/SaveDailyOperationNote";
            CustomResponseNullDto response = _apiHandler.PostApi<CustomResponseNullDto>(saveDailyOperationNoteDto, apiUrl);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveTicketInfo(SaveTicketInfoDto saveTicketInfoDto)
        {
            string apiUrl = _url + "PanelOperation/SaveTicketInfo";
            CustomResponseNullDto response = _apiHandler.PostApi<CustomResponseNullDto>(saveTicketInfoDto, apiUrl);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveHour(SaveHourInfoDto saveHOurDto)
        {
            string apiUrl = _url + "PanelOperation/SaveHourInfo";
            CustomResponseNullDto response = _apiHandler.PostApi<CustomResponseNullDto>(saveHOurDto, apiUrl);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeShopStatus(ChangeShopStatusDto changeShopStatusDto)
        {
            string apiUrl = _url + "PanelOperation/ChangeShopStatus";
            CustomResponseNullDto response = _apiHandler.PostApi<CustomResponseNullDto>(changeShopStatusDto, apiUrl);
            return RedirectToAction("Index");
        }

    }

}
