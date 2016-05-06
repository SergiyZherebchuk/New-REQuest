using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewRequest.DAL.DAL;
using NewRequest.Entities;

namespace NewRequest.Web.Controllers
{
    [RoutePrefix("api/GamesAPI")]
    public class GamesAPIController : ApiController
    {
        private readonly GamesDAL _gamesDal;

        public GamesAPIController()
        {
            this._gamesDal = new GamesDAL();
        }

        [HttpGet]
        public List<Game> SelectAll()
        {
            return _gamesDal.SelectAllGame();
        }

        [HttpGet]
        [Route("{gameId}")]
        public Game SelectOneGame(int gameId)
        {
            return _gamesDal.SelectOneGame(gameId);
        }

        [HttpGet]
        [Route("Author/{authorId}")]
        public List<Game> SelectOneAuthor(int authorId)
        {
            return _gamesDal.SelectGameFromOneAuthor(authorId);
        }

        [HttpGet]
        [Route("Active")]
        public List<Game> SelectActiveGames()
        {
            return _gamesDal.SelectActiveGames();
        }

        [HttpGet]
        [Route("Archive")]
        public List<Game> SelectArchiveGames()
        {
            return _gamesDal.SelectArchiveGames();
        }

        [HttpGet]
        [Route("Anounce")]
        public List<Game> SelectAnounceGames()
        {
            return _gamesDal.SelectAnounceGames();
        }

        [HttpPost]
        public HttpResponseMessage AddNewGame(Game game)
        {
            _gamesDal.AddNewGame(game);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, game);
            response.Headers.Location = new Uri(Url.Link("DefaultAPI", new { id = game.Id }));

            return response;
        }

        [HttpPut]
        public void UpdateGame(Game game)
        {
            _gamesDal.UpdateGame(game);
        }

        [HttpDelete]
        [Route("{gameId}")]
        public void DeleteGame(int gameId)
        {
            _gamesDal.DeleteGame(gameId);
        }
    }
}
