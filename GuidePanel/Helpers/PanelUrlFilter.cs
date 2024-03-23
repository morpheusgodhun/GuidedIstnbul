using Microsoft.AspNetCore.Mvc.Filters;

namespace GuidePanel.Helpers
{
    public class PanelUrlFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // URL'yi özelleştirme kodunu burada yazın.
            // Örnek olarak, tüm URL'lerin başına "/prefix" ekleyelim.
            var request = context.HttpContext.Request;
            var originalPath = request.Path;
            request.Path = "/panel" + originalPath;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Gerektiğinde işlem sonrası işlemleri yapabilirsiniz.
        }
    }

    /*options =>
{
    options.Filters.Add(new PanelUrlFilter());
}*/
}
