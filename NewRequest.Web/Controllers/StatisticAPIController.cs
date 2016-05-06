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
    [RoutePrefix("api/StatisticAPI")]
    public class StatisticAPIController : ApiController
    {
        private readonly StatisticDAL _statDal;

        public StatisticAPIController()
        {
            this._statDal = new StatisticDAL();
        }

        [HttpGet]
        [Route("Possition/{gameId}/{level}")]
        public int GetLastPossition(int gameId, int level)
        {
            return _statDal.GetLastPossition(gameId, level);
        }

        [HttpGet]
        [Route("User/{gameId}")]
        public IList<UserStatistic> SelectUserStat(int gameId)
        {
            return _statDal.SelectUserStat(gameId);
        }

        [HttpGet]
        [Route("Teams/{gameId}")]
        public IList<TeamStatistic> SelectTeamStat(int gameId)
        {
            return _statDal.SelectTeamStat(gameId);
        }

        [HttpGet]
        [Route("Last/User/{gameId}/{memberId}")]
        public UserStatistic SelectLastUser(int gameId, int memberId)
        {
            return _statDal.SelectLastUser(gameId, memberId);
        }

        [HttpGet]
        [Route("Last/Team/{gameId}/{memberId}")]
        public TeamStatistic SelectLastTeam(int gameId, int memberId)
        {
            return _statDal.SelectLastTeam(gameId, memberId);
        }

        [HttpPost]
        public HttpResponseMessage AddNewStat(Statistic stat)
        {
            _statDal.AddNewStatistic(stat);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, stat);
            response.Headers.Location = new Uri(Url.Link("DefaultAPI", new { id = stat.Id }));

            return response;
        }
    }
}
