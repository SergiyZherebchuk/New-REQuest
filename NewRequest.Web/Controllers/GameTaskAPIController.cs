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
    [RoutePrefix("api/GameTaskAPI")]
    public class GameTaskAPIController : ApiController
    {
        private readonly GameTasksDAL _gamesTaskDal;

        public GameTaskAPIController()
        {
            this._gamesTaskDal = new GameTasksDAL();
        }

        [HttpGet]
        [Route("{levelId}")]
        public GameTask SelectTask(int levelId)
        {
            return _gamesTaskDal.SelectTask(levelId);
        }
        
        [HttpPost]
        public HttpResponseMessage AddNewTask(GameTask task)
        {
            _gamesTaskDal.AddNewTask(task);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, task);
            response.Headers.Location = new Uri(Url.Link("DefaultAPI", new { id = task.Id }));

            return response;
        }

        [HttpPut]
        public void UpdateTask(GameTask task)
        {
            _gamesTaskDal.UpdateTask(task);
        }
        
        [HttpDelete]
        [Route("{taskId}")]
        public void DeleteTask(int taskId)
        {
            _gamesTaskDal.DeleteTask(taskId);
        }
    }
}
