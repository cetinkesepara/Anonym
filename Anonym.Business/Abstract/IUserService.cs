using Anonym.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.Abstract
{
    public interface IUserService
    {
        IResult Register(User user, string password);
        IResult UserExists(string email);
        User GetByUsername(string username);
        User GetByEmail(string email);
    }
}
