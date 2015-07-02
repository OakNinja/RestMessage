using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestMessage.App
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
    }
}
