using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Entities.Dtos
{
    public class PasswordDto : IDto
    {
        public string Password { get; set; }
    }
}
