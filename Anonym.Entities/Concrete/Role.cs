using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Entities.Concrete
{
    public class Role:IEntity
    {
        public Role()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        public List<UserRole> UserRoles { get; set; }
        public List<RoleClaim> RoleClaims { get; set; }
    }
}
