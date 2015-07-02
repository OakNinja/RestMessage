using System.Collections.Generic;
using System.Web.Http;
using RestMessage.API.Interfaces;
using RestMessage.API.ViewModels;

namespace RestMessage.API.Controllers
{
    //Default Routeprefix
    [RoutePrefix("api/v1/user/{userName}/messages")]
    public class MessageController : ApiController
    {
        private readonly IMessageFacade _facade;
        //public MessageController()
        //{
        //    if (_facade == null)
        //        _facade = new MessageFacade();
        //}

        public MessageController(IMessageFacade facade)
        {
            _facade = facade;
        }

        [Route("unread")]
        [HttpGet]
        public List<MessageViewModel> GetUnread(string userName)
        {
            return _facade.GetUnread(userName);
        }

        [Route("all")]
        [HttpGet]
        public List<MessageViewModel> GetAll(string userName)
        {
            return _facade.GetAll(userName);
        }

        [Route("range")]
        [HttpGet]
        public List<MessageViewModel> GetRange(string userName, int from, int to)
        {
            return _facade.GetRange(userName, from, to);
        }

        [Route("")]
        [HttpPost]
        public void Post(string message, string userName)
        {
            _facade.Create(userName, message);
        }

        /// <summary>
        ///     Delete messages
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="messageIds"></param>
        [Route("")]
        [HttpDelete]
        public void Delete(string userName, [FromUri] int[] messageIds)
        {
            _facade.Delete(userName, messageIds);
        }
    }
}