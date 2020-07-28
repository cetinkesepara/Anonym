using Anonym.Business.Abstract;
using Anonym.Business.Constants.Messages;
using Anonym.Entities.Concrete;
using Anonym.Entities.Dtos;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
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

        public async Task<IDataResult<UserForSignUpDto>> SignUp(UserForSignUpDto userForSignUpDto)
        {
            AppUser user = new AppUser
            {
                UserName = userForSignUpDto.UserName,
                Email = userForSignUpDto.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, userForSignUpDto.Password);

            if (result.Succeeded == false)
            {
                return new ErrorDataResult<UserForSignUpDto>(userForSignUpDto, IdentityMessages.UserSignUpError);
            }

            return new SuccessDataResult<UserForSignUpDto>(userForSignUpDto, IdentityMessages.UserSignUpSuccess);
        }
    }
}
