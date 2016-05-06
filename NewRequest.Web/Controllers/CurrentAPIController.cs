using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewRequest.Entities;
using NewRequest.DAL.DAL;
using NewRequest.DAL.Models;

namespace NewRequest.Web.Controllers
{
    [RoutePrefix("api/CurrentAPI")]
    public class CurrentAPIController : ApiController
    {
        private readonly CurrentSituationDAL _csDal;
        private readonly AllLevelsDAL _allLevelDal;
        private readonly CodesDAL _codesDal;

        public CurrentAPIController()
        {
            this._csDal = new CurrentSituationDAL();
            this._allLevelDal = new AllLevelsDAL();
            this._codesDal = new CodesDAL();
        }

        [HttpGet]
        [Route("{memberId}/{gameId}")]
        public CurrentSituation GetCurrent(int memberId, int gameId)
        {
            return _csDal.GetCurrentLevel(memberId, gameId);
        }

        [HttpGet]
        [Route("Level/{level}/{gameId}")]
        public AllLevel GetLevel(int level, int gameId)
        {
            return _allLevelDal.SelectLevelByJNumber(level, gameId);
        }

        [HttpGet]
        [Route("AllLevel/{gameId}")]
        public int GetAllLevel(int gameId)
        {
            return _allLevelDal.SelectLevelByNumber(gameId);
        }

        [HttpGet]
        [Route("MyCode/{levelId}/{memberId}")]
        public IList<MyCodes> SelectCode(int levelId, int memberId)
        {
            return _codesDal.SelectMyCodes(levelId, memberId);
        }

        [HttpPost]
        public HttpResponseMessage PostCurrent(CurrentSituation cs)
        {
            _csDal.AddNewCurrent(cs);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, cs);
            response.Headers.Location = new Uri(Url.Link("DefaultAPI", new { id = cs.Id }));

            return response;
        }

        [HttpPut]
        public void UpdateCS(CurrentSituation cs)
        {
            _csDal.UpdateCurrent(cs);
        }
    }
}
