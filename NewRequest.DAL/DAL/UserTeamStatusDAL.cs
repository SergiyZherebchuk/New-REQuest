using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRequest.Entities;
using NewRequest.DAL.Models;

namespace NewRequest.DAL.DAL
{
    public class UserTeamStatusDAL
    {
        private readonly NamesDAL _namesDal;

        public UserTeamStatusDAL()
        {
            this._namesDal = new NamesDAL();
        }

        public UserTeamStatu SelectOneUser(int userId)
        {
            using (var context = new NewRequestEntities())
            {
                var res = context.UserTeamStatus.FirstOrDefault(x => x.UserId == userId);

                return res;
            }
        }

        public TeamUser SelectCaptainUser(int teamId)
        {
            using (var context = new NewRequestEntities())
            {
                var res = context.UserTeamStatus.FirstOrDefault(u => u.StatusId == 1 && u.TeamId == teamId);
                var point = context.Rankings.FirstOrDefault(x => x.UserId == res.UserId);

                TeamUser user = new TeamUser
                {
                    Id = res.UserId,
                    Name = _namesDal.GetUserLogin(res.UserId),
                    Point = point != null ? point.Point : 0
                };

                return user;
            }
        }

        public List<TeamUser> SelectActiveUser(int teamId)
        {
            using (var context = new NewRequestEntities())
            {
                List<TeamUser> list = new List<TeamUser>();
                var res = from x in context.UserTeamStatus
                          where x.TeamId == teamId && x.StatusId == 2
                          select x;

                foreach(var x in res)
                {
                    var rnk = context.Rankings.FirstOrDefault(u => u.UserId == x.UserId);

                    list.Add(new TeamUser
                    {
                        Id = x.Id,
                        Name = _namesDal.GetUserLogin(x.Id),
                        Point = rnk != null ? rnk.Point : 0
                    });
                }

                return list;
            }
        }

        public List<TeamUser> SelectSpareUser(int teamId)
        {
            using (var context = new NewRequestEntities())
            {
                List<TeamUser> list = new List<TeamUser>();
                var res = from x in context.UserTeamStatus
                          where x.TeamId == teamId && x.StatusId == 3
                          select x;

                foreach (var x in res)
                {
                    var rnk = context.Rankings.FirstOrDefault(u => u.UserId == x.UserId);

                    list.Add(new TeamUser
                    {
                        Id = x.Id,
                        Name = _namesDal.GetUserLogin(x.Id),
                        Point = rnk != null ? rnk.Point : 0
                    });
                }

                return list;
            }
        }

        public List<TeamUser> SelectJoinUser(int teamId)
        {
            using (var context = new NewRequestEntities())
            {
                List<TeamUser> list = new List<TeamUser>();
                var res = from x in context.UserTeamStatus
                          where x.TeamId == teamId && x.StatusId == 4
                          select x;

                foreach (var x in res)
                {
                    var rnk = context.Rankings.FirstOrDefault(u => u.UserId == x.UserId);

                    list.Add(new TeamUser
                    {
                        Id = x.Id,
                        Name = _namesDal.GetUserLogin(x.Id),
                        Point = rnk != null ? rnk.Point : 0
                    });
                }

                return list;
            }
        }

        public void AddNewStatus(UserTeamStatu usTeamStatus)
        {
            using (var context = new NewRequestEntities())
            {
                if(IsTeamStatusValid(usTeamStatus.UserId))
                {
                    context.UserTeamStatus.Add(usTeamStatus);
                    context.SaveChanges();
                }
            }
        }

        public void UpdateNewStatus(UserTeamStatu usTeamStatus)
        {
            using (var context = new NewRequestEntities())
            {
                UserTeamStatu thisTeamStatus = context.Set<UserTeamStatu>().FirstOrDefault(u => u.Id == usTeamStatus.Id);
                context.Entry(thisTeamStatus).CurrentValues.SetValues(usTeamStatus);
                context.SaveChanges();
            }
        }

        private bool IsTeamStatusValid(int userId)
        {
            using (var context = new NewRequestEntities())
            {
                if(context.UserTeamStatus.FirstOrDefault(u=>u.UserId == userId) == null)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
