using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using NewRequest.DAL.DAL;
using NewRequest.SPA.Code;
using NewRequest.Entities;

namespace NewRequest.SPA.Controllers
{
    public class AuthController : Controller
    {
        private readonly ISecurityService _securityService;

        public AuthController()
        {
            this._securityService = new SecurityService(new UsersDAL());
        }

        [HttpGet]
        public ActionResult Index()
        {
            if(System.Web.HttpContext.Current.User.Identity.Name == "")
            {
                return View();
            }
            return Redirect("~/#/");
        }

        public ActionResult Logout()
        {
            if (this._securityService.Logout(HttpContext.User.Identity.Name))
            {
                return Redirect("~/login");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(string login, string password)
        {
            if (this._securityService.Login(login, password) == true)
            {
                return Redirect("~/#/");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult AuthStatePartial()
        {
            ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
            string firstName = principal.Claims.Where(r => r.Type == ClaimTypes.GivenName).ToString();

            return null;
        }
    }
}