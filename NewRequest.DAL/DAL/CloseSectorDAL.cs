using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRequest.Entities;

namespace NewRequest.DAL.DAL
{
    public class CloseSectorDAL
    {
        public List<CloseSector> SelectCloseLevels(int gameId, int level, int memberId)
        {
            using (var context = new NewRequestEntities())
            {
                var res = from x in context.CloseSectors
                          where x.GameId == gameId && x.Level == level && x.MemberId == memberId
                          select x;

                return res.ToList();
            }
        }

        public int GetCloseSector(int gameId, int level, int memberId)
        {
            using(var context = new NewRequestEntities())
            {
                var res = context.CloseSectors.LastOrDefault(x => x.GameId == gameId 
                                                             && x.Level == level 
                                                             && x.MemberId == memberId);

                return res.SectorPoss;
            }
        }

        public void AddNewCloseSector(CloseSector closeSector)
        {
            using (var context = new NewRequestEntities())
            {
                if(IsValidCloseSector(closeSector))
                {
                    var res = from x in context.CloseSectors
                              where x.GameId == closeSector.GameId && x.Level == closeSector.Level
                              && x.MemberId == closeSector.MemberId
                              select x;

                    var poss = 0;

                    foreach(var x in res)
                    {
                        if(x.SectorPoss > poss)
                        {
                            poss = x.SectorPoss;
                        }
                    }

                    closeSector.SectorPoss = poss + 1;

                    context.CloseSectors.Add(closeSector);
                    context.SaveChanges();
                }
            }
        }

        public void DeleteSector(int gameId, int levelId, int memberId)
        {
            using (var context = new NewRequestEntities())
            {
                var res = from x in context.CloseSectors
                          where x.GameId == gameId && x.Level == levelId && x.MemberId == memberId
                          select x;

                foreach(var x in res)
                {
                    var close = context.CloseSectors.Find(x.Id);
                    context.CloseSectors.Remove(close);
                    context.SaveChanges();
                }
            }
        }

        private bool IsValidCloseSector(CloseSector sector)
        {
            using (var context = new NewRequestEntities())
            {
                var res = (from x in context.CloseSectors
                          where x.Level == sector.Level && x.MemberId == sector.MemberId && x.Sector == sector.Sector
                          && x.GameId == sector.GameId && x.SectorPoss == sector.SectorPoss
                          select x).ToList();

                if(res.Count() == 0)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
