using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRequest.Entities;

namespace NewRequest.DAL.DAL
{
    public class CurrentSituationDAL
    {
        public CurrentSituation GetCurrentLevel(int memberId, int gameId)
        {
            using(var context = new NewRequestEntities())
            {
                var res = context.CurrentSituations.FirstOrDefault(x => x.MemberId == memberId && x.GameId == gameId);

                if(res != null)
                {
                    return res;
                }
                else
                {
                    return null;
                }
            }
        }

        public void AddNewCurrent(CurrentSituation cs)
        {
            using(var context = new NewRequestEntities())
            {
                if(isCurrentValid(cs.MemberId, cs.GameId, cs.Level))
                {
                    context.CurrentSituations.Add(cs);
                    context.SaveChanges();
                }
            }
        }

        public void UpdateCurrent (CurrentSituation cs)
        {
            using(var context = new NewRequestEntities())
            {
                CurrentSituation thisCs = context.Set<CurrentSituation>().First(u => u.Id == cs.Id);
                context.Entry(thisCs).CurrentValues.SetValues(cs);
                context.SaveChanges();
            }
        }

        private bool isCurrentValid(int memberId, int gameId, int level)
        {
            using(var context = new NewRequestEntities())
            {
                var res = from x in context.CurrentSituations
                          where x.GameId == gameId && x.MemberId == memberId && x.Level == level
                          select x;

                if (res.Count() == 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
