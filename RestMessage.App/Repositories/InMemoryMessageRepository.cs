using System;
using System.Collections.Generic;
using System.Linq;

namespace RestMessage.App.Repositories
{
    public class InMemoryMessageRepository : IMessageRepository<IMessage>
    {
        private readonly List<IMessage> _messages;
        private int _messageIdIncrementer;

        public InMemoryMessageRepository()
        {
            if (_messages == null)
                _messages = new List<IMessage>();
        }

        public void Insert(string userName, string messageBody)
        {
            var message = new Message
            {
                UserName = userName,
                Body = messageBody,
                Unread = true,
                Id = _messageIdIncrementer++,
                Created = DateTime.Now
            };

            _messages.Add(message);
        }

        public void Delete(string userName, int[] messageIds)
        {
            foreach (var id in messageIds)
            {
                var message = _messages.FirstOrDefault(m => m.Id == id && m.UserName.Equals(userName));

                var deleted = _messages.Remove(message);
            }
        }

        public IEnumerable<IMessage> GetAll(string userName)
        {
            var messages = _messages.Where(m => m.UserName.Equals(userName));

            return SetMessagesAsRead(messages);
        }

        public IEnumerable<IMessage> GetUnread(string userName)
        {
            // We have to create a new object from the IQueryable to be able to return the messages and 
            // still set them to read, otherwise zero messages will be unread when the query is resolved.
            var messages = _messages.Where(m => m.UserName.Equals(userName) && m.Unread).ToList();

            return SetMessagesAsRead(messages);
        }

        public IEnumerable<IMessage> GetRange(string userName, int fromMessageId, int toMessageId)
        {
            var messages =
                _messages.Where(m => m.UserName.Equals(userName) && m.Id >= fromMessageId && m.Id <= toMessageId);

            return SetMessagesAsRead(messages);
        }

        #region Private, Non Interface Methods

        /// <summary>
        ///     The repository is responsible for setting messages as read - could be done with an SP in MSSQL,
        ///     or script fields in ElasticSearch. In this InMemoryRepository, it's set within a private method.
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