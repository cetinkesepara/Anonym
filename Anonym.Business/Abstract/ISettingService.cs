using Anonym.Entities.Concrete;
using Anonym.Entities.Dtos;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Business.Abstract
{
    public interface ISettingService
    {
        IDataResult<List<Setting>> GetList();
        IDataResult<Setting> GetByName(string settingName);
        IDataResult<OptionForSendEmailDto> GetEmailSettings();
        IResult Add(Setting setting);
        IResult Delete(Setting setting);
        IResult Update(Setting setting);
    }
}
