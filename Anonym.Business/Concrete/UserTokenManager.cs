using Anonym.Business.Abstract;
using Anonym.Business.Constants.Settings;
using Anonym.DataAccess.Abstract;
using Anonym.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.Concrete
{
    public class UserTokenManager : IUserTokenSevice
    {
        private readonly IUserTokenDal _userTokenDal;

        public UserTokenManager(IUserTokenDal userTokenDal)
        {
            _userTokenDal = userTokenDal;
        }

        public IResult Add(UserToken userToken)
        {
            _userTokenDal.Add(userToken);
            return new SuccessResult();
        }

        public IResult Delete(UserToken userToken)
        {
            _userTokenDal.Delete(userToken);
            return new SuccessResult();
        }

        public IDataResult<UserToken> GetEmailConfirmTokenByUserId(string userId)
        {
            var userToken = _userTokenDal.Get(ut => ut.Name == TokenConstants.EmailConfirmation && ut.UserId == userId);
            return new SuccessDataResult<UserToken>(userToken);
        }

        public IDataResult<UserToken> GetPasswordResetTokenByUserId(string userId)
        {
            var userToken = _userTokenDal.Get(ut => ut.Name == TokenConstants.PasswordReset && ut.UserId == userId);
            return new SuccessDataResult<UserToken>(userToken);
        }
    }
}
