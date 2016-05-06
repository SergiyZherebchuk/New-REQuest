using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using System.Threading;
using System.Security.Claims;
using NewRequest.Entities;
using NewRequest.DAL.DAL;

namespace NewRequest.SPA.Code
{
    public class SecurityService : ISecurityService
    {
        private readonly UsersDAL _userClass;

        public SecurityService(UsersDAL user)
        {
            this._userClass = user;
        }

        public bool Login(string login, string password)
        {
            if (this._userClass.IsUserLoginValid(login, password) == false)
            {
                return false;
            }

            FormsAuthentication.SetAuthCookie(login, false);

            IPrincipal principal = this.CreatePrincipal(login);
            Thread.CurrentPrincipal = principal;

            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }

            return true;
        }

        public bool Logout(string login)
        {
            FormsAuthentication.GetAuthCookie(login, false);
            FormsAuthentication.SignOut();
            return true;
        }

        public void RefreshPrincipal()
        {
            IPrincipal oldPrincipal = HttpContext.Current.User;
            if (oldPrincipal == null)
            {
                return;
            }

            IPrincipal principal = this.CreatePrincipal(oldPrincipal.Identity.Name);
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        private IPrincipal CreatePrincipal(string login)
        {
            User user = this._userClass.GetUser(login);
            ClaimsIdentity identity = new ClaimsIdentity("Password");
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Login));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.Surname));

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            return principal;
        }
    }
}