using GSES2_BTC.Core.Contracts.Data;
using GSES2_BTC.Core.Contracts.Data.Messaging;
using GSES2_BTC.Core.Contracts.Service.MessagingProvider;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;

namespace SmtpGoogleProvider
{
    public class EmailSender : IMessageSender
    {
        private readonly MailSettings _mailSettings;
        public EmailSender(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task<List<Reciever>> SendMessage(List<Reciever> recievers, Message message)
        {
            List<Reciever> result = new List<Reciever>();
            foreach (var item in recievers)
            {
                var notRecivedTo = await Send(item, message);
                if (notRecivedTo != null)
                {
                    result.Add(notRecivedTo);
                }
            }
            return result;
        }

        private async Task<Reciever?> Send(Reciever to, Message message)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(to.Email));
                email.Subject = message.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = string.Format(@"<p>{0}<br>
                    <p><b>1 {1} is {2} {3}</b><br>
                    <p>Rate source: {4}<br>
                    <p>{5}", message.Body, message.From, message.Rate, message.To, message.ToSourceUrl, message.Footer);
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                var res = await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return null;
            }
            catch (Exception)
            {
                return to;
                throw;
            }
        }
    }
}