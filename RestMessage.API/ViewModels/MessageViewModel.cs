﻿using System;

namespace RestMessage.API.ViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
    }
}