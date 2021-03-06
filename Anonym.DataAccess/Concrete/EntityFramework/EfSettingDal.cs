﻿using Anonym.DataAccess.Abstract;
using Anonym.DataAccess.Concrete.EntityFramework.Contexts;
using Anonym.Entities.Concrete;
using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.DataAccess.Concrete.EntityFramework
{
    public class EfSettingDal : EfEntityRepositoryBase<Setting>, ISettingDal
    {
        public EfSettingDal(AnonymContext context) : base(context)
        {
        }
    }
}
