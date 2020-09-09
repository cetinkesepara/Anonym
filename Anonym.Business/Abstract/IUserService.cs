using Anonym.Business.Utilities.Security.Jwt.Concrete;
using Anonym.Entities.Concrete;
using Anonym.Entities.Dtos;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.Abstract
{
    public interface IUserService
    {
        IDataResult<AccessToken> CreateAccessTokenForLogin(User user, bool rememberMe);
        IDataResult<AccessToken> CreateAccessTokenForUser(User user);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IDataResult<User> GetById(string userId);
        IResult Register(User user, string password);
        IResult RegisterForSocialLogin(User user);
        IResult UserExists(string email);
        IResult ConfirmationEmail(User user);
        IResult ActivateEmail(User user, string token);
        IResult IsConfirmEmail(string email);
        IResult ForgettingPassword(User user);
        IResult ResetPasswordForForgetten(User user, string token, string password);
        User GetByUsername(string username);
        User GetByEmail(string email);
    }
}
