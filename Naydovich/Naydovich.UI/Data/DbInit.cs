using Microsoft.AspNetCore.Identity;
using Naydovich.UI.Data;
using System.Security.Claims;

public class DbInit
{
    public static async Task SeedData(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Проверяем, существует ли пользователь
        var user = await userManager.FindByEmailAsync("admin@gmail.com");
        if (user == null)
        {
            // Создаем нового пользователя
            user = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user, "Admin123!"); // Установите пароль
            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin")); // Добавляем роль
        }
    }
}