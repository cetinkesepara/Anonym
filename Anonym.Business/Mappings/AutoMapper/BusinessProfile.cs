using Anonym.Entities.Concrete;
using Anonym.Entities.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.Mappings.AutoMapper
{
    public class BusinessProfile:Profile
    {
        public BusinessProfile()
        {
            CreateMap<CategoryForAddDto, Category>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<OptionForSendEmailDto, EmailSendDto>();
            CreateMap<User, AccountUserDto>();
        }
    }
}
