﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Entities.Dtos
{
    public class UserForLoginDto:IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
