using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestMessage.App
{
    public interface IMessageRepository<IMessage>
    {
        bool Insert(IMessage entity);
        bool Delete(string userId, int[] messageIds);
        IEnumerable<IMessage> GetAll(string userId);
        IEnumerable<IMessage> GetUnread(string userId);
        IEnumerable<IMessage> GetRange(string userId, int fromMessageId, int toMessageId);
    }
}
