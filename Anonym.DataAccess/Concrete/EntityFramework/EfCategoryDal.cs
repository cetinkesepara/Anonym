using Anonym.DataAccess.Abstract;
using Anonym.Entities.Concrete;
using Core.DataAccess.EntityFramework;
using Core.DataAccess.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, AnonymContext>, ICategoryDal
    {
    }
}
