using Anonym.Business.Abstract;
using Anonym.Business.Constants.Messages;
using Anonym.Business.Constants.Settings;
using Anonym.Business.Utilities.Helpers;
using Anonym.Business.Utilities.Security.Jwt.Abstract;
using Anonym.Business.Utilities.Security.Jwt.Concrete;
using Anonym.DataAccess.Abstract;
using Anonym.Entities.Concrete;
using Anonym.Entities.Dtos;
using AutoMapper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Transactions;

namespace Anonym.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;
        private readonly IConfiguration _configuration;
        private readonly ISettingService _settingService;
        private readonly IMapper _mapper;
        private readonly IUserTokenSevice _userTokenSevice;

        public UserManager(IUserDal userDal, 
            ITokenHelper tokenHelper, 
            IConfiguration configuration, 
            ISettingService settingService, 
            IMapper mapper, 
            IUserTokenSevice userTokenSevice)
        {
            _userDal = userDal;
            _tokenHelper = tokenHelper;
            _configuration = configuration;
            _settingService = settingService;
            _mapper = mapper;
            _userTokenSevice = userTokenSevice;
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            User user = GetByEmail(userForLoginDto.Email);

            if (user == null)
            {
                return new ErrorDataResult<User>(user, SecurityMessages.LoginCheckError);
            }

            if(!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ErrorDataResult<User>(user, SecurityMessages.LoginCheckError);
            }

            return new SuccessDataResult<User>(user);
        }

        public IDataResult<AccessToken> CreateAccessTokenForLogin(User user, bool rememberMe)
        {
            var roleClaims = _userDal.GetRoleClaims(user);
            var userClaims = _userDal.GetUserClaims(user);
            var accessToken = _tokenHelper.CreateTokenForLogin(user, roleClaims, userClaims, rememberMe);

            return new SuccessDataResult<AccessToken>(accessToken, SecurityMessages.LoginSuccessful);
        }

        public IDataResult<AccessToken> CreateAccessTokenForUser(User user)
        {
            var accessToken = _tokenHelper.CreateTokenForUser(user);
            return new SuccessDataResult<AccessToken>(accessToken, SecurityMessages.LoginSuccessful);
        }

        public IResult Register(User user, string password)
        {
            string registerSuccessMessage = "";
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userDal.Add(user);
            registerSuccessMessage += CrudMessages.UserAdded;

            IResult result = ConfirmationEmail(user);
            if (result.Success)
            {
                registerSuccessMessage += " " + result.Message;
            }

            return new SuccessResult(registerSuccessMessage);
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

        public IResult ConfirmationEmail(User user)
        {
            IDataResult<UserToken> userTokenResult = _userTokenSevice.GetEmailConfirmTokenByUserId(user.Id);
            if (userTokenResult.Success && userTokenResult.Data != null)
            {
                if (userTokenResult.Data.Expiration > DateTime.Now)
                {
                    return new ErrorResult(SecurityMessages.TokenNotExpired);
                }

                IResult deleteOldTokenResult = _userTokenSevice.Delete(userTokenResult.Data);
                if (!deleteOldTokenResult.Success)
                {
                    return new ErrorResult(SecurityMessages.SystemError);
                }
            }

            IDataResult<OptionForSendEmailDto> emailSettingResult = _settingService.GetEmailSettings();
            if (!emailSettingResult.Success || emailSettingResult.Data == null)
            {
                return new ErrorResult(SecurityMessages.SystemError);
            }

            IDataResult<AccessToken> tokenResult = CreateAccessTokenForUser(user);
            if (!tokenResult.Success || tokenResult.Data == null)
            {
                return new ErrorResult(SecurityMessages.SystemError);
            }

            UserToken userToken = new UserToken
            {
                Name = TokenConstants.EmailConfirmation,
                Value = tokenResult.Data.Token,
                Expiration = tokenResult.Data.Expiration,
                UserId = user.Id
            };
            IResult addTokenResult = _userTokenSevice.Add(userToken);
            if (!addTokenResult.Success)
            {
                return new ErrorResult(SecurityMessages.SystemError);
            }

            IResult sendEmailResult = SendConfirmationEmail(user, emailSettingResult, tokenResult);
            if (!sendEmailResult.Success)
            {
                return new ErrorResult(SecurityMessages.SystemError);
            }

            return new SuccessResult(SecurityMessages.ConfirmEmailSuccessful);
        }

        private IResult SendConfirmationEmail(User user, IDataResult<OptionForSendEmailDto> emailSettingResult, IDataResult<AccessToken> tokenResult)
        {
            string originLink = _configuration.GetSection("Origins:Anonym.WebUI.SPA").Value;
            string confirmLink = originLink + "/activateEmail/" + user.Id + "/" + tokenResult.Data.Token;

            EmailSendDto confirmationDto = _mapper.Map<EmailSendDto>(emailSettingResult.Data);
            confirmationDto.RecipientEmail = user.Email;
            confirmationDto.MailSubject = "Üyelik Etkinleştirme";
            confirmationDto.MailBody = "<h2>Üyeliğinizi etkinleştirmek için lütfen aşağıdaki bağlantıya tıklayınız.</h2><hr/>"
                + $"<a href='{confirmLink}'>- Üyelik doğrulama bağlantısı</a><br/>"
                + "<p>Bu bağlantıyı başkalarıyla paylaşmayınız!</p>";

            EmailHelper.SendEmail(confirmationDto);

            return new SuccessResult();
        }

        public IDataResult<User> GetById(string userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == userId));
        }

        public IResult ActivateEmail(User user, string token)
        {
            IDataResult<UserToken> userTokenResult = _userTokenSevice.GetEmailConfirmTokenByUserId(user.Id);
            if (!userTokenResult.Success || userTokenResult.Data == null)
            {
                return new ErrorResult(SecurityMessages.SystemError);
            }

            if (userTokenResult.Data.Value != token)
            {
                return new ErrorResult(SecurityMessages.InvalidTransaction);
            }

            if (userTokenResult.Data.Expiration < DateTime.Now)
            {
                _userTokenSevice.Delete(userTokenResult.Data);
                return new ErrorResult(SecurityMessages.TokenHasExpiredForEmailConfirm);
            }

            user.EmailConfirmed = true;

            using (TransactionScope transaction = new TransactionScope())
            {
                _userDal.Update(user);
                _userTokenSevice.Delete(userTokenResult.Data);

                transaction.Complete();
            }
            
            return new SuccessResult(SecurityMessages.UserEmailActivated);
        }

        public IResult IsConfirmEmail(string email)
        {
            User user = GetByEmail(email);
            if (!user.EmailConfirmed)
            {
                return new ErrorResult(SecurityMessages.LoginNotConfirmEmail);
            }
            return new SuccessResult();
        }

        public IResult ForgettingPassword(User user)
        {
            IDataResult<UserToken> userTokenResult = _userTokenSevice.GetPasswordResetTokenByUserId(user.Id);
            if (userTokenResult.Success && userTokenResult.Data != null)
            {
                if (userTokenResult.Data.Expiration > DateTime.Now)
                {
                    return new ErrorResult(SecurityMessages.TokenNotExpired);
                }

                IResult deleteOldTokenResult = _userTokenSevice.Delete(userTokenResult.Data);
                if (!deleteOldTokenResult.Success)
                {
                    return new ErrorResult(SecurityMessages.SystemError);
                }
            }

            IDataResult<OptionForSendEmailDto> emailSettingResult = _settingService.GetEmailSettings();
            if (!emailSettingResult.Success || emailSettingResult.Data == null)
            {
                return new ErrorResult(SecurityMessages.SystemError);
            }

            IDataResult<AccessToken> tokenResult = CreateAccessTokenForUser(user);
            if (!tokenResult.Success || tokenResult.Data == null)
            {
                return new ErrorResult(SecurityMessages.SystemError);
            }

            UserToken userToken = new UserToken
            {
                Name = TokenConstants.PasswordReset,
                Value = tokenResult.Data.Token,
                Expiration = tokenResult.Data.Expiration,
                UserId = user.Id
            };
            IResult addTokenResult = _userTokenSevice.Add(userToken);
            if (!addTokenResult.Success)
            {
                return new ErrorResult(SecurityMessages.SystemError);
            }

            IResult sendEmailResult = SendForgettingPasswordEmail(user, emailSettingResult, tokenResult);
            if (!sendEmailResult.Success)
            {
                return new ErrorResult(SecurityMessages.SystemError);
            }

            return new SuccessResult(SecurityMessages.ForgettingPasswordEmailSuccessful);
        }

        private IResult SendForgettingPasswordEmail(User user, IDataResult<OptionForSendEmailDto> emailSettingResult, IDataResult<AccessToken> tokenResult)
        {
            string originLink = _configuration.GetSection("Origins:Anonym.WebUI.SPA").Value;
            string confirmLink = originLink + "/resetPassword/" + user.Id + "/" + tokenResult.Data.Token;

            EmailSendDto confirmationDto = _mapper.Map<EmailSendDto>(emailSettingResult.Data);
            confirmationDto.RecipientEmail = user.Email;
            confirmationDto.MailSubject = "Şifre Sıfırlama";
            confirmationDto.MailBody = "<h2>Şifrenizi sıfırlamak için lütfen aşağıdaki bağlantıya tıklayınız.</h2><hr/>"
                + $"<a href='{confirmLink}'>- Şifre sıfırlama bağlantısı</a><br/>"
                + "<p>Bu bağlantıyı başkalarıyla paylaşmayınız!</p>";

            EmailHelper.SendEmail(confirmationDto);

            return new SuccessResult();
        }

        public IResult ResetPasswordForForgetten(User user, string token, string password)
        {
            IDataResult<UserToken> userTokenResult = _userTokenSevice.GetPasswordResetTokenByUserId(user.Id);
            if (!userTokenResult.Success || userTokenResult.Data == null)
            {
                return new ErrorResult(SecurityMessages.SystemError);
            }

            if (userTokenResult.Data.Value != token)
            {
                return new ErrorResult(SecurityMessages.InvalidTransaction);
            }

            if (userTokenResult.Data.Expiration < DateTime.Now)
            {
                _userTokenSevice.Delete(userTokenResult.Data);
                return new ErrorResult(SecurityMessages.TokenHasExpiredForPasswordReset);
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            using (TransactionScope transaction = new TransactionScope())
            {
                _userDal.Update(user);
                _userTokenSevice.Delete(userTokenResult.Data);

                transaction.Complete();
            }

            return new SuccessResult(SecurityMessages.PasswordHasBeenReset);
        }
    }
}
