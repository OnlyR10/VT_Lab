﻿using Microsoft.AspNetCore.Identity.UI.Services;

namespace Naydovich.UI.Services
{
    public class NoOpEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}