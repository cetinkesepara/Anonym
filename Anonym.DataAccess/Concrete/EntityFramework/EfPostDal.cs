﻿using Anonym.DataAccess.Abstract;
using Anonym.Entities.Concrete;
using Core.DataAccess.EntityFramework;
using Core.DataAccess.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Anonym.DataAccess.Concrete.EntityFramework
{
    public class EfPostDal : EfEntityRepositoryBase<Post, AnonymContext>, IPostDal
    {
    }
}