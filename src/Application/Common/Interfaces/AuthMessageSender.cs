

namespace MyWebApi.Application.Common.Interfaces
{
    public interface IAuthMessageSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendSmsAsync(string number, string message);

    }
}