// Install the C# / .NET helper library from twilio.com/docs/csharp/install

using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace SmsService;
public class SmsToClient
{
    private readonly string _phoneNumber;
    private readonly string _message;
    private readonly string _twilloPhone = "+15307231577";

    public SmsToClient(string phoneNumber, string message)
    {
        _phoneNumber = phoneNumber;
        _message = message;
    }

    public async Task SendSmsAsync()
    {
        string accountSid = "AC6e9294eba0df2777fff88a4454b084c4";
        string authToken = "27ecc3c40c6732c77d68939d8b648309";

        TwilioClient.Init(accountSid, authToken);

            await MessageResource.CreateAsync(
            body: _message,
            from: new Twilio.Types.PhoneNumber(_twilloPhone),
            to: new Twilio.Types.PhoneNumber(_phoneNumber)
        );
    }
}

