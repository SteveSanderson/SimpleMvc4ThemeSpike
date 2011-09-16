using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VeryBasicThemingExample.Controllers
{
    public class ThemePickerController : Controller
    {
        public ActionResult ChooseTheme(string theme, string returnUrl) {
            Response.Cookies.Add(new HttpCookie("theme", theme));
            return Redirect(returnUrl);
        }
    }
}
