using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Entities.Concrete
{
    public class UserToken : IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime Expiration { get; set; }

        public User User { get; set; }
    }
}
