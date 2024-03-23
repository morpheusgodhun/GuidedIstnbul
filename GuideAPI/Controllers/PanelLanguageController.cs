
using Core.StaticClass;
using Dto.ApiPanelDtos.LanguageDtos;
using DTO;
using Microsoft.AspNetCore.Mvc;


namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelLanguageController : ControllerBase
    {


        [HttpGet]
        public CustomResponseDto<List<LanguageListDto>> LanguageList()
        {
            var languageListDtos = (from language in Core.StaticClass.LanguageList.AllLanguages
                                    select new LanguageListDto
                                       {
                                           LanguageID = language.Id,
                                           LanguageName = language.Name
                                       }).ToList<LanguageListDto>();

            return CustomResponseDto<List<LanguageListDto>>.Success(200, languageListDtos);
        }
     }
}
