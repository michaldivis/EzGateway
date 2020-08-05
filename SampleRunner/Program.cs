using EzGateway.Config;
using EzGateway.Implementations;
using EzGateway.Interfaces;
using SampleRunner.Config;
using System;
using System.Net.Mail;

namespace SampleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var title = "Test message title";
            var message = "Test message text";

            IEzGateway smsGateway = TwilioSmsGateway.Create(new TwilioSettings(TwilioConstants.AccountSid, TwilioConstants.ApiToken, TwilioConstants.SenderPhoneNumber));
            smsGateway.SendMessage("PUT_A_VALID_PHONE_NUMBER_HERE", message);

            IEzGateway mailGateway = GmailGateway.Create(new GmailSettings(new MailAddress(GmailConstants.SenderEmailAddress, GmailConstants.SenderAlias), GmailConstants.SenderEmailAddress, GmailConstants.SenderPassword));
            mailGateway.SendMessage("PUT_A_VALID_EMAIL_ADDRESS_HERE", message, title);

            Console.ReadKey();
        }
    }
}
