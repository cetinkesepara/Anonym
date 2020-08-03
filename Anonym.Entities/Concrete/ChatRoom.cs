using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Entities.Concrete
{
    public class ChatRoom:IEntity
    {
        public string ChatRoomId { get; set; }
        public string ReviewerUserId { get; set; }
        public string PostId { get; set; }
        public string PublisherName { get; set; }
        public string ReviewerName { get; set; }
        public bool PublisherCommented { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }

        public Post Post { get; set; }
        public List<ChatMessage> ChatMessages { get; set; }
    }
}
