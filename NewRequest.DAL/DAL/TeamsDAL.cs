using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRequest.Entities;
using NewRequest.DAL.Models;

namespace NewRequest.DAL.DAL
{
    public class TeamsDAL
    {
        private readonly UserTeamStatusDAL _usTeamStatDal;
        private readonly NamesDAL _namesDal;

        public TeamsDAL()
        {
            this._usTeamStatDal = new UserTeamStatusDAL();
            this._namesDal = new NamesDAL();
        }

        public Team SelectLastTeam(int userId)
        {
            using (var context = new NewRequestEntities())
            {
                var res = context.UserTeamStatus.FirstOrDefault(u => u.UserId == userId && u.StatusId != 5);

                if (res != null)
                {
                    return SelectOneTeam(res.TeamId);
                }
                return null;
            }
        }

        public Team SelectOneTeam(int teamId)
        {
            using (var context = new NewRequestEntities())
            {
                return context.Teams.FirstOrDefault(x => x.Id == teamId);
            }
        }

        public List<AllTeam> SelectAllTeams()
        {
            using (var context = new NewRequestEntities())
            {
                List<AllTeam> list = new List<AllTeam>();

                var res = from x in context.Teams
                          select x;

                foreach(var x in res)
                {

                    list.Add(new AllTeam
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CaptainId = x.CaptainId,
                        Captain = _namesDal.GetUserLogin(x.CaptainId)
                    });
                }

                return list;
            }
        }

        public List<TeamRank> SelectTop10()
        {
            using(var context = new NewRequestEntities())
            {
                int i = 1;
                List<TeamRank> res = new List<TeamRank>();
                var result = from x in context.Teams
                          orderby x.Points descending
                          select x;

                foreach(var x in result)
                {
                    if(i<11)
                    {
                        res.Add(new TeamRank {
                            Id = x.Id,
                            Rank = i,
                            Name = x.Name,
                            Points = x.Points
                        });

                        i++;
                    }
                    else
                    {
                        return res;
                    }
                }
                return res;
            }
        }

        public List<TeamRank> SelectSort()
        {
            using (var context = new NewRequestEntities())
            {
                int i = 1;
                List<TeamRank> res = new List<TeamRank>();
                var result = from x in context.Teams
                          orderby x.Points descending
                          select x;

                foreach (var x in result)
                {
                    res.Add(new TeamRank
                    {
                        Id = x.Id,
                        Rank = i,
                        Name = x.Name,
                        Points = x.Points
                    });
                    i++;
                }
                return res;
            }
        }

        public void AddNewTeam(Team team)
        {
            using (var context = new NewRequestEntities())
            {
                if (IsTeamNameValid(team.Name))
                {
                    context.Teams.Add(team);
                    context.SaveChanges();
                }
            }
        }

        public void UpdateTeam(Team team)
        {
            using (var context = new NewRequestEntities())
            {
                Team thisteam = context.Set<Team>().First(u => u.Id == team.Id);
                context.Entry(thisteam).CurrentValues.SetValues(team);
                context.SaveChanges();
            }
        }

        public void DeleteTeam(int id)
        {
            using (var context = new NewRequestEntities())
            {
                Team team = context.Teams.Find(id);
                context.Teams.Remove(team);
                context.SaveChanges();
            }
        }

        private bool IsTeamNameValid(string name)
        {
            using (var context = new NewRequestEntities())
            {
                Team us = context.Teams.FirstOrDefault(t => t.Name == name);

                if (us == null)
                {
                    return true;
                }
            }
            return false;
        }

        public void UpdatePointInTeam(int teamId, int teamPoint)
        {
            Team team = SelectOneTeam(teamId);

            if (team.Points == null || team.Points == 0)
            {
                team.Points = teamPoint;
            }
            else
            {
                team.Points += teamPoint;
            }

            UpdateTeam(team);
        }
    }
}
