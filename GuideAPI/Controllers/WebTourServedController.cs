using Dto.ApiWebDtos.ApiToWebDtos.Guide;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebTourServedController : ControllerBase
    {
        [HttpGet("{LanguageID}")]  //Done
        public CustomResponseDto<GetTourServedDto> GetTourServed(string LanguageID)
        {
            GetTourServedDto getTourServedDto = new GetTourServedDto()
            {
                ConstantValues = new List<ConstantValueDto>()
                {
                    new ConstantValueDto()
                    {
                        Key = "TourServed.Title",
                        Value = "Tours I've Served"
                    },
                    new ConstantValueDto()
                    {
                        Key = "TourServed.Table.ProgramDetailLabel",
                        Value = "Program Detail"
                    },
                    new ConstantValueDto()
                    {
                        Key = "TourServed.Table.CommentReportLabel",
                        Value = "Comment Report"
                    },
                    new ConstantValueDto()
                    {
                        Key = "TourServed.Table.TourDateLabel",
                        Value = "Tour Date"
                    },
                    new ConstantValueDto()
                    {
                        Key = "TourServed.Table.TourTitleLabel",
                        Value = "Tour Title"
                    },
                    new ConstantValueDto()
                    {
                        Key = "TourServed.Table.CityLabel",
                        Value = "City"
                    },
                    new ConstantValueDto()
                    {
                        Key = "TourServed.Table.ProgramDetailButton",
                        Value = "Program Detail"
                    },
                    new ConstantValueDto()
                    {
                        Key = "TourServed.Table.CommentReportButton",
                        Value = "Send Comment Report"
                    },
                    new ConstantValueDto()
                    {
                        Key = "TourServed.DetailModal.Title",
                        Value = "Program Detail"
                    },
                    new ConstantValueDto()
                    {
                        Key = "TourServed.CommentReportModal.Title",
                        Value = "Send Comment Report"
                    },
                    new ConstantValueDto()
                    {
                        Key = "TourServed.CommentReportModal.Select",
                        Value = "Select"
                    },
                    new ConstantValueDto()
                    {
                        Key = "TourServed.CommentReportModal.Glad",
                        Value = "Plased"
                    },
                    new ConstantValueDto()
                    {
                        Key = "TourServed.CommentReportModal.NotGlad",
                        Value = "Not Glad"
                    },
                    new ConstantValueDto()
                    {
                        Key = "TourServed.CommentReportModal.Question",
                        Value = "How was customer satisfaction?"
                    },
                    new ConstantValueDto()
                    {
                        Key = "TourServed.CommentReportModal.SaveButton",
                        Value = "Save"
                    },
                },
                Tours = new List<TourServedTourDto>()
                {
                    new TourServedTourDto()
                    {
                        TourID = "1",
                        TourTitle = "BEST OF ISTANBUL: 1, 2 OR 3-DAY PRIVATE GUIDED ISTANBUL TOUR",
                        City = "İstanbul",
                        ProgramDetail = "<p class=\"text-indent-2\">Misafirler sabah otellerinden alınacak ve Troya bölgesine doğru yola çıkacaklardır.</p> <p class=\"text-indent-2\">Troy(Truva), Tevfikiye Köyü, İntepe, Çanakkale Türkiye, 3 saat.</p> <p class=\"text-indent-2\">Truva Şehir Maketi, İskele Meydanı, Çanakkale 17000 Türkiye, 4 saat. </p>",
                        Date = DateTime.Now.AddDays(1),
                    },
                    new TourServedTourDto()
                    {
                        TourID = "2",
                        TourTitle = "2 DAYS PRIVATE EPHESUS AND PAMUKKALE TOUR",
                        City = "Denizli",
                        ProgramDetail = "<p class=\"text-indent-2\">Misafirler sabah otellerinden alınacak ve Troya bölgesine doğru yola çıkacaklardır.</p> <p class=\"text-indent-2\">Troy(Truva), Tevfikiye Köyü, İntepe, Çanakkale Türkiye, 3 saat.</p> <p class=\"text-indent-2\">Truva Şehir Maketi, İskele Meydanı, Çanakkale 17000 Türkiye, 4 saat. </p>",
                        Date = DateTime.Now.AddDays(-2),
                    },
                    
                    new TourServedTourDto()
                    {
                        TourID = "3",
                        TourTitle = "BEST OF CAPPADOCIA: 1, 2 OR 3-DAY PRIVATE CAPPADOCIA TOUR",
                        City = "Nevşehir",
                        ProgramDetail = "<p class=\"text-indent-2\">Misafirler sabah otellerinden alınacak ve Troya bölgesine doğru yola çıkacaklardır.</p> <p class=\"text-indent-2\">Troy(Truva), Tevfikiye Köyü, İntepe, Çanakkale Türkiye, 3 saat.</p> <p class=\"text-indent-2\">Truva Şehir Maketi, İskele Meydanı, Çanakkale 17000 Türkiye, 4 saat. </p>",
                        Date = DateTime.Now.AddDays(3),
                    },
                }
            };


            return CustomResponseDto<GetTourServedDto>.Success(200, getTourServedDto);
        }
    }
}
