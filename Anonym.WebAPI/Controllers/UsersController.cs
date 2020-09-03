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

            IDataResult<AccessToken> createTokenResult = _userService.CreateAccessToken(loginResult.Data);
            if (!createTokenResult.Success)
            {
                return BadRequest();
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
    }
}
