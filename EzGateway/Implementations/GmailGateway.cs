using EzGateway.Config;
using EzGateway.Interfaces;
using System;
using System.Net;
using System.Net.Mail;

namespace EzGateway.Implementations
{
    public class GmailGateway : IEzGateway
    {
        private GmailSettings _settings;

        private GmailGateway(GmailSettings settings)
        {
            _settings = settings;
        }

        public static GmailGateway Create(GmailSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.Username))
                throw new ArgumentNullException(nameof(settings.Username));
            if (string.IsNullOrWhiteSpace(settings.Password))
                throw new ArgumentNullException(nameof(settings.Password));
            if (settings.Sender == null)
                throw new ArgumentNullException(nameof(settings.Sender.Address));
            if (string.IsNullOrWhiteSpace(settings.Sender.Address))
                throw new ArgumentNullException(nameof(settings.Sender.Address));

            return new GmailGateway(settings);
        }

        public void SendMessage(string recipient, string messageText)
        {
            ValidateAndSendAnEmail(recipient, "", messageText);
        }

        public void SendMessage(string recipient, string messageText, string messageTitle)
        {
            ValidateAndSendAnEmail(recipient, messageTitle, messageText);
        }

        private void ValidateAndSendAnEmail(string recipient, string subject, string body)
        {
            if (string.IsNullOrWhiteSpace(recipient))
                throw new ArgumentNullException(nameof(recipient));
            if (subject == null)
                throw new ArgumentNullException(nameof(subject));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if (body.Length >= 10000)
                throw new ArgumentOutOfRangeException(nameof(body), "Body cannot be longer than 10000 characters");

            var recipientAddress = new MailAddress(recipient);

            SendAnEmail(recipientAddress, subject, body);
        }

        private void SendAnEmail(MailAddress recipient, string subject, string body)
        {
            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential()
                {
                    UserName = _settings.Username,
                    Password = _settings.Password,
                };
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;

                smtpClient.Send(new MailMessage(_settings.Sender, recipient)
                {
                    Subject = subject,
                    Body = body
                });
            }
        }
    }
}
