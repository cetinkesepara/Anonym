using Anonym.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Anonym.Business.Abstract
{
    public interface IAppIdentityService
    {
        Task<UserForSignUpDto> SignUp(UserForSignUpDto userForSignUpDto);
    }
}
