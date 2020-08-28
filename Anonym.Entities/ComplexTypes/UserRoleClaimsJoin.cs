using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Entities.ComplexTypes
{
    public class UserRoleClaimsJoin : UserRoleJoin
    {
        public int RoleClaimId { get; set; }
        public string RoleClaimType { get; set; }
        public string RoleClaimValue { get; set; }
    }
}
