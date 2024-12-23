using Microsoft.AspNetCore.Identity;
using Naydovich.UI.Data;
using System.Security.Claims;

public class DbInit
{
    public static async Task SeedData(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var user = await userManager.FindByEmailAsync("admin@gmail.com");
        if (user == null)
        {
            user = new ApplicationUser
            {
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(user, "123456");
            if (!createResult.Succeeded)
            {
                throw new Exception($"Ошибка создания пользователя: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
            }

            var claimResult = await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin"));
            if (!claimResult.Succeeded)
            {
                throw new Exception($"Ошибка добавления клейма: {string.Join(", ", claimResult.Errors.Select(e => e.Description))}");
            }
        }
    }
}