using Anonym.Business.Abstract;
using Anonym.Business.Constants.Messages;
using Anonym.Business.Constants.Settings;
using Anonym.DataAccess.Abstract;
using Anonym.Entities.Concrete;
using Anonym.Entities.Dtos;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anonym.Business.Concrete
{
    public class SettingManager : ISettingService
    {
        private readonly ISettingDal _settingDal;

        public SettingManager(ISettingDal settingDal)
        {
            _settingDal = settingDal;
        }

        public IResult Add(Setting setting)
        {
            _settingDal.Add(setting);
            return new SuccessResult(CrudMessages.SettingAdded);
        }

        public IResult Delete(Setting setting)
        {
            _settingDal.Delete(setting);
            return new SuccessResult(CrudMessages.SettingDeleted);
        }

        public IDataResult<Setting> GetByName(string settingName)
        {
            return new SuccessDataResult<Setting>(_settingDal.Get(s => s.Name == settingName && s.IsMain == true));
        }

        public IDataResult<OptionForSendEmailDto> GetEmailSettings()
        {
            var emailSettings = _settingDal.GetList(s => 
            s.Name == EmailConstants.SmtpHost || 
            s.Name == EmailConstants.SmtpPort ||
            s.Name == EmailConstants.NetworkCredentialUserName ||
            s.Name == EmailConstants.NetworkCredentialPassword ||
            s.Name == EmailConstants.MailMessageFromAddress ||
            s.Name == EmailConstants.MailMessageFromDisplayName && s.IsMain == true).ToList();

            OptionForSendEmailDto optionForSendEmailDto = new OptionForSendEmailDto
            {
                SmtpHost = emailSettings.FirstOrDefault(e => e.Name == EmailConstants.SmtpHost).Value,
                SmtpPort = emailSettings.FirstOrDefault(e => e.Name == EmailConstants.SmtpPort).Value,
                NetworkCredentialUserName = emailSettings.FirstOrDefault(e => e.Name == EmailConstants.NetworkCredentialUserName).Value,
                NetworkCredentialPassword = emailSettings.FirstOrDefault(e => e.Name == EmailConstants.NetworkCredentialPassword).Value,
                MailMessageFromAddress = emailSettings.FirstOrDefault(e => e.Name == EmailConstants.MailMessageFromAddress).Value,
                MailMessageFromDisplayName = emailSettings.FirstOrDefault(e => e.Name == EmailConstants.MailMessageFromDisplayName).Value
            };

            return new SuccessDataResult<OptionForSendEmailDto>(optionForSendEmailDto);
        }

        public IDataResult<List<Setting>> GetList()
        {
            return new SuccessDataResult<List<Setting>>(_settingDal.GetList().ToList());
        }

        public IResult Update(Setting setting)
        {
            _settingDal.Update(setting);
            return new SuccessResult(CrudMessages.SettingUpdated);
        }
    }
}
