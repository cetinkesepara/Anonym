using Anonym.Business.Abstract;
using Anonym.Entities.Concrete;
using Anonym.Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Anonym.Business.Concrete
{
    public class AppIdentityManager : IAppIdentityService
    {
        private readonly UserManager<AppUser> _userManager;

        public AppIdentityManager(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserForSignUpDto> SignUp(UserForSignUpDto userForSignUpDto)
        {
            AppUser user = new AppUser
            {
                UserName = userForSignUpDto.UserName,
                Email = userForSignUpDto.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, userForSignUpDto.Password);

            return userForSignUpDto;
        }
    }
}
