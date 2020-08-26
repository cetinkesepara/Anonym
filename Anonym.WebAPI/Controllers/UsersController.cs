using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anonym.Business.Constants.Messages;
using Anonym.Entities.Concrete;
using Anonym.Entities.Dtos;
using AutoMapper;
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
        private readonly UserManager<AppUser> _userManager;

        public UsersController(IMapper mapper, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            var user = _mapper.Map<AppUser>(userForRegisterDto);
            user.UserName = userForRegisterDto.Email;

            IdentityResult result = await _userManager.CreateAsync(user, userForRegisterDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new SuccessResult(CrudMessages.UserAdded));
        }
    }
}
