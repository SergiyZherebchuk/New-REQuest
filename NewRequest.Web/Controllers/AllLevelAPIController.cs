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
    [RoutePrefix("api/AllLevelAPI")]
    public class AllLevelAPIController : ApiController
    {
        private readonly AllLevelsDAL _allLevelDal;

        public AllLevelAPIController()
        {
            this._allLevelDal = new AllLevelsDAL();
        }

        [HttpGet]
        [Route("Level/{levelId}")]
        public int SelectGameId(int levelId)
        {
            return _allLevelDal.SelectGameIdByLevel(levelId);
        }

        [HttpGet]
        [Route("OneLevel/{levelId}")]
        public AllLevel SelectOneLevel(int levelId)
        {
            return _allLevelDal.SelectLevelById(levelId);
        }

        [HttpGet]
        [Route("Game/{gameId}")]
        public IList<AllLevel> SelectAllLevel(int gameId)
        {
            return _allLevelDal.SelectAllLevByGame(gameId);
        }

        [HttpPost]
        public HttpResponseMessage AddNewLevel(AllLevel level)
        {
            _allLevelDal.AddNewLevel(level);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, level);
            response.Headers.Location = new Uri(Url.Link("DefaultAPI", new { id = level.Id }));

            return response;
        }

        [HttpPut]
        public void UpdateLevel(AllLevel level)
        {
            _allLevelDal.UpdateLevel(level);
        }

        [HttpDelete]
        [Route("{levelId}")]
        public void DeleteLevel(int levelId)
        {
            _allLevelDal.DeleteLevel(levelId);
        }
    }
}
