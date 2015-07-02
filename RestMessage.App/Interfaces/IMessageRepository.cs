using System.Collections.Generic;

namespace RestMessage.App
{
    public interface IMessageRepository<IMessage>
    {
        void Insert(string userId, string message);
        void Delete(string userId, int[] messageIds);
        IEnumerable<IMessage> GetAll(string userId);
        IEnumerable<IMessage> GetUnread(string userId);
        IEnumerable<IMessage> GetRange(string userId, int fromMessageId, int toMessageId);
    }
}