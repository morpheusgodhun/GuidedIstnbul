using GuidePanel.APIHandler;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    public class CustomBaseController : Controller
    {
        protected readonly IApiHandler _apiHandler;
        protected readonly IConfiguration _configuration;
        protected readonly string _url;

        public CustomBaseController(IApiHandler apiHandler, IConfiguration configuration)
        {
            _apiHandler = apiHandler;
            _configuration = configuration;
            _url = this._configuration["BaseUrl"];
        }


    }
}

