using Core.IService;
using Core.StaticClass;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        public CustomBaseController()
        {
        }

        protected string GetLanguage(HttpRequest request)
        {
            var requestLangauge = request.Headers["language"].ToString();
            return string.IsNullOrEmpty(requestLangauge) ? LanguageList.BaseLanguage.UrlPrefix : requestLangauge;
        }
        public enum HttpStatusCode
        {
            NotFound = 404,
            BadRequest = 404,
            UnSporttedMediaType = 415,
            Created = 201,
            NoContent = 204,
            Ok = 200

        }
        public enum EntityStatus
        {
            Passive = 2,
            Active = 1,
            AwaitingResponseDiveCenter = 0
        }
        /// <summary>
        /// Api tarafındaki dönüş türümüzü custom response dto ile çevrelememizi sağlayan
        /// yapıddır.
        /// </summary>
        /// <typeparam name="T">Apı tarafından hangi datayı döneceğimizi buraya
        /// generic olarak belirtiriz.</typeparam>
        /// <param name="response">REsponse olarak da customresponsedto türünden
        /// bir dönüş değeri alır. içerisinde generic T nesnesi ile.</param>
        /// <returns></returns>
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204)
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
