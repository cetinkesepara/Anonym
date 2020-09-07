using Anonym.Business.Utilities.Security.Jwt.Concrete;
using Anonym.Entities.ComplexTypes;
using Anonym.Entities.Concrete;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Anonym.Business.Utilities.Security.Jwt.Abstract
{
    public interface ITokenHelper
    {
        AccessToken CreateTokenForLogin(User user, List<UserRoleClaimsJoin> roleClaims, List<UserClaim> userClaims, bool rememberMe);
        AccessToken CreateTokenForUser(User user);
    }
}
