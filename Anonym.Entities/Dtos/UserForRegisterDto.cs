using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Entities.Dtos
{
    public class UserForRegisterDto:IDto
    {
        public string Name { get; set; }
        public string City { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
