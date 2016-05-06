using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRequest.Entities;

namespace NewRequest.DAL.DAL
{
    public class AllLevelsDAL
    {
        public int SelectGameIdByLevel(int levelId)
        {
            using(var context = new NewRequestEntities())
            {
                var result = context.AllLevels.FirstOrDefault(x => x.Id == levelId);

                if(result == null)
                {
                    return 0;
                }
                return result.GameId;
            }
        }

        public int SelectLevelByNumber(int gameId)
        {
            using(var context = new NewRequestEntities())
            {
                var max = 0;
                var res = from x in context.AllLevels
                          where x.GameId == gameId
                          select x;
                foreach(var l in res)
                {
                    if(l.JNumber > max)
                    {
                        max = l.JNumber;
                    }
                }

                return max;
            }
        }

        public AllLevel SelectLevelById(int levelId)
        {
            using(var context = new NewRequestEntities())
            {
                var result = context.AllLevels.FirstOrDefault(x => x.Id == levelId);

                return result;
            }
        }

        public AllLevel SelectLevelByJNumber(int level, int gameId)
        {
            using(var context = new NewRequestEntities())
            {
                return context.AllLevels.FirstOrDefault(x => x.JNumber == level && x.GameId == gameId);
            }
        }

        public List<AllLevel> SelectAllLevByGame(int gameId)
        {
            using(var context = new NewRequestEntities())
            {
                var res = from x in context.AllLevels
                          where x.GameId == gameId
                          orderby x.JNumber ascending
                          select x;

                return res.ToList();
            }
        }

        public void AddNewLevel (AllLevel lev)
        {
            using (var context = new NewRequestEntities())
            {
                context.AllLevels.Add(lev);
                context.SaveChanges();
            }
        }

        public void UpdateLevel(AllLevel lev)
        {
            using (var context = new NewRequestEntities())
            {
                AllLevel thisLevel = context.Set<AllLevel>().First(u => u.Id == lev.Id);
                context.Entry(thisLevel).CurrentValues.SetValues(lev);
                context.SaveChanges();
            }
        }

        public void DeleteLevel(int id)
        {
            using (var context = new NewRequestEntities())
            {
                DeleteTask(id);
                DeleteTips(id);
                DeleteLevelType(id);
                DeleteCodes(id);

                var lev = context.AllLevels.Find(id);
                context.AllLevels.Remove(lev);
                context.SaveChanges();
            }
        }

        private void DeleteTask(int levelId)
        {
            using (var context = new NewRequestEntities())
            {
                var gameTask = context.GameTasks.FirstOrDefault(x => x.LevelId == levelId);
                context.GameTasks.Remove(gameTask);
                context.SaveChanges();
            }
        }

        private void DeleteTips(int levelId)
        {
            using(var context = new NewRequestEntities())
            {
                var res = (from x in context.GameTips
                           where x.LevelId == levelId
                           select x).ToList();

                foreach(var l in res)
                {
                    var gameTip = context.GameTips.Find(l.Id);
                    context.GameTips.Remove(gameTip);
                    context.SaveChanges();
                }
            }
        }

        private void DeleteLevelType(int levelId)
        {
            using(var context = new NewRequestEntities())
            {
                var levType = context.LevelTypes.FirstOrDefault(x => x.LevelId == levelId);
                context.LevelTypes.Remove(levType);
                context.SaveChanges();
            }
        }

        private void DeleteCodes(int levelId)
        {
            CodesDAL codesDal = new CodesDAL();

            codesDal.DeleteSectorCode(levelId);

        }
    }
}
