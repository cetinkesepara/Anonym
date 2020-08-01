using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Entities.Concrete
{
    public class ChatMessage:IEntity
    {
        public string ChatMessageId { get; set; }
        public string ChatRoomId { get; set; }
        public string DisplayUserName { get; set; }
        public string Message { get; set; }
        public bool IsPublisherMessage { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
