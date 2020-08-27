using Anonym.Business.Abstract;
using Anonym.Business.Constants.Messages;
using Anonym.DataAccess.Abstract;
using Anonym.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Anonym.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userDal.Add(user);

            return new SuccessResult(CrudMessages.UserAdded);
        }

        public IResult UserExists(string email)
        {
            if (GetByEmail(email) != null)
            {
                return new ErrorResult(CrudMessages.UserExistsForEmail);
            }
            return new SuccessResult();
        }

        public User GetByEmail(string email)
        {
            return _userDal.Get(u=>u.NormalizedEmail == email.ToUpper(new CultureInfo("en-Us")));
        }

        public User GetByUsername(string username)
        {
            return _userDal.Get(u => u.NormalizedUserName == username.ToUpper(new CultureInfo("en-Us")));
        }
    }
}
