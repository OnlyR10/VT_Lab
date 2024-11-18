using Microsoft.AspNetCore.Identity.UI.Services;

public class NoOpEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        // Имитация отправки email, ничего не делаем
        return Task.CompletedTask;
    }
}