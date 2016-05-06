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
    [RoutePrefix("api/LevelTypeAPI")]
    public class LevelTypeAPIController : ApiController
    {
        private readonly LevelTypesDAL _levelTypeDal;

        public LevelTypeAPIController()
        {
            this._levelTypeDal = new LevelTypesDAL();
        }

        [HttpGet]
        [Route("{levelId}")]
        public LevelType GetLevelType(int levelId)
        {
            return _levelTypeDal.GetOneLevelType(levelId);
        }

        [HttpGet]
        [Route("Name/{levelId}")]
        public string LevelTypeName(int levelId)
        {
            return _levelTypeDal.SelectLevelType(levelId);
        }

        [HttpGet]
        [Route("Type/{levelId}")]
        public int LevelTypeInt(int levelId)
        {
            return _levelTypeDal.GetLevelType(levelId);
        }

        [HttpPost]
        public HttpResponseMessage AddNewLevelType(LevelType type)
        {
            _levelTypeDal.AddNewLevelType(type);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, type);
            response.Headers.Location = new Uri(Url.Link("DefaultAPI", new { id = type.Id }));

            return response;
        }

        [HttpPut]
        public void UpdateLevelType(LevelType type)
        {
            _levelTypeDal.UpdateLevelType(type);
        }

        [HttpDelete]
        [Route("{levTypeId}")]
        public void DeleteLevelType(int levTypeId)
        {
            _levelTypeDal.DeleteLevelTypes(levTypeId);
        }
    }
}
