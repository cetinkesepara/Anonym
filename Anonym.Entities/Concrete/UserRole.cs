using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Entities.Concrete
{
    public class UserRole:IEntity
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}
