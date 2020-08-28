using Anonym.Entities.ComplexTypes;
using Anonym.Entities.Concrete;
using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<UserRoleClaimsJoin> GetRoleClaims(User user);
        List<UserClaim> GetUserClaims(User user);
    }
}
