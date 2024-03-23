using Dto.ApiWebDtos.ApiToWebDtos.Faq;
using Dto.ApiWebDtos.ApiToWebDtos.Layout;
using DTO;
using GuideWeb.APIHandler;
using Microsoft.AspNetCore.Mvc;

namespace GuideWeb.Controllers
{
    public class LayoutController : Controller
    {

        public PartialViewResult _HeadPartial()
        {
            return PartialView();
        }
        public PartialViewResult _PreloaderPartial()
        {
            return PartialView();
        }
        public PartialViewResult _TopbarPartial()
        {
            return PartialView();
        }
        public PartialViewResult _FooterPartial()
        {
            return PartialView();
        }
        public PartialViewResult _ConstantObjectPartial()
        {
            return PartialView();
        }
        public PartialViewResult _ScriptPartial()
        {
            return PartialView();
        }
    }
}
