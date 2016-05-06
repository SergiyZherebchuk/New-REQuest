using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRequest.Entities;

namespace NewRequest.DAL.DAL
{
    public class UsersDAL
    {
        public User GetUser(string login)
        {
            using (var context = new NewRequestEntities())
            {
                return context.Users.FirstOrDefault(x => x.Login == login);
            }
        }

        public int GetUserId(string login)
        {
            using (var context = new NewRequestEntities())
            {
                return context.Users.FirstOrDefault(x => x.Login == login).Id;
            }
        }

        public List<User> SelectUsers()
        {
            using (var context = new NewRequestEntities())
            {
                var res = from x in context.Users
                          select x;

                return res.ToList();
            }
        }

        public User SelectOneUser(int id)
        {
            using (var context = new NewRequestEntities())
            {
                return context.Users.Find(id);
            }
        }

        public void AddUser(User u)
        {
            if (u != null)
            {
                if (IsUserValid(u.Login))
                {
                    using (var context = new NewRequestEntities())
                    {
                        context.Users.Add(u);
                        context.SaveChanges();
                    }
                }
            }
        }

        public void UpdateUser(User user)
        {
            using (var context = new NewRequestEntities())
            {
                User thisUser = context.Set<User>().First(u => u.Id == user.Id);
                context.Entry(thisUser).CurrentValues.SetValues(user);
                context.SaveChanges();
            }
        }

        public bool IsUserValid(string login)
        {
            using (var context = new NewRequestEntities())
            {
                User us = context.Users.FirstOrDefault(u => u.Login == login);

                if (us == null)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsUserLoginValid(string login, string password)
        {
            using (var context = new NewRequestEntities())
            {
                User us = context.Users.FirstOrDefault(u => u.Login == login);

                if (us != null)
                {
                    if (us.Password.Equals(password))
                    {
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }
    }
}
