using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Entities.Dtos
{
    public class UserForSocialLoginDto : IDto
    {
        public string Provider { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
