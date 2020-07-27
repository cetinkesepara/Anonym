using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Entities.Concrete
{
    public class Post:IEntity
    {
        public string PostId { get; set; }
        public string UserId { get; set; }
        public string CategoryId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatePostDate { get; set; }
        public bool IsActive { get; set; }
    }
}
