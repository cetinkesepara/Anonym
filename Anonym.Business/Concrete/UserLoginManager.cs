using Anonym.Business.Abstract;
using Anonym.DataAccess.Abstract;
using Anonym.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.Concrete
{
    public class UserLoginManager : IUserLoginService
    {
        private readonly IUserLoginDal _userLoginDal;

        public UserLoginManager(IUserLoginDal userLoginDal)
        {
            _userLoginDal = userLoginDal;
        }

        public IResult Add(UserLogin userLogin)
        {
            _userLoginDal.Add(userLogin);
            return new SuccessResult();
        }

        public IResult Delete(UserLogin userLogin)
        {
            _userLoginDal.Delete(userLogin);
            return new SuccessResult();
        }

        public IDataResult<UserLogin> GetByProviderKey(string providerKey, string provider)
        {
            return new SuccessDataResult<UserLogin>(_userLoginDal.Get(u => u.ProviderKey == providerKey && u.LoginProvider == provider));
        }
    }
}
