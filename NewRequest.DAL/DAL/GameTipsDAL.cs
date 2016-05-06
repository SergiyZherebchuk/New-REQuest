using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRequest.Entities;
using NewRequest.DAL.Models;

namespace NewRequest.DAL.DAL
{
    public class GameTipsDAL
    {
        public GameTip SelectOneTip(int tipId)
        {
            using (var context = new NewRequestEntities())
            {
                return context.GameTips.Find(tipId);
            }
        }

        public List<GameTip> SelectOneGameTips(int levelId)
        {
            using (var context = new NewRequestEntities())
            {
                var res = from x in context.GameTips
                          where x.LevelId == levelId
                          orderby x.Times ascending
                          select x;

                return res.ToList();
            }
        }

        public List<TipsTimer> SelectOneLevelTips(int levelId)
        {
            using(var context = new NewRequestEntities())
            {
                List<TipsTimer> list = new List<TipsTimer>();
                var res = from x in context.GameTips
                          where x.LevelId == levelId
                          select x;

                foreach(var l in res)
                {
                    list.Add(new TipsTimer
                    {
                        Id = l.Id,
                        Times = l.Times
                    });
                }
                return list;
            }
        } // Select tips for timer!!!

        public void AddNewTip(GameTip gameTip)
        {
            using (var context = new NewRequestEntities())
            {
                context.GameTips.Add(gameTip);
                context.SaveChanges();
            }
        }

        public void UpdateTip(GameTip gameTip)
        {
            using (var context = new NewRequestEntities())
            {
                GameTip thisTip = context.Set<GameTip>().First(u => u.Id == gameTip.Id);
                context.Entry(thisTip).CurrentValues.SetValues(gameTip);
                context.SaveChanges();
            }
        }

        public void DeleteTip(int tipId)
        {
            using (var context = new NewRequestEntities())
            {
                var gameTip = context.GameTips.Find(tipId);
                context.GameTips.Remove(gameTip);
                context.SaveChanges();
            }
        }
    }
}
