using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Entities.Dtos
{
    public class ForgettingPasswordDto : IDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
