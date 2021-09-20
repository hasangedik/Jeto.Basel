using System;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Jeto.Basel.Common.Options;
using Jeto.Basel.Core.Service.Abstract;
using Jeto.Basel.Domain.Contracts;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Jeto.Basel.Core.Service.Concrete
{
    public class EmailService : IEmailService
    {
        private readonly MailServerOptions _mailServerOptions;

        public EmailService(IOptions<MailServerOptions> mailServerOptions)
        {
            _mailServerOptions = mailServerOptions.Value;
        }

        public Task Send(EmailContract contract)
        {
            if (contract.To.IsNullOrEmpty())
                throw new ArgumentException("To list can't be null or empty.");

            if (string.IsNullOrEmpty(contract.Subject))
                throw new ArgumentException("Subject can't be null or empty.");

            if (string.IsNullOrEmpty(contract.Body))
                throw new ArgumentException("Body can't be null or empty.");

            return SendAsync(contract);
        }

        private async Task SendAsync(EmailContract contract)
        {
            var message = new MimeMessage
            {
                Priority = MessagePriority.Urgent,
                Subject = contract.Subject,
                Body = new TextPart(TextFormat.Html) { Text = contract.Body }
            };
            message.From.Add(new MailboxAddress(_mailServerOptions.FromName, _mailServerOptions.FromEmail));
            contract.To.ForEach(x => message.To.Add(new MailboxAddress("Basel User", x)));
            using var client = new SmtpClient();
            await client.ConnectAsync(_mailServerOptions.Host, _mailServerOptions.Port, _mailServerOptions.UseSsl);
            await client.AuthenticateAsync(_mailServerOptions.Username, _mailServerOptions.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}