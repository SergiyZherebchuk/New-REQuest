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
    [RoutePrefix("api/UserTeamStatusAPI")]
    public class UserTeamStatusAPIController : ApiController
    {
        private readonly UserTeamStatusDAL _utsDal;

        public UserTeamStatusAPIController()
        {
            this._utsDal = new UserTeamStatusDAL();
        }

        [HttpGet]
        [Route("User/{userId}")]
        public UserTeamStatu SelectOneUser(int userId)
        {
            return _utsDal.SelectOneUser(userId);
        }

        [HttpGet]
        [Route("Captain/{teamId}")]
        public TeamUser SelectCaptain(int teamId)
        {
            return _utsDal.SelectCaptainUser(teamId);
        }

        [HttpGet]
        [Route("Active/{teamId}")]
        public List<TeamUser> SelectActive(int teamId)
        {
            return _utsDal.SelectActiveUser(teamId);
        }

        [HttpGet]
        [Route("Spare/{teamId}")]
        public List<TeamUser> SelectSpare(int teamId)
        {
            return _utsDal.SelectSpareUser(teamId);
        }

        [HttpGet]
        [Route("Join/{teamId}")]
        public List<TeamUser> SelectJoin(int teamId)
        {
            return _utsDal.SelectJoinUser(teamId);
        }

        [HttpPost]
        public HttpResponseMessage AddNewUTS(UserTeamStatu uts)
        {
            _utsDal.AddNewStatus(uts);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, uts);
            response.Headers.Location = new Uri(Url.Link("DefaultAPI", new { id = uts.Id }));

            return response;
        }

        [HttpPut]
        public void UpdateRank(UserTeamStatu uts)
        {
            _utsDal.UpdateNewStatus(uts);
        }
    }
}
