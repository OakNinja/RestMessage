using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestMessage.App
{
    public interface IMessage
    {
        int Id { get; set; }
        string UserName { get; set; }
        string Body { get; set; }
        DateTime Created { get; set; }
        bool Unread { get; set; }
    }
}
