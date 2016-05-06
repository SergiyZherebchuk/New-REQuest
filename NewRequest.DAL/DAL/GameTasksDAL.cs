using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRequest.Entities;

namespace NewRequest.DAL.DAL
{
    public class GameTasksDAL
    {
        public GameTask SelectTask(int levelId)
        {
            using (var context = new NewRequestEntities())
            {
                return context.GameTasks.FirstOrDefault(x => x.LevelId == levelId);
            }
        }

        public void AddNewTask(GameTask gameTask)
        {
            using (var context = new NewRequestEntities())
            {
                context.GameTasks.Add(gameTask);
                context.SaveChanges();
            }
        }

        public void UpdateTask(GameTask gameTask)
        {
            using (var context = new NewRequestEntities())
            {
                GameTask thisTask = context.Set<GameTask>().First(u => u.Id == gameTask.Id);
                context.Entry(thisTask).CurrentValues.SetValues(gameTask);
                context.SaveChanges();
            }
        }

        public void DeleteTask(int taskId)
        {
            using (var context = new NewRequestEntities())
            {
                var gameTask = context.GameTasks.Find(taskId);
                context.GameTasks.Remove(gameTask);
                context.SaveChanges();
            }
        }
    }
}
