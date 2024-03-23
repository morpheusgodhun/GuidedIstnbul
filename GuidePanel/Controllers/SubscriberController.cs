using ClosedXML.Excel;
using Dto.ApiPanelDtos.SubscriberDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.SubscriberManagement)]
    public class SubscriberController : CustomBaseController
    {
        public SubscriberController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            string url = _url + "PanelSubscriber/SubscriberList";
            CustomResponseDto<List<SubscriberListDto>> subscriberListDto = _apiHandler.GetApi<CustomResponseDto<List<SubscriberListDto>>>(url);
            if (subscriberListDto is null)
            {
                return View();
            }
            else
            {
                return View(subscriberListDto.Data);
            }
        }
        public IActionResult ToggleSubscriberStatus(string id)
        {
            string url = _url + "PanelSubscriber/ToggleSubscriberStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }

        public IActionResult ExportAllSubscribers()
        {
            string url = _url + "PanelSubscriber/SubscriberList";
            CustomResponseDto<List<SubscriberListDto>> subscriberListDto = _apiHandler.GetApi<CustomResponseDto<List<SubscriberListDto>>>(url);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("SubscriberList");
                worksheet.Cell(1, 1).Value = "SubscriberID";
                worksheet.Cell(1, 2).Value = "Email";
                worksheet.Cell(1, 3).Value = "MemberShipDate";
                worksheet.Cell(1, 4).Value = "Status";
                int i = 2;
                foreach (var subscriber in subscriberListDto.Data)
                {
                    worksheet.Cell(i, 1).Value = subscriber.SubscriberID;
                    worksheet.Cell(i, 2).Value = subscriber.Email;
                    worksheet.Cell(i, 3).Value = subscriber.MembershipDate.ToString("dd/MM/yyyy");
                    if (subscriber.Status)
                    {
                        worksheet.Cell(i, 4).Value = "Active";
                    }
                    else
                    {
                        worksheet.Cell(i, 4).Value = "Passive";
                    }

                    i++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Subscribers.xlsx");
                }
            }
        }

        public IActionResult ExportActiveSubscribers()
        {
            string url = _url + "PanelSubscriber/GetActiveSubscriberList";
            CustomResponseDto<List<SubscriberListDto>> subscriberListDto = _apiHandler.GetApi<CustomResponseDto<List<SubscriberListDto>>>(url);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("SubscriberList");
                worksheet.Cell(1, 1).Value = "SubscriberID";
                worksheet.Cell(1, 2).Value = "Email";
                worksheet.Cell(1, 3).Value = "MemberShipDate";
                worksheet.Cell(1, 4).Value = "Status";
                int i = 2;
                foreach (var subscriber in subscriberListDto.Data)
                {
                    worksheet.Cell(i, 1).Value = subscriber.SubscriberID;
                    worksheet.Cell(i, 2).Value = subscriber.Email;
                    worksheet.Cell(i, 3).Value = subscriber.MembershipDate.ToString("dd/MM/yyyy");
                    if (subscriber.Status)
                    {
                        worksheet.Cell(i, 4).Value = "Active";
                    }
                    else
                    {
                        worksheet.Cell(i, 4).Value = "Passive";
                    }

                    i++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Subscribers.xlsx");
                }
            }
        }
    }
}
