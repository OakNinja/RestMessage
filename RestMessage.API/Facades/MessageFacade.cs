using System;
using System.Collections.Generic;
using System.Linq;
using RestMessage.App;
using RestMessage.API.Interfaces;
using RestMessage.API.ViewModels;

namespace RestMessage.API.Facades
{
    public class MessageFacade : IMessageFacade
    {
        private readonly IMessageRepository<IMessage> _repository;
        //public MessageFacade()
        //{
        //    if (_repository == null)
        //        _repository = new MessageRepository();
        //}

        public MessageFacade(IMessageRepository<IMessage> repository)
        {
            _repository = repository;
        }

        public List<MessageViewModel> GetUnread(string userName)
        {
            return _repository.GetUnread(userName).OrderBy(m => m.Created).Select(MessageToMessageViewModel()).ToList();
        }

        public List<MessageViewModel> GetAll(string userName)
        {
            return _repository.GetAll(userName).OrderBy(m => m.Created).Select(MessageToMessageViewModel()).ToList();
        }

        public List<MessageViewModel> GetRange(string userName, int from, int to)
        {
            return
                _repository.GetRange(userName, from, to)
                    .OrderByDescending(m => m.Created)
                    .Select(MessageToMessageViewModel())
                    .ToList();
        }

        public void Create(string userName, string message)
        {
            _repository.Insert(userName, message);
        }

        public void Delete(string userName, int[] messageIds)
        {
            _repository.Delete(userName, messageIds);
        }

        #region Non interface methods

        private static Func<IMessage, MessageViewModel> MessageToMessageViewModel()
        {
            return m => new MessageViewModel {Id = m.Id, Body = m.Body, Created = m.Created, UserName = m.UserName};
        }

        #endregion
    }
}