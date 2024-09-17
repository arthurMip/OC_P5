using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpressVoitures.Extensions
{
    public static class HtmlExtensions
    {
        public static string IsActive(this IHtmlHelper html, string control, string action)
        {
            var routeData = html.ViewContext.RouteData;
            var routeAction = routeData?.Values["action"]?.ToString();
            var routeControl = routeData?.Values["controller"]?.ToString();

            var isActive = control == routeControl && action == routeAction;

            return isActive ? "active" : "";
        }
    }
}
