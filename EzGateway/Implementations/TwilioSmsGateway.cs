using EzGateway.Config;
using EzGateway.Interfaces;
using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace EzGateway.Implementations
{
    public class TwilioSmsGateway : IEzGateway
    {
        private TwilioSettings _settings;

        private TwilioSmsGateway(TwilioSettings settings)
        {
            _settings = settings;
            TwilioClient.Init(settings.AccountSid, settings.ApiToken);
        }

        public static TwilioSmsGateway Create(TwilioSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.AccountSid))
                throw new ArgumentNullException(nameof(settings.AccountSid));
            if (string.IsNullOrWhiteSpace(settings.ApiToken))
                throw new ArgumentNullException(nameof(settings.ApiToken));
            if (string.IsNullOrWhiteSpace(settings.Sender))
                throw new ArgumentNullException(nameof(settings.Sender));

            return new TwilioSmsGateway(settings);
        }

        public void SendMessage(string recipient, string messageText)
        {
            ValidateAndSendSmsMessage(recipient, messageText);
        }

        public void SendMessage(string recipient, string messageText, string messageTitle)
        {
            ValidateAndSendSmsMessage(recipient, $"{messageTitle} - {messageText}");
        }

        private void ValidateAndSendSmsMessage(string recipient, string messageText)
        {
            if (string.IsNullOrWhiteSpace(recipient))
                throw new ArgumentNullException(nameof(recipient));
            if (string.IsNullOrWhiteSpace(messageText))
                throw new ArgumentNullException(nameof(messageText));

            if (messageText.Length >= 1600)
                throw new ArgumentOutOfRangeException(nameof(messageText), "Message cannot be longer than 1600 characters");

            SendSmsMessage(recipient, messageText);
        }

        private void SendSmsMessage(string recipient, string messageText)
        {
            MessageResource.Create(
                body: messageText,
                from: new PhoneNumber(_settings.Sender),
                to: new PhoneNumber(recipient)
            );
        }
    }
}
