using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Entities.Concrete
{
    public class User:IEntity
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
        public string City { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Gender { get; set; }

        public List<UserRole> UserRoles { get; set; }
        public List<UserLogin> UserLogins { get; set; }
        public List<UserClaim> UserClaims { get; set; }
        public List<Post> Posts { get; set; }
    }
}
