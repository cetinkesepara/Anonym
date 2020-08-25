using Anonym.DataAccess.Abstract;
using Anonym.DataAccess.Concrete.EntityFramework.Contexts;
using Anonym.Entities.Concrete;
using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Anonym.DataAccess.Concrete.EntityFramework
{
    public class EfPostDal : EfEntityRepositoryBase<Post>, IPostDal
    {
        public EfPostDal(AnonymContext context):base(context)
        {

        }
    }
}
