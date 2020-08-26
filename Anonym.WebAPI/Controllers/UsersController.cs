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

        public UsersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            return Ok();
        }
    }
}
