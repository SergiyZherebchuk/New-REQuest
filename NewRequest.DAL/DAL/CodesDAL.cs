using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRequest.Entities;
using NewRequest.DAL.Models;

namespace NewRequest.DAL.DAL
{
    public class CodesDAL
    {
        public List<Code> SelectSectorCodes(int levelId)
        {
            using (var context = new NewRequestEntities())
            {
                var res = from x in context.Codes
                          where x.LevelId == levelId
                          orderby x.Sector ascending
                          select x;

                return res.ToList();
            }
        }

        public List<MyCodes> SelectMyCodes(int levelId, int memberId)
        {
            using (var context = new NewRequestEntities())
            {
                List<MyCodes> list = new List<MyCodes>();
                var listCount = list.Count;
                var res = SelectSectorCodes(levelId);
                var level = this.GetLevel(levelId);

                var closeCode = (from x in context.CloseSectors
                                where x.Level == level && x.MemberId == memberId
                                select x).ToList();

                if (res != null)
                {
                    foreach (var code in res)
                    {
                        if (closeCode.Count != 0)
                        {
                            foreach (var cls in closeCode)
                            {
                                if (code.Sector == cls.Sector)
                                {
                                    list.Add(new MyCodes
                                    {
                                        Id = code.Id,
                                        LevelId = code.LevelId,
                                        Sector = code.Sector,
                                        Name = code.Name,
                                        Code1 = code.Code1
                                    });
                                }
                            }
                        }

                        if ((list.Count == listCount))
                        {
                            list.Add(new MyCodes
                            {
                                Id = code.Id,
                                LevelId = code.LevelId,
                                Sector = code.Sector,
                                Name = code.Name,
                                Code1 = null
                            });
                        }
                        listCount = list.Count;
                    }
                }


                return list;
            }
        }

        public Code SelectOneCode(int levelId)
        {
            using (var context = new NewRequestEntities())
            {
                var res = context.Codes.FirstOrDefault(x => x.LevelId == levelId);

                return res;
            }
        }
        
        public void AddNewCode(Code code)
        {
            using (var context = new NewRequestEntities())
            {
                context.Codes.Add(code);
                context.SaveChanges();
            }
        }

        public void AddSectorCode(List<Code> listCodes)
        {
            using (var context = new NewRequestEntities())
            {
                foreach(var code in listCodes)
                {
                    context.Codes.Add(code);
                    context.SaveChanges();
                }
            }
        }

        public bool IsTrueCode(int levelId, string code)
        {
            using (var context = new NewRequestEntities())
            {
                if(context.Codes.FirstOrDefault(u=>u.LevelId == levelId && u.Code1 == code) != null)
                {
                    return true;
                }

                return false;
            }
        }

        public CloseCode IsSectorTrueCode (int levelId, string code)
        {
            using(var context = new NewRequestEntities())
            {
                var tmp = context.Codes.FirstOrDefault(x => x.LevelId == levelId && x.Code1 == code);

                if(tmp != null)
                {
                    return new CloseCode
                    {
                        Sector = tmp.Sector,
                        IsTrue = true
                    };
                }
                return new CloseCode
                {
                    IsTrue = false
                };
            }
        }

        public void UpdateCode(Code code)
        {
            using (var context = new NewRequestEntities())
            {
                Code thisCode = context.Set<Code>().FirstOrDefault(u => u.Id == code.Id);
                context.Entry(thisCode).CurrentValues.SetValues(code);
                context.SaveChanges();
            }
        }

        public void UpdateSectorCodes(List<Code> listCodes)
        {
            using (var context = new NewRequestEntities())
            {
                List<Code> list = new List<Code>();

                foreach(var code in listCodes)
                {
                    if(context.Codes.FirstOrDefault(x => x.Id == code.Id) == null)
                    {
                        list.Add(code);
                    }
                    else
                    {
                        Code thisCode = context.Set<Code>().FirstOrDefault(u => u.Id == code.Id);
                        context.Entry(thisCode).CurrentValues.SetValues(code);
                        context.SaveChanges();
                    }
                }

                AddSectorCode(list);
            }
        }

        public void DeleteCodes(int codeId)
        {
            using (var context = new NewRequestEntities())
            {
                var code = context.Codes.Find(codeId);
                context.Codes.Remove(code);
                context.SaveChanges();
            }
        }

        public void DeleteSectorCode(int levelId)
        {
            using (var context = new NewRequestEntities())
            {
                var result = (from x in context.Codes
                             where x.LevelId == levelId
                             select x).ToList();

                foreach (var code in result)
                {
                    var myCode = context.Codes.Find(code.Id);
                    context.Codes.Remove(myCode);
                    context.SaveChanges();
                }

            }
        }

        private int GetLevel(int levelId)
        {
            using(var context = new NewRequestEntities())
            {
                var res = context.AllLevels.FirstOrDefault(x => x.Id == levelId);

                if(res != null)
                {
                    return res.JNumber;
                }
                return 0;
            }
        }
    }
}
