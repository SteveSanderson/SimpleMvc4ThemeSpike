using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;

namespace VeryBasicThemingExample
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            
            DisplayModes.Modes.Clear();
            DisplayModes.Modes.Add(new ThemeDisplayMode("Red") { ContextCondition = ctx => ctx.CurrentTheme() == "Red" });
            DisplayModes.Modes.Add(new ThemeDisplayMode("Blue") { ContextCondition = ctx => ctx.CurrentTheme() == "Blue" });
            DisplayModes.Modes.Add(new DefaultDisplayMode(string.Empty));
        }
    }

    public class ThemeDisplayMode : DefaultDisplayMode
    {
        public ThemeDisplayMode(string suffix) : base(suffix) {
        }

        protected override string TransformPath(string virtualPath, string suffix)
        {
            if (virtualPath.StartsWith("~/Views/") && !string.IsNullOrEmpty(suffix))
                return "~/Views/Themed/" + suffix + virtualPath.Substring("~/Views".Length);
            return virtualPath;
        }
    }

    public static class Themes
    {
        public static string CurrentTheme(this HttpContextBase httpContext)
        {
            var cookie = httpContext.Request.Cookies["theme"];
            return cookie != null ? cookie.Value : null;
        }
    }
}