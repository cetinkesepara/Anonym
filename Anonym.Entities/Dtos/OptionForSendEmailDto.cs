using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Entities.Dtos
{
    public class OptionForSendEmailDto : IDto
    {
        public string SmtpHost { get; set; }
        public string SmtpPort { get; set; }
        public string NetworkCredentialUserName { get; set; }
        public string NetworkCredentialPassword { get; set; }
        public string MailMessageFromAddress { get; set; }
        public string MailMessageFromDisplayName { get; set; }
    }
}
