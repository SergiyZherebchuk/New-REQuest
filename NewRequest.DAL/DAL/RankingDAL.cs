using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRequest.Entities;
using NewRequest.DAL.Models;

namespace NewRequest.DAL.DAL
{
    public class RankingDAL
    {
        private readonly NamesDAL _namesDal;

        public RankingDAL()
        {
            this._namesDal = new NamesDAL();
        }

        public Ranking SelectOneUser(int userId)
        {
            using (var context = new NewRequestEntities())
            {
                return context.Rankings.FirstOrDefault(u => u.UserId == userId);
            }
        }

        public List<UserRank> SelectTop10Points()
        {
            using (var context = new NewRequestEntities())
            {
                int i = 1;
                List<UserRank> list = new List<UserRank>();
                var res = from x in context.Rankings
                          orderby x.Point descending
                          select x;

                foreach(var x in res)
                {
                    if(i<11)
                    {
                        list.Add(new UserRank
                        {
                            Rank = i,
                            Name = _namesDal.GetUserLogin(x.UserId),
                            Points = x.Point
                        });

                        i++;
                    }
                    else
                    {
                        return list;
                    }                    
                }
                return list;
            }
        }

        public List<UserRank> SelectTop10TeamPoints()
        {
            using (var context = new NewRequestEntities())
            {
                int i = 1;
                List<UserRank> list = new List<UserRank>();
                var res = from x in context.Rankings
                          orderby x.TeamPoint descending
                          select x;

                foreach (var x in res)
                {
                    if (i < 11)
                    {
                        list.Add(new UserRank
                        {
                            Rank = i,
                            Name = _namesDal.GetUserLogin(x.UserId),
                            TeamPoint = x.TeamPoint
                        });

                        i++;
                    }
                    else
                    {
                        return list;
                    }
                }
                return list;
            }
        }

        public List<UserRank> SelectPointsSort()
        {
            using (var context = new NewRequestEntities())
            {
                int i = 1;
                List<UserRank> list = new List<UserRank>();
                var res = from x in context.Rankings
                          orderby x.Point descending
                          select x;

                foreach (var x in res)
                {
                    list.Add(new UserRank
                    {
                        Rank = i,
                        Name = _namesDal.GetUserLogin(x.UserId),
                        Points = x.Point
                    });

                    i++;
                }
                return list;
            }
        }

        public List<UserRank> SelectTeamPointsSort()
        {
            using (var context = new NewRequestEntities())
            {
                int i = 1;
                List<UserRank> list = new List<UserRank>();
                var res = from x in context.Rankings
                          orderby x.TeamPoint descending
                          select x;

                foreach (var x in res)
                {
                    list.Add(new UserRank
                    {
                        Rank = i,
                        Name = _namesDal.GetUserLogin(x.UserId),
                        TeamPoint = x.TeamPoint
                    });

                    i++;
                }
                return list;
            }
        }

        public void AddNewUserRank(Ranking usRank)
        {
            using (var context = new NewRequestEntities())
            {


                if(IsUserRankValid(usRank.UserId))
                {
                    context.Rankings.Add(usRank);
                    context.SaveChanges();
                }
            }
        }

        public void UpdatePointInTeam(int userId, int point, int teamPoint)
        {
            Ranking rank = SelectOneUser(userId);

            if (rank.Point == null || rank.Point == 0)
            {
                rank.Point = point;
            }
            else
            {
                rank.Point += point;
            }

            if (rank.TeamPoint == null || rank.TeamPoint == 0)
            {
                rank.TeamPoint = teamPoint;
            }
            else
            {
                rank.TeamPoint += teamPoint;
            }

            UpdateUserRank(rank);
        }

        public void UpdateUserRank(Ranking usRank)
        {
            using (var context = new NewRequestEntities())
            {
                Ranking thisRank = context.Set<Ranking>().FirstOrDefault(u => u.UserId == usRank.UserId);
                context.Entry(thisRank).CurrentValues.SetValues(usRank);
                context.SaveChanges();
            }
        }

        private bool IsUserRankValid(int userId)
        {
            using (var context = new NewRequestEntities())
            {
                var res = context.Rankings.FirstOrDefault(u => u.UserId == userId);

                if (res == null)
                {
                    return true;
                }
                return false;
            }
        }        
    }
}
