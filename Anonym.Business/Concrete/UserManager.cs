using Anonym.Business.Abstract;
using Anonym.Business.Constants.Messages;
using Anonym.Business.Utilities.Security.Jwt.Abstract;
using Anonym.Business.Utilities.Security.Jwt.Concrete;
using Anonym.DataAccess.Abstract;
using Anonym.Entities.Concrete;
using Anonym.Entities.Dtos;
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
        private readonly ITokenHelper _tokenHelper;

        public UserManager(IUserDal userDal, ITokenHelper tokenHelper)
        {
            _userDal = userDal;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            User user = GetByEmail(userForLoginDto.Email);

            if (user == null)
            {
                return new ErrorDataResult<User>(SecurityMessages.LoginCheckError);
            }

            if(!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ErrorDataResult<User>(SecurityMessages.LoginCheckError);
            }

            return new SuccessDataResult<User>(user);
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var roleClaims = _userDal.GetRoleClaims(user);
            var userClaims = _userDal.GetUserClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, roleClaims, userClaims);

            return new SuccessDataResult<AccessToken>(accessToken, SecurityMessages.LoginSuccessful);
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
