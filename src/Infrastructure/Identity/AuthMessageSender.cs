
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;


namespace MyWebApi.Infrastructure.Identity
{
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public AuthMessageSender(IOptions<SMSoptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public SMSoptions Options { get; }  // set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            ASPSMS.SMS SMSSender = new ASPSMS.SMS();

            SMSSender.Userkey = Options.SMSAccountIdentification;
            SMSSender.Password = Options.SMSAccountPassword;
            SMSSender.Originator = Options.SMSAccountFrom;

            SMSSender.AddRecipient(number);
            SMSSender.MessageData = message;

            SMSSender.SendTextSMS();

            return Task.FromResult(0);
        }
    }
    public class SMSoptions
    {
        public string? SMSAccountIdentification { get; set; }
        public string? SMSAccountPassword { get; set; }
        public string? SMSAccountFrom { get; set; }
    }
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);

    }
}