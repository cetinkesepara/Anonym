using Anonym.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Anonym.Business.Utilities.Helpers
{
    public static class EmailHelper
    {
        public static void SendEmail(EmailSendDto confirmationDto)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(confirmationDto.MailMessageFromAddress, confirmationDto.MailMessageFromDisplayName);
            mail.To.Add(confirmationDto.RecipientEmail);
            mail.Subject = confirmationDto.MailSubject;
            mail.Body = confirmationDto.MailBody;
            mail.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient(confirmationDto.SmtpHost, Convert.ToInt32(confirmationDto.SmtpPort));
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(confirmationDto.NetworkCredentialUserName, confirmationDto.NetworkCredentialPassword);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            smtpClient.Send(mail);
        }
    }
}
