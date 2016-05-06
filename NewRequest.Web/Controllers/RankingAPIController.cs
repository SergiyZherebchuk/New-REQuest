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
    [RoutePrefix("api/RankingAPI")]
    public class RankingAPIController : ApiController
    {
        private readonly RankingDAL _rankDal;

        public RankingAPIController()
        {
            this._rankDal = new RankingDAL();
        }

        [HttpGet]
        [Route("{userId}")]
        public Ranking SelectRankOneUser(int userId)
        {
            return _rankDal.SelectOneUser(userId);
        }

        [HttpGet]
        [Route("top10Point")]
        public List<UserRank> SelectTop10Point()
        {
            return _rankDal.SelectTop10Points();
        }

        [HttpGet]
        [Route("top10TeamPoint")]
        public List<UserRank> SelectTop10TeamPoint()
        {
            return _rankDal.SelectTop10TeamPoints();
        }

        [HttpGet]
        [Route("AllPoint")]
        public List<UserRank> SelectAllPoint()
        {
            return _rankDal.SelectPointsSort();
        }

        [HttpGet]
        [Route("AllTeamPoint")]
        public List<UserRank> SelectAllTeamPoint()
        {
            return _rankDal.SelectTeamPointsSort();
        }        

        [HttpPost]
        public HttpResponseMessage AddNewRank(Ranking rank)
        {
            _rankDal.AddNewUserRank(rank);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, rank);
            response.Headers.Location = new Uri(Url.Link("DefaultAPI", new { id = rank.Id }));

            return response;
        }

        [HttpPut]
        public void UpdateRank(Ranking rank)
        {
            _rankDal.UpdateUserRank(rank);
        }
    }
}
