using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewRequest.DAL.DAL;
using NewRequest.Entities;
using NewRequest.DAL.Models;

namespace NewRequest.Web.Controllers
{
    [RoutePrefix("api/CodesAPI")]
    public class CodesAPIController : ApiController
    {
        private readonly CodesDAL _codesDal;

        public CodesAPIController()
        {
            this._codesDal = new CodesDAL();
        }

        [HttpGet]
        [Route("One/{levelId}")]
        public Code SelectOneCode(int levelId)
        {
            return _codesDal.SelectOneCode(levelId);
        }

        [HttpGet]
        [Route("Sector/{levelId}")]
        public IList<Code> SelectSectorCode(int levelId)
        {
            return _codesDal.SelectSectorCodes(levelId);
        }

        [HttpPost]
        [Route("One")]
        public HttpResponseMessage AddNewCode(Code code)
        {
            _codesDal.AddNewCode(code);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, code);
            response.Headers.Location = new Uri(Url.Link("DefaultAPI", new { id = code.Id }));

            return response;
        }

        [HttpPost]
        [Route("Sector")]
        public void AddSectorCodes(List<Code> codes)
        {
            _codesDal.AddSectorCode(codes);
        }

        [HttpGet]
        [Route("True/{levelId}")]
        public bool IsTrueCode(int levelId, string code)
        {
            return _codesDal.IsTrueCode(levelId, code);
        }

        [HttpGet]
        [Route("Sector/True/{levelId}")]
        public CloseCode IsSectorTrueCode(int levelId, string code)
        {
            return _codesDal.IsSectorTrueCode(levelId, code);
        }

        [HttpPut]
        [Route("One")]
        public void UpdateCodes(Code code)
        {
            _codesDal.UpdateCode(code);
        }

        [HttpPut]
        [Route("Sector")]
        public void UpdateSectorCodes(List<Code> codes)
        {
            _codesDal.UpdateSectorCodes(codes);
        }

        [HttpDelete]
        [Route("One/{codeId}")]
        public void DeleteCode(int codeId)
        {
            _codesDal.DeleteCodes(codeId);
        }

        [HttpDelete]
        [Route("Sector/{codesLevelId}")]
        public void DeleteSectorCode(int codesLevelId)
        {
            _codesDal.DeleteSectorCode(codesLevelId);
        }
    }
}
