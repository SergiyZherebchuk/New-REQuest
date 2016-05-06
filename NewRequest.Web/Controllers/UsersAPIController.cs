using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewRequest.DAL.DAL;
using NewRequest.Entities;

namespace NewRequest.Web.Controllers
{
    public class UsersAPIController : ApiController
    {
        private readonly UsersDAL _usersDal;

        public UsersAPIController()
        {
            this._usersDal = new UsersDAL();
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _usersDal.SelectUsers();
        }

        [HttpGet]
        public User GetUser(int id)
        {
            return _usersDal.SelectOneUser(id);
        }

        [HttpGet]
        public User GetUserByLogin(string myLogin)
        {
            return _usersDal.GetUser(myLogin);
        }

        [HttpGet]
        public bool IsLoginTrue(string login, string pass)
        {
            return _usersDal.IsUserLoginValid(login, pass);
        }

        [HttpPost]
        public void PostUser(User user)
        {
            _usersDal.AddUser(user);
        }

        [HttpPut]
        public void PostUser(int id, User user)
        {
            if (id == user.Id)
            {
                _usersDal.UpdateUser(user);
            }
        }
    }
}
