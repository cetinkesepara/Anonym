using Anonym.Entities.Dtos;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Anonym.Business.Abstract
{
    public interface IAppIdentityService
    {
        Task<IDataResult<UserForSignUpDto>> SignUp(UserForSignUpDto userForSignUpDto);
    }
}
