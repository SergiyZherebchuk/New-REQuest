using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewRequest.DAL.DAL;

namespace NewRequest.SPA.Controllers
{
    [RoutePrefix("api/[controller]")]
    public class AuthAPIController : ApiController
    {
        private readonly UsersDAL _userDal;

        public AuthAPIController()
        {
            this._userDal = new UsersDAL();
        }

        [HttpGet]
        public int Get()
        {
            string login = System.Web.HttpContext.Current.User.Identity.Name;
            
            if(login != "")
            {
                return _userDal.GetUserId(login);
            }
            return 0;
        }
    }
}
