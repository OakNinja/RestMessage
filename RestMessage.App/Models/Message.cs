using System;

namespace RestMessage.App
{
    public class Message : IMessage
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public bool Unread { get; set; }
    }
}