using EzGateway;
using System.Net;
using System.Net.Mail;

namespace SampleMailGateway
{
    public class GmailGateway : IEzGateway
    {
        private MailAddress _from;
        private string _username;
        private string _password;

        public GmailGateway(MailAddress from, string username, string password)
        {
            _from = from;
            _username = username;
            _password = password;
        }

        public void SendMessage(string to, string text)
        {
            SendAnEmail(new MailAddress(to), "", text);
        }

        public void SendMessage(string to, string text, string title)
        {
            SendAnEmail(new MailAddress(to), title, text);
        }

        private void SendAnEmail(MailAddress toAddress, string subject, string body)
        {
            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential()
                {
                    UserName = _username,
                    Password = _password,
                };
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;

                smtpClient.Send(_from.Address, toAddress.Address, subject, body);
            }
        }
    }
}
