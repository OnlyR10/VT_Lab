using Naydovich.Domain.Entities;

namespace Naydovich.Api.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            // Uri проекта
            var uri = "https://localhost:7002/";
            // Получение контекста БД
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            // Заполнение данными
            if (!context.Categories.Any() && !context.Cleaners.Any())
            {
                var categories = new Category[]
                {
                    new Category { Name = "Напольные", NormalizedName= "floor" },
                    new Category { Name = "Ручные", NormalizedName= "handheld" },
                    new Category { Name = "Роботы", NormalizedName= "robot" },
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();

                var cleaners = new List<Cleaner>
                {
                    new Cleaner
                    {
                        Name = "Hotpoint",
                        Description = "Пылесос SL M07 A4H B",
                        Power = 200,
                        Image = uri + "Images/FloorCleanerHotpoint.png",
                        Category = categories.FirstOrDefault(c=>c.NormalizedName.Equals("floor")),
                    },
                    new Cleaner
                    {
                        Name = "Miele",
                        Description = "Пылесос C1",
                        Power = 160,
                        Image = uri + "Images/FloorCleanerMiele.png",
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("floor")),
                    },
                    new Cleaner
                    {
                        Name = "Zanussi",
                        Description = "Пылесос  ZAN1214",
                        Power = 200,
                        Image = uri + "Images/FloorCleanerZanussi.png",
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("floor")),
                    },
                    new Cleaner
                    {
                        Name = "Philips",
                        Description = "Ручной пылесос FC6728/01",
                        Power = 180,
                        Image = uri + "Images/HandheldCleanerPhilips.png",
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("handheld")),
                    },
                    new Cleaner
                    {
                        Name = "Scarlett",
                        Description = "Ручной пылесос SC-VC80H23",
                        Power = 230,
                        Image = uri + "Images/HandheldCleanerScarlett.png",
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("handheld")),
                    },
                    new Cleaner
                    {
                        Name = "Xiaomi",
                        Description = "Ручной пылесос BHR8195EU",
                        Power = 215,
                        Image = uri + "Images/HandheldCleanerXiaomi.png",
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("handheld")),
                    },
                    new Cleaner
                    {
                        Name = "Dream",
                        Description = "Робот-пылесос D9 Max Gen 2",
                        Power = 75,
                        Image = uri + "Images/RobotCleanerDream.png",
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("robot")),
                    },
                    new Cleaner
                    {
                        Name = "Karcher",
                        Description = "Робот-пылесос RCV 3",
                        Power = 60,
                        Image = uri + "Images/RobotCleanerKarcher.png",
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("robot")),
                    },
                    new Cleaner
                    {
                        Name = "Xiaomi",
                        Description = "Робот-пылесос Vacuum S20+",
                        Power = 55,
                        Image = uri + "Images/RobotCleanerXiaomi.png",
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("robot")),
                    },
                };

                await context.AddRangeAsync(cleaners);
                await context.SaveChangesAsync();
            }
        }
    }
}

