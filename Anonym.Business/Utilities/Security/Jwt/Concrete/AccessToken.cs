using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.Utilities.Security.Jwt.Concrete
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
