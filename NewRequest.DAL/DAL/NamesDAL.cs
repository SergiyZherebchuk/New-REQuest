using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRequest.Entities;

namespace NewRequest.DAL.DAL
{
    public class NamesDAL
    {
        public string GetUserStatus(int id)
        {
            using (var context = new NewRequestEntities())
            {
                return context.UserStatus.Find(id).Status;
            }
        }

        public string GetUserType(int id)
        {
            using (var context = new NewRequestEntities())
            {
                return context.UserTypes.Find(id).Type;
            }
        }

        public string GetUserLogin(int id)
        {
            using (var context = new NewRequestEntities())
            {
                return context.Users.Find(id).Login;
            }
        }

        public string GetGameName(int id)
        {
            using (var context = new NewRequestEntities())
            {
                return context.Games.Find(id).Name;
            }
        }

        public string GetLevelName(int id)
        {
            using (var context = new NewRequestEntities())
            {
                return context.AllLevels.Find(id).Name;
            }
        }

        public string GetTeamName(int id)
        {
            using (var context = new NewRequestEntities())
            {
                return context.Teams.Find(id).Name;
            }
        }
    }
}
