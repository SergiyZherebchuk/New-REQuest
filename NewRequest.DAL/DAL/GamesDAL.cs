using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRequest.Entities;

namespace NewRequest.DAL.DAL
{
    public class GamesDAL
    {
        public Game SelectOneGame(int id)
        {
            using (var context = new NewRequestEntities())
            {
                return context.Games.Find(id);
            }
        }

        public List<Game> SelectAllGame()
        {
            using (var context = new NewRequestEntities())
            {
                var res = from x in context.Games
                          select x;

                if(res.Count() != 0)
                {
                    return res.ToList();
                }
                return null;
            }
        }

        public List<Game> SelectGameFromOneAuthor(int userId)
        {
            using (var context = new NewRequestEntities())
            {
                var res = from x in context.Games
                          where x.AuthorId == userId && DateTime.Now < x.DateEnd
                          select x;

                return res.ToList();
            }
        }

        public List<Game> SelectActiveGames()
        {
            using (var context = new NewRequestEntities())
            {
                var res = from x in context.Games
                          where x.DateStart <= DateTime.UtcNow && DateTime.UtcNow <= x.DateEnd
                          orderby x.DateStart descending
                          select x;

                return res.ToList();
            }
        }

        public List<Game> SelectArchiveGames()
        {
            using (var context = new NewRequestEntities())
            {
                var res = from x in context.Games
                          where x.DateEnd < DateTime.UtcNow
                          orderby x.DateStart descending
                          select x;

                return res.ToList();
            }
        }

        public List<Game> SelectAnounceGames()
        {
            using (var context = new NewRequestEntities())
            {
                var res = from x in context.Games
                          where x.DateStart > DateTime.UtcNow
                          orderby x.DateStart descending
                          select x;

                return res.ToList();
            }
        }

        public void AddNewGame(Game game)
        {
            using (var context = new NewRequestEntities())
            {
                if (IsValidGame(game.Name))
                {
                    context.Games.Add(game);
                    context.SaveChanges();
                }
            }
        }

        public void UpdateGame(Game game)
        {
            using (var context = new NewRequestEntities())
            {
                Game thisgame = context.Set<Game>().First(u => u.Id == game.Id);
                context.Entry(thisgame).CurrentValues.SetValues(game);
                context.SaveChanges();
            }
        }

        public void DeleteGame(int id)
        {
            using (var context = new NewRequestEntities())
            {
                var game = context.Games.Find(id);
                context.Games.Remove(game);
                context.SaveChanges();
            }
        }

        private bool IsValidGame(string name)
        {
            using (var context = new NewRequestEntities())
            {
                var res = from x in context.Games
                          where x.Name == name
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
