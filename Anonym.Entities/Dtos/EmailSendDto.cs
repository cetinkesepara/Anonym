using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.Entities.Dtos
{
    public class EmailSendDto : OptionForSendEmailDto, IDto
    {
        public string RecipientEmail { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
    }
}
