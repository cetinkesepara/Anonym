using Anonym.DataAccess.Abstract;
using Anonym.DataAccess.Concrete.EntityFramework.Contexts;
using Anonym.Entities.ComplexTypes;
using Anonym.Entities.Concrete;
using Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anonym.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User>, IUserDal
    {
        private readonly AnonymContext _context;
        public EfUserDal(AnonymContext context):base(context)
        {
            _context = context;
        }

        public List<UserRoleClaimsJoin> GetRoleClaims(User user)
        {
            var userRoleJoin = from role in _context.Roles
                         join userRoles in _context.UserRoles
                         on role.Id equals userRoles.RoleId
                         where userRoles.UserId == user.Id
                         select new UserRoleJoin 
                         { 
                            UserId = userRoles.UserId,
                            RoleId = userRoles.RoleId,
                            RoleName = role.Name
                         };

            var userRoleClaimsJoin = from urc in userRoleJoin.ToList()
                                     join roleClaims in _context.RoleClaims
                                     on urc.RoleId equals roleClaims.RoleId
                                     where urc.RoleId == roleClaims.RoleId
                                     select new UserRoleClaimsJoin
                                     {
                                         UserId = urc.UserId,
                                         RoleId = urc.RoleId,
                                         RoleName = urc.RoleName,
                                         RoleClaimId = roleClaims.Id,
                                         RoleClaimType = roleClaims.ClaimType,
                                         RoleClaimValue = roleClaims.ClaimValue
                                     };
            return userRoleClaimsJoin.ToList();
        }

        public List<UserClaim> GetUserClaims(User user)
        {
            var result = from u in _context.Users
                         join userClaim in _context.UserClaims
                         on u.Id equals userClaim.UserId
                         where u.Id == user.Id
                         select new UserClaim
                         {
                             Id = userClaim.Id,
                             ClaimType = userClaim.ClaimType,
                             ClaimValue = userClaim.ClaimValue,
                             UserId = u.Id
                         };
            return result.ToList();
        }
    }
}
