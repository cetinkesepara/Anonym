using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using Anonym.Business.Abstract;
using Anonym.Business.Constants.Errors;
using Anonym.Business.Constants.Messages;
using Anonym.Business.Utilities.Security.Jwt.Concrete;
using Anonym.Entities.Concrete;
using Anonym.Entities.Dtos;
using AutoMapper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Anonym.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IMapper _mapper;
        private IUserService _userService;
        private IUserLoginService _userLoginService;

        public UsersController(IMapper mapper, IUserService userService, IUserLoginService userLoginService)
        {
            _mapper = mapper;
            _userService = userService;
            _userLoginService = userLoginService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserForLoginDto userForLoginDto)
        {
            IDataResult<User> loginResult = _userService.Login(userForLoginDto);
            if (!loginResult.Success)
            {
                return Unauthorized(new ErrorResultDto { 
                    Name = ErrorNames.UnauthorizedUser, 
                    Type = ErrorTypes.Warning,
                    Value = loginResult.Message
                });
            }

            IResult confirmEmailResult = _userService.IsConfirmEmail(userForLoginDto.Email);
            if (!confirmEmailResult.Success)
            {
                return BadRequest(new ErrorResultDto { 
                    Name = ErrorNames.UnverifiedEmail,
                    Type = ErrorTypes.Warning,
                    Value = confirmEmailResult.Message
                });
            }

            IDataResult<AccessToken> createTokenResult = _userService.CreateAccessTokenForLogin(loginResult.Data, userForLoginDto.RememberMe);
            if (!createTokenResult.Success)
            {
                return BadRequest(new ErrorResultDto { 
                    Name = ErrorNames.DefaultError,
                    Type = ErrorTypes.Danger,
                    Value = SecurityMessages.SystemError
                });
            }

            return Ok(createTokenResult);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            User user = _mapper.Map<User>(userForRegisterDto);
            user.UserName = userForRegisterDto.Email;
            user.NormalizedEmail = userForRegisterDto.Email.ToUpper(new CultureInfo("en-Us"));
            user.NormalizedUserName = userForRegisterDto.Email.ToUpper(new CultureInfo("en-Us"));

            IResult userExistsResult = _userService.UserExists(userForRegisterDto.Email);

            if (!userExistsResult.Success)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.AlreadyRegisteredUser,
                    Type = ErrorTypes.Warning,
                    Value = userExistsResult.Message
                });
            }

            IResult registerResult = _userService.Register(user, userForRegisterDto.Password);

            if (!registerResult.Success)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.DefaultError,
                    Type = ErrorTypes.Danger,
                    Value = SecurityMessages.SystemError
                });
            }

            return Ok(registerResult.Message);
        }

        [HttpPost("confrimEmail")]
        public IActionResult ConfirmationEmail([FromBody] EmailDto emailDto)
        {
            User user = _userService.GetByEmail(emailDto.Email);

            if (user == null)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.NotFoundUser,
                    Type = ErrorTypes.Warning,
                    Value = CrudMessages.UserNotFoundForEmail
                });
            }

            if (user.EmailConfirmed)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.AlreadyConfirmatedEmail,
                    Type = ErrorTypes.Danger,
                    Value = SecurityMessages.UserEmailAlreadyConfirmed
                });
            }

            IResult result = _userService.ConfirmationEmail(user);

            if (!result.Success)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.ConfirmationErrorEmail,
                    Type = ErrorTypes.Danger,
                    Value = result.Message
                });
            }

            return Ok(result.Message);
        }

        [HttpPost("activatedEmail")]
        public IActionResult ActivateEmail([FromBody] ActivateEmailDto activateEmailDto)
        {
            IDataResult<User> userResult = _userService.GetById(activateEmailDto.UserId);
            if (!userResult.Success || userResult.Data == null)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.DefaultError,
                    Type = ErrorTypes.Danger,
                    Value = SecurityMessages.SystemError
                });
            }

            IResult result = _userService.ActivateEmail(userResult.Data, activateEmailDto.Token);
            if (!result.Success)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.ActivatedErrorEmail,
                    Type = ErrorTypes.Danger,
                    Value = result.Message
                });
            }

            return Ok(result.Message);
        }

        [HttpPost("forgettingPassword")]
        public IActionResult ForgettingPassword([FromBody] EmailDto emailDto)
        {
            User user = _userService.GetByEmail(emailDto.Email);
            if (user == null)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.NotFoundUser,
                    Type = ErrorTypes.Warning,
                    Value = CrudMessages.UserNotFoundForEmail
                });
            }

            IResult result = _userService.ForgettingPassword(user);
            if (!result.Success)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.ForgettingPasswordError,
                    Type = ErrorTypes.Danger,
                    Value = result.Message
                });
            }

            return Ok(result.Message);
        }

        [HttpPost("resetPasswordForForgetten")]
        public IActionResult ResetPasswordForForgetten([FromBody] ForgettingPasswordDto forgettingPasswordDto)
        {
            IDataResult<User> userResult = _userService.GetById(forgettingPasswordDto.UserId);
            if (!userResult.Success || userResult.Data == null)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.DefaultError,
                    Type = ErrorTypes.Danger,
                    Value = SecurityMessages.SystemError
                });
            }

            IResult result = _userService.ResetPasswordForForgetten(userResult.Data, forgettingPasswordDto.Token, forgettingPasswordDto.Password);
            if (!result.Success)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.ResetPasswordError,
                    Type = ErrorTypes.Danger,
                    Value = result.Message
                });
            }

            return Ok(result.Message);
        }

        [HttpPost("loginWithSocial")]
        public IActionResult LoginWithSocial([FromBody] UserForSocialLoginDto userForSocial)
        {
            if(userForSocial == null)
            {
                return BadRequest();
            }

            User user = new User
            {
                Name = userForSocial.Name,
                Email = userForSocial.Email,
                UserName = userForSocial.Email,
                NormalizedEmail = userForSocial.Email.ToUpper(new CultureInfo("en-Us")),
                NormalizedUserName = userForSocial.Email.ToUpper(new CultureInfo("en-Us")),
                EmailConfirmed = true
            };

            IDataResult<UserLogin> userLoginResult = _userLoginService.GetByProviderKey(userForSocial.Id, userForSocial.Provider);
            if(userLoginResult.Success && userLoginResult.Data == null)
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    UserLogin userLogin = new UserLogin
                    {
                        LoginProvider = userForSocial.Provider,
                        ProviderKey = userForSocial.Id,
                        ProviderDisplayName = userForSocial.Provider
                    };

                    var userDb = _userService.GetByEmail(userForSocial.Email);
                    if (userDb == null)
                    {
                        _userService.RegisterForSocialLogin(user);
                        userLogin.UserId = user.Id;
                    }
                    else
                    {
                        userLogin.UserId = userDb.Id;
                    }

                   
                    IResult userLoginAddResult = _userLoginService.Add(userLogin);
                    if (!userLoginAddResult.Success)
                    {
                        return BadRequest();
                    }

                    transaction.Complete();
                }
            }


            IDataResult<AccessToken> createTokenResult = _userService.CreateAccessTokenForLogin(user, true);
            if (!createTokenResult.Success)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.DefaultError,
                    Type = ErrorTypes.Danger,
                    Value = SecurityMessages.SystemError
                });
            }

            return Ok(createTokenResult);
        }

        [HttpGet("getAccountUser")]
        [Authorize()]
        public IActionResult GetAccountUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string email = identity.FindFirst(ClaimTypes.Email).Value;

            IDataResult<AccountUserDto> accountUserResult = _userService.GetAccountUserInfo(email);
            if(!accountUserResult.Success || accountUserResult.Data == null)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.DefaultError,
                    Type = ErrorTypes.Danger,
                    Value = SecurityMessages.SystemError
                });
            }

            return Ok(accountUserResult.Data);
        }

        [HttpPost("updateUserInformation")]
        [Authorize()]
        public IActionResult UpdateUserInfo([FromBody] UserInformationDto userInformationDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string email = identity.FindFirst(ClaimTypes.Email).Value;

            User user = _userService.GetByEmail(email);
            if(user == null)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.DefaultError,
                    Type = ErrorTypes.Danger,
                    Value = SecurityMessages.SystemError
                });
            }

            if(userInformationDto.Name != null)
            {
                user.Name = userInformationDto.Name;
            }

            if (userInformationDto.City != null)
            {
                user.City = userInformationDto.City;
            }

            if (userInformationDto.Gender != null)
            {
                user.Gender = userInformationDto.Gender;
            }

            if (userInformationDto.BirthDay != null)
            {
                user.BirthDay = userInformationDto.BirthDay;
            }

            IResult updateResult = _userService.Update(user);
            if (!updateResult.Success)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.DefaultError,
                    Type = ErrorTypes.Danger,
                    Value = SecurityMessages.SystemError
                });
            }

            return Ok(updateResult.Message);
        }

        [HttpPost("updateUsername")]
        [Authorize()]
        public IActionResult UpdateUsername([FromBody] UsernameDto usernameDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string email = identity.FindFirst(ClaimTypes.Email).Value;

            User user = _userService.GetByEmail(email);
            if (user == null)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.DefaultError,
                    Type = ErrorTypes.Danger,
                    Value = SecurityMessages.SystemError
                });
            }

            if(!Regex.IsMatch(usernameDto.UserName, "^[a-zA-Z0-9]*$"))
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.DefaultError,
                    Type = ErrorTypes.Danger,
                    Value = SecurityMessages.SystemError
                });
            }

            IResult usernameExistResult = _userService.UsernameExists(usernameDto.UserName);
            if (!usernameExistResult.Success)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.UsernameExistError,
                    Type = ErrorTypes.Warning,
                    Value = usernameExistResult.Message
                });
            }

            user.UserName = usernameDto.UserName;
            user.NormalizedUserName = usernameDto.UserName.ToUpper(new CultureInfo("en-Us"));

            IResult updateResult = _userService.Update(user);
            if (!updateResult.Success)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.DefaultError,
                    Type = ErrorTypes.Danger,
                    Value = SecurityMessages.SystemError
                });
            }

            return Ok(updateResult.Message);
        }

        [HttpPost("updatePassword")]
        [Authorize()]
        public IActionResult UpdatePassword([FromBody] PasswordDto passwordDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string email = identity.FindFirst(ClaimTypes.Email).Value;

            User user = _userService.GetByEmail(email);
            if (user == null)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.DefaultError,
                    Type = ErrorTypes.Danger,
                    Value = SecurityMessages.SystemError
                });
            }

            IResult updateResult = _userService.UpdatePassword(user, passwordDto.Password);
            if (!updateResult.Success)
            {
                return BadRequest(new ErrorResultDto
                {
                    Name = ErrorNames.DefaultError,
                    Type = ErrorTypes.Danger,
                    Value = SecurityMessages.SystemError
                });
            }

            return Ok(updateResult.Message);
        }
    }
}
