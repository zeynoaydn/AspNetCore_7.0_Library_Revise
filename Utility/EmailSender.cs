using Microsoft.AspNetCore.Identity.UI.Services;

namespace Mvc_Project.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //ileride email hosting işlemlerini burada yapabilirsin
            return Task.CompletedTask;
        }
    }
}
