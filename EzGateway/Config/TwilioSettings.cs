namespace EzGateway.Config
{
    public class TwilioSettings
    {
        public string AccountSid { get; set; }
        public string ApiToken { get; set; }
        public string Sender { get; set; }

        public TwilioSettings(string accountSid, string apiToken, string sender)
        {
            AccountSid = accountSid;
            ApiToken = apiToken;
            Sender = sender;
        }
    }
}
