using Anonym.Business.Utilities.Security.Jwt.Concrete;
using Anonym.Entities.ComplexTypes;
using Anonym.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.Utilities.Security.Jwt.Abstract
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<UserRoleClaimsJoin> roleClaims, List<UserClaim> userClaims);
    }
}
