using System.Net.Mail;

namespace EzGateway.Config
{
    public class GmailSettings
    {
        public MailAddress Sender { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public GmailSettings(MailAddress sender, string username, string password)
        {
            Sender = sender;
            Username = username;
            Password = password;
        }
    }
}
