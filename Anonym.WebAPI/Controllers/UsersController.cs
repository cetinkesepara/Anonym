using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Anonym.Business.Abstract;
using Anonym.Business.Constants.Messages;
using Anonym.Business.Utilities.Security.Jwt.Concrete;
using Anonym.Entities.Concrete;
using Anonym.Entities.Dtos;
using AutoMapper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
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

        public UsersController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserForLoginDto userForLoginDto)
        {
            IDataResult<User> loginResult = _userService.Login(userForLoginDto);
            if (!loginResult.Success)
            {
                return Unauthorized(loginResult.Message);
            }

            IResult confirmEmailResult = _userService.IsConfirmEmail(userForLoginDto.Email);
            if (!confirmEmailResult.Success)
            {
                return BadRequest(confirmEmailResult.Message);
            }

            IDataResult<AccessToken> createTokenResult = _userService.CreateAccessToken(loginResult.Data);
            if (!createTokenResult.Success)
            {
                return StatusCode(500);
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
                return BadRequest(userExistsResult.Message);
            }

            IResult registerResult = _userService.Register(user, userForRegisterDto.Password);

            if (!registerResult.Success)
            {
                return BadRequest();
            }

            return Ok(registerResult.Message);
        }

        [HttpPost("confrimEmail")]
        public IActionResult ConfirmationEmail([FromBody] EmailDto emailDto)
        {
            User user = _userService.GetByEmail(emailDto.Email);

            if (user == null)
            {
                return BadRequest(CrudMessages.UserNotFoundForEmail);
            }

            if (user.EmailConfirmed)
            {
                return BadRequest(SecurityMessages.UserEmailAlreadyConfirmed);
            }

            IResult result = _userService.ConfirmationEmail(user);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpPost("activatedEmail")]
        public IActionResult ActivateEmail([FromBody] ActivateEmailDto activateEmailDto)
        {
            IDataResult<User> userResult = _userService.GetById(activateEmailDto.UserId);
            if (!userResult.Success || userResult.Data == null)
            {
                return BadRequest(SecurityMessages.SystemError);
            }

            IResult result = _userService.ActivateEmail(userResult.Data, activateEmailDto.Token);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
    }
}
