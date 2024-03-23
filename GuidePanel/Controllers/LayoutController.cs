using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    public class LayoutController : Controller
    {
        readonly IHttpContextAccessor _httpContextAccessor;

        public LayoutController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public PartialViewResult HeadPartial()
        {
            return PartialView();
        }
        public PartialViewResult TopbarPartial()
        {
            return PartialView();
        }
        public PartialViewResult LeftSidebarPartial()
        {
            return PartialView();
        }

        public PartialViewResult RightSidebarPartial()
        {
            return PartialView();
        }

        public PartialViewResult FooterPartial()
        {
            return PartialView();
        }

        public PartialViewResult ScriptPartial()
        {

            return PartialView();
        }
    }
}
