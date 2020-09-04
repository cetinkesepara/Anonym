using Anonym.Business.Utilities.Security.Jwt.Abstract;
using Anonym.Entities.ComplexTypes;
using Anonym.Entities.Concrete;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Anonym.Business.Utilities.Security.Jwt.Concrete
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken(User user, List<UserRoleClaimsJoin> roleClaims, List<UserClaim> userClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

            var jwtSecurityToken = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roleClaims, userClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            return new AccessToken { Token = token, Expiration = _accessTokenExpiration };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, List<UserRoleClaimsJoin> roleClaims, List<UserClaim> userClaims)
        {
            var jwt = new JwtSecurityToken(
                    issuer: tokenOptions.Issuer,
                    audience: tokenOptions.Audience,
                    expires: _accessTokenExpiration,
                    notBefore: DateTime.Now,
                    signingCredentials: signingCredentials,
                    claims: SetClaims(user, roleClaims, userClaims)
                );

            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<UserRoleClaimsJoin> roleClaims, List<UserClaim> userClaims)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

            foreach (var roleClaim in roleClaims)
            {
                claims.Add(new Claim(roleClaim.RoleClaimType, roleClaim.RoleClaimValue));
            }

            foreach (var userClaim in userClaims)
            {
                claims.Add(new Claim(userClaim.ClaimType, userClaim.ClaimValue));
            }

            return claims;
        }


    }
}
