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
    [RoutePrefix("api/TeamsAPI")]
    public class TeamsAPIController : ApiController
    {
        private readonly TeamsDAL _teamsDal;

        public TeamsAPIController()
        {
            this._teamsDal = new TeamsDAL();
        }

        [HttpGet]
        public List<AllTeam> SelectAllTeam()
        {
            return _teamsDal.SelectAllTeams();
        }

        [HttpGet]
        [Route("Last/{userId}")]
        public Team SelectLastUserTeams(int userId)
        {
            return _teamsDal.SelectLastTeam(userId);
        }

        [HttpGet]
        [Route("{teamId}")]
        public Team SelectOneTeams(int teamId)
        {
            return _teamsDal.SelectOneTeam(teamId);
        }

        [HttpGet]
        [Route("Top10")]
        public List<TeamRank> SelectTop10Rank()
        {
            return _teamsDal.SelectTop10();
        }

        [HttpGet]
        [Route("All")]
        public List<TeamRank> SelectAllRank()
        {
            return _teamsDal.SelectSort();
        }     

        [HttpPost]
        public HttpResponseMessage AddNewTeam(Team team)
        {
            _teamsDal.AddNewTeam(team);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, team);
            response.Headers.Location = new Uri(Url.Link("DefaultAPI", new { id = team.Id }));

            return response;
        }

        [HttpPut]
        public void UpdateTeam(Team team)
        {
            _teamsDal.UpdateTeam(team);
        }

        [HttpPut]
        public void UpdateTeamPoint(int teamId, int teamPoint)
        {
            _teamsDal.UpdatePointInTeam(teamId, teamPoint);
        }

        [HttpDelete]
        [Route("{teamId}")]
        public void DeleteTeam(int teamId)
        {
            _teamsDal.DeleteTeam(teamId);
        }
    }
}
