using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Entities.Dtos
{
    public class UserInformationDto : IDto
    {
        public string Name { get; set; }
        public string City { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Gender { get; set; }
    }
}
