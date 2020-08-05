# EzGateway
An abstraction layer for sending SMS (and other) messages. Including some implementations.

## Currently supported platforms:
- SMS via Twilio
- E-mail via Gmail

## Future plans
- Return a status from the SendMessage method (Success, Error, etc.)
- Add async versions of the SendMessage method
- Implement more messaging platforms (Facebook, Viber, WhatsApp, ...)

## Tutorial

### How to use

Add the using statements required
```csharp
using EzGateway.Config;
using EzGateway.Implementations;
using EzGateway.Interfaces;
```

In order to use the TwilioSmsGateway implementation a Twilio account is required. More information about that is [here](https://www.twilio.com/docs/usage/tutorials/how-to-use-your-free-trial-account).

Create a Twilio SMS gateway
```csharp
IEzGateway smsGateway = TwilioSmsGateway.Create(new TwilioSettings("TWILIO_ACCOUNT_SID", "TWILIO_API_TOKEN", "TWILIO_SENDER_PHONE_NUMBER"));
```

Send an SMS message
```csharp
smsGateway.SendMessage("RECIPIENT_PHONE_NUMBER", "SMS_MESSAGE_TEXT");
```

### How to run the unit test project
In order to run the unit test project (EzGateway.Tests), you need to replace the configuration placeholders with your own values.

Go to the file EzGateway.Tests/Config/GmailTestConstants.cs and replace the placeholders:
```csharp
public static class GmailTestConstants
{
    public const string SenderAlias = "TEST Email Account";
    public const string SenderEmailAddress = "PUT_YOUR_GMAIL_ADDRESS_HERE"; //replace this with your sender Gmail account email address
    public const string SenderPassword = "PUT_YOUR_GMAIL_PASSWORD_HERE"; //replace this with your sender Gmail account password
    public const string RecipientEmailAddress = "PUT_YOUR_RECIPIENT_EMAIL_ADDRESS_HERE"; //replace this with any valid e-mail address
}
```

Go to the file EzGateway.Tests/Config/TwilioTestConstants.cs and replace the placeholders:
```csharp
public static class TwilioTestConstants
{
    public const string AccountSid = "PUT_YOUR_TWILIO_ACCOUNT_SID_HERE"; //replace this with your Twillio account SID
    public const string ApiToken = "PUT_YOUR_TWILIO_API_TOKEN_HERE"; //replace this with your Twilio API token
    public const string SenderPhoneNumber = "PUT_YOUR_TWILIO_PHONE_NUMBER_HERE"; //replace this with your Twilio generated phone number
    public const string RecipientPhoneNumber = "PUT_YOUR_RECIPIENT_PHONE_NUMBER_HERE"; //replace this with any valid phone number
}
```

You can now run the unit testing project.

### How to run the sample project
Playing with the exising implementations is already prepared for you in the SampleRunner project. In order to run it, you need to replace the configuration placeholders just like you did for the unit test project.

Go to the file SampleRunner/Config/GmailConstants.cs and replace the placeholders:
```csharp
public static class GmailConstants
{
    public const string SenderAlias = "TEST Email Account";
    public const string SenderEmailAddress = "PUT_YOUR_GMAIL_ADDRESS_HERE"; //replace this with your sender Gmail account email address
    public const string SenderPassword = "PUT_YOUR_GMAIL_PASSWORD_HERE"; //replace this with your sender Gmail account password
}
```

Go to the file SampleRunner/Config/TwilioConstants.cs and replace the placeholders:
```csharp
public static class TwilioConstants
{
    public const string AccountSid = "PUT_YOUR_TWILIO_ACCOUNT_SID_HERE"; //replace this with your Twillio account SID
    public const string ApiToken = "PUT_YOUR_TWILIO_API_TOKEN_HERE"; //replace this with your Twilio API token
    public const string SenderPhoneNumber = "PUT_YOUR_TWILIO_PHONE_NUMBER_HERE"; //replace this with your Twilio generated phone number
}
```

Finally, go to the SampleRunner/Program.cs file and replace the placeholders there, as well.
```csharp
var title = "Test message title";
var message = "Test message text";

IEzGateway smsGateway = TwilioSmsGateway.Create(new TwilioSettings(TwilioConstants.AccountSid, TwilioConstants.ApiToken, TwilioConstants.SenderPhoneNumber));
smsGateway.SendMessage("PUT_A_VALID_PHONE_NUMBER_HERE", message); //replace the placeholder with a valid phone number

IEzGateway mailGateway = GmailGateway.Create(new GmailSettings(new MailAddress(GmailConstants.SenderEmailAddress, GmailConstants.SenderAlias), GmailConstants.SenderEmailAddress, GmailConstants.SenderPassword));
mailGateway.SendMessage("PUT_A_VALID_EMAIL_ADDRESS_HERE", message, title); //replace the placeholder with a valid email address
```

You can now run the code. You should be receiving an SMS and an e-mail message after the SampleRunner finishes.
