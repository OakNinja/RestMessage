using RestMessage.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestMessage.API.Tests.Mocks
{
    public class MockMessageRepository : IMessageRepository<IMessage>
    {
        private int messageIdIncrementer = 0;
        private List<IMessage> _messages;

        public MockMessageRepository()
        {
            if (_messages == null)
                _messages = new List<IMessage>();
        }

        public bool Insert(IMessage message)
        {
            if (message == null)
                return false;

            message.Unread = true;
            message.Id = ++messageIdIncrementer;
            message.Created = DateTime.Now;

            _messages.Add(message);

            return true;
        }

        public bool Delete(string userName, int[] messageIds)
        {
            foreach (var id in messageIds)
            {
                var message = _messages.FirstOrDefault(m => m.Id == id && m.UserName.Equals(userName));

                var deleted = _messages.Remove(message);
            }

            return true;
        }

        public IEnumerable<IMessage> GetAll(string userName)
        {
            var messages = _messages.Where(m => m.UserName.Equals(userName));

            return SetMessagesAsRead(messages);
        }

        public IEnumerable<IMessage> GetUnread(string userName)
        {
            //If we don't create a new object and return the IQueryable, 
            //the method will return zero results since IQueryable isn't executed until it's used. 
            var messages = _messages.Where(m => m.UserName.Equals(userName) && m.Unread == true).ToList();
            
            return SetMessagesAsRead(messages);
        }
        
        public IEnumerable<IMessage> GetRange(string userName, int fromMessageId, int toMessageId)
        {
            var messages = _messages.Where(m => m.UserName.Equals(userName) && m.Id >= fromMessageId && m.Id <= toMessageId);

            return SetMessagesAsRead(messages);
        }

        #region Private, Non Interface Methods

        /// <summary>
        /// The repository is responsible for setting messages as read - could be done with an SP in MSSQL, 
        /// or script fields in ElasticSearch. In this InMemoryRepository, it's set within a private method.
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        private IEnumerable<IMessage> SetMessagesAsRead(IEnumerable<IMessage> messages)
        {
            foreach (var message in messages)
                SetMessageAsRead(message);

            return messages;
        }

        private IMessage SetMessageAsRead(IMessage message)
        {
            //Only update the entity if it's unread 
            if (message.Unread)
                _messages.Find(m => m.Id == message.Id).Unread = false;

            return message;
        }

        #endregion

    }
}
