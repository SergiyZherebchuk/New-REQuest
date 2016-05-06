using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRequest.Entities;
using NewRequest.DAL.Models;

namespace NewRequest.DAL.DAL
{
    public class StatisticDAL
    {
        private readonly TeamStatistic _teamStat;
        private readonly UserStatistic _userStat;
        private readonly NamesDAL _namesDal;

        public StatisticDAL()
        {
            this._teamStat = new TeamStatistic();
            this._userStat = new UserStatistic();
            this._namesDal = new NamesDAL();
        }

        public int GetLastPossition(int gameId, int level)
        {
            using(var context = new NewRequestEntities())
            {
                var max = 0;
                var res = from x in context.Statistics
                          where x.GameId == gameId && x.Level == level
                          select x;

                foreach(var l in res)
                {
                    if(l.Position > max)
                    {
                        max = l.Position;
                    }
                }

                return max + 1;
            }
        }

        public UserStatistic SelectLastUser(int gameId, int memberId)
        {
            using(var context = new NewRequestEntities())
            {
                List<Statistic> list = (from x in context.Statistics where x.GameId == gameId && x.MemberId == memberId select x).ToList();

                if (list.Count() > 0)
                {
                    var res = list.Last();

                    return new UserStatistic
                    {
                        Possition = res.Position,
                        UserId = res.MemberId,
                        User = _namesDal.GetUserLogin(res.MemberId),
                        Level = res.Level,
                        DateEnd = res.DateEnd
                    };
                }
                return null;                
            }
        }

        public TeamStatistic SelectLastTeam(int gameId, int memberId)
        {
            using (var context = new NewRequestEntities())
            {
                var res = from x in context.Statistics
                          where x.GameId == gameId && x.MemberId == memberId
                          select x;

                var last = (res.Last() != null) ? res.Last() : null;

                return new TeamStatistic
                {
                    Possition = last.Position,
                    TeamId = last.MemberId,
                    Team = _namesDal.GetTeamName(last.MemberId),
                    Level = last.Level,
                    DateEnd = last.DateEnd
                };
            }
        }

        public List<UserStatistic> SelectUserStat(int gameId)
        {
            using (var context = new NewRequestEntities())
            {
                List<UserStatistic> list = new List<UserStatistic>();
                var res = context.Statistics.OrderByDescending(x => x.Level).ThenByDescending(x => x.DateEnd).ToList();

                foreach(var stat in res)
                {
                    if (stat.GameId == gameId)
                    {
                        list.Add(new UserStatistic
                        {
                            Possition = stat.Position,
                            UserId = stat.MemberId,
                            User = _namesDal.GetUserLogin(stat.MemberId),
                            Level = stat.Level,
                            DateEnd = stat.DateEnd
                        });
                    }                    
                }

                return list;
            }
        }

        public List<TeamStatistic> SelectTeamStat(int gameId)
        {
            using (var context = new NewRequestEntities())
            {
                List<TeamStatistic> list = new List<TeamStatistic>();
                var res = context.Statistics.OrderByDescending(x => x.Level).ThenByDescending(x => x.DateEnd).ToList();
                

                foreach (var stat in res)
                {
                    if (stat.GameId == gameId)
                    {
                        list.Add(new TeamStatistic
                        {
                            Possition = stat.Position,
                            TeamId = stat.MemberId,
                            Team = _namesDal.GetTeamName(stat.MemberId),
                            Level = stat.Level,
                            DateEnd = stat.DateEnd
                        });
                    }
                }

                return list;
            }
        }

        public void AddNewStatistic(Statistic statistic)
        {
            using (var context = new NewRequestEntities())
            {
                if(IsValidStatistic(statistic.GameId, statistic.Level, statistic.MemberId))
                {
                    var res = from x in context.Statistics
                              where x.GameId == statistic.GameId && x.Level == statistic.Level
                              select x;

                    if(res.Count() != 0)
                    {
                        var last = res.Last();

                        statistic.Position = (last.Position + 1);
                    }
                    else
                    {
                        statistic.Position = 1;
                    }

                    context.Statistics.Add(statistic);
                    context.SaveChanges();
                }
            }
        }

        private bool IsValidStatistic(int gameId, int level, int memberId)
        {
            using (var context = new NewRequestEntities())
            {
                var res = from x in context.Statistics
                          where x.GameId == gameId && x.Level == level
                          && x.MemberId == memberId
                          select x;

                if(res.Count() == 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
