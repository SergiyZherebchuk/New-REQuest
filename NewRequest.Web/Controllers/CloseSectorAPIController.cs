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
    [RoutePrefix("api/CloseSectorAPI")]
    public class CloseSectorAPIController : ApiController
    {
        private readonly CloseSectorDAL _closeSector;

        public CloseSectorAPIController()
        {
            this._closeSector = new CloseSectorDAL();
        }

        [HttpGet]
        public IList<CloseSector> SelectAllCloseSector(int gameId, int levelId, int memberId)
        {
            return _closeSector.SelectCloseLevels(gameId, levelId, memberId);
        }

        [HttpGet]
        [Route("Sector")]
        public int LastCloseSector(int gameId, int levelId, int memberId)
        {
            return _closeSector.GetCloseSector(gameId, levelId, memberId);
        }

        [HttpPost]
        public HttpResponseMessage AddCloseSector(CloseSector closeSector)
        {
            _closeSector.AddNewCloseSector(closeSector);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, closeSector);
            response.Headers.Location = new Uri(Url.Link("DefaultAPI", new { id = closeSector.Id }));

            return response;
        }

        [HttpDelete]
        public void DeleteSector(int gameId, int levelId, int memberId)
        {
            _closeSector.DeleteSector(gameId, levelId, memberId);
        }
    }
}
