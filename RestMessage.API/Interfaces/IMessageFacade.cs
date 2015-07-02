using System.Collections.Generic;
using RestMessage.API.ViewModels;

namespace RestMessage.API.Interfaces
{
    public interface IMessageFacade
    {
        List<MessageViewModel> GetUnread(string userName);
        List<MessageViewModel> GetAll(string userName);
        List<MessageViewModel> GetRange(string userName, int from, int to);
        void Create(string userName, string message);
        void Delete(string userName, int[] messageIds);
    }
}