using Anonym.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.Abstract
{
    public interface IUserLoginService
    {
        IDataResult<UserLogin> GetByProviderKey(string providerKey, string provider);
        IResult Add(UserLogin userLogin);
        IResult Delete(UserLogin userLogin);
    }
}
