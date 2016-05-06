using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewRequest.DAL.DAL;
using NewRequest.DAL.Models;
using NewRequest.Entities;

namespace NewRequest.Web.Controllers
{
    [RoutePrefix("api/GameTips")]
    public class GameTipsAPIController : ApiController
    {
        private readonly GameTipsDAL _gameTipsDal;

        public GameTipsAPIController()
        {
            this._gameTipsDal = new GameTipsDAL();
        }

        [HttpGet]
        public GameTip GetOneTip(int tipId)
        {
            return _gameTipsDal.SelectOneTip(tipId);
        }

        [HttpGet]
        public List<GameTip> GetLevelTips(int levelId)
        {
            return _gameTipsDal.SelectOneGameTips(levelId);
        }

        [HttpGet]
        [Route("Timer/{levelId}")]
        public List<TipsTimer> GetTimerTips(int levelId)
        {
            return _gameTipsDal.SelectOneLevelTips(levelId);
        }

        [HttpPost]
        public HttpResponseMessage AddNewTip(GameTip tip)
        {
            _gameTipsDal.AddNewTip(tip);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tip);
            response.Headers.Location = new Uri(Url.Link("DefaultAPI", new { id = tip.Id }));

            return response;
        }

        [HttpPut]
        public void UpdateTip(GameTip tip)
        {
            _gameTipsDal.UpdateTip(tip);
        }

        [HttpDelete]
        [Route("{tipId}")]
        public void DeleteTip(int tipId)
        {
            _gameTipsDal.DeleteTip(tipId);
        }
    }
}
