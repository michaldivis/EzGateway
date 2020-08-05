using EzGateway;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace SampleTwilioGateway
{
    public class TwilioSmsGateway : IEzGateway
    {
        private string _from;

        public TwilioSmsGateway(string accountSid, string apiToken, string from)
        {
            _from = from;
            TwilioClient.Init(accountSid, apiToken);
        }

        public void SendMessage(string to, string text)
        {
            SendSmsMessage(to, text);
        }

        public void SendMessage(string to, string text, string title)
        {
            SendSmsMessage(to, text);
        }

        private void SendSmsMessage(string to, string text)
        {
            MessageResource.Create(
                body: text,
                from: new PhoneNumber(_from),
                to: new PhoneNumber(to)
            );
        }
    }
}
