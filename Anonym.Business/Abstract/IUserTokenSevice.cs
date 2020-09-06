using Anonym.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.Abstract
{
    public interface IUserTokenSevice 
    {
        IDataResult<UserToken> GetEmailConfirmTokenByUserId(string userId);
        IDataResult<UserToken> GetPasswordResetTokenByUserId(string userId);
        IResult Add(UserToken userToken);
        IResult Delete(UserToken userToken);
    }
}
