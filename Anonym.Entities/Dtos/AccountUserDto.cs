using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Entities.Dtos
{
    public class AccountUserDto : IDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Gender { get; set; }
    }
}
