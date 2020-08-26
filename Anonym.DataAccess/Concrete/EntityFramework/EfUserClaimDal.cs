using Anonym.DataAccess.Abstract;
using Anonym.DataAccess.Concrete.EntityFramework.Contexts;
using Anonym.Entities.Concrete;
using Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.DataAccess.Concrete.EntityFramework
{
    public class EfUserClaimDal : EfEntityRepositoryBase<UserClaim>, IUserClaimDal
    {
        public EfUserClaimDal(AnonymContext context):base(context)
        {

        }
    }
}
