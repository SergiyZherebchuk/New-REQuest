using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRequest.Entities;
using NewRequest.DAL.DAL;

namespace NewRequest.DAL.DAL
{
    public class LevelTypesDAL
    {
        private readonly NamesDAL _namesDAL;

        public LevelTypesDAL()
        {
            this._namesDAL = new NamesDAL();
        }

        public LevelType GetOneLevelType(int levelId)
        {
            using(var context = new NewRequestEntities())
            {
                var res = context.LevelTypes.FirstOrDefault(x => x.LevelId == levelId);

                return res;
            }
        }

        public string SelectLevelType(int levelId)
        {
            using (var context = new NewRequestEntities())
            {
                var res = context.LevelTypes.FirstOrDefault(x => x.LevelId == levelId);

                if(res.Type == 1)
                {
                    return "Code";
                }
                else if(res.Type == 2)
                {
                    return "Sectors";
                }
                else if (res.Type == 3)
                {
                    return "Olympian";
                }
                else if(res.Type == 4)
                {
                    return "Agent";
                }
                return null;
            }
        }

        public int GetLevelType(int levelId)
        {
            using (var context = new NewRequestEntities())
            {
                var result = context.LevelTypes.FirstOrDefault(x => x.LevelId == levelId);

                if(result != null)
                {
                    return result.Type;
                }
                return 0;
            }
        }

        public void AddNewLevelType(LevelType levType)
        {
            using (var context = new NewRequestEntities())
            {
                context.LevelTypes.Add(levType);
                context.SaveChanges();
            }
        }

        public void UpdateLevelType(LevelType levType)
        {
            using (var context = new NewRequestEntities())
            {
                LevelType thisType = context.Set<LevelType>().First(u => u.Id == levType.Id);
                context.Entry(thisType).CurrentValues.SetValues(levType);
                context.SaveChanges();
            }
        }

        public void DeleteLevelTypes(int id)
        {
            using (var context = new NewRequestEntities())
            {
                var levType = context.LevelTypes.Find(id);
                context.LevelTypes.Remove(levType);
                context.SaveChanges();
            }
        }
    }
}
