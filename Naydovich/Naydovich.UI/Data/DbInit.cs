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
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user, "superAdmin");
            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin"));
        }
    }
}