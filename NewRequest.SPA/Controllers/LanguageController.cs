using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace NewRequest.SPA.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Index()
        {
            return View();
        }

        /*
        public ActionResult Change(String LangAbbrevation)
        {
            if(LangAbbrevation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangAbbrevation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangAbbrevation);
            }

            HttpCookie cookie = new HttpCookie("lang");
            cookie.Value = LangAbbrevation;
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home");
        }*/
    }
}