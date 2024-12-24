using Microsoft.AspNetCore.Mvc;
using Naydovich.Domain.Entities;
using Naydovich.Domain.Models;

namespace Naydovich.Services
{
    public class MemoryCleanerService : ICleanerService
    {
        List<Cleaner> _cleaners;
        List<Category> _categories;
        IConfiguration _config;
        public MemoryCleanerService([FromServices] IConfiguration config, ICategoryService categoryService)
        {
            _config = config;
            _categories = categoryService.GetCategoryListAsync().Result.Data;
            SetupData();
        }
        /// <summary>
        /// Инициализация списков
        /// </summary>
        private void SetupData()
        {
            _cleaners = new List<Cleaner>
            {
                new Cleaner
                {
                    Id = 1,
                    Name = "Hotpoint",
                    Description = "Пылесос SL M07 A4H B",
                    Power = 200,
                    Image = "/images/FloorCleanerHotpoint.png",
                    CategoryId = 1,
                    Category = _categories.Find(c=>c.NormalizedName.Equals("floor"))
                },
                    new Cleaner
                {
                    Id = 2,
                    Name = "Miele",
                    Description = "Пылесос C1",
                    Power = 160,
                    Image = "/images/FloorCleanerMiele.png",
                    CategoryId = 1,
                    Category = _categories.Find(c => c.NormalizedName.Equals("floor")),
                },
                new Cleaner
                {
                    Id = 3,
                    Name = "Zanussi",
                    Description = "Пылесос  ZAN1214",
                    Power = 200,
                    Image = "/images/FloorCleanerZanussi.png",
                    CategoryId = 1,
                    Category = _categories.Find(c => c.NormalizedName.Equals("floor")),
                },
                new Cleaner
                {
                    Id = 4,
                    Name = "Philips",
                    Description = "Ручной пылесос FC6728/01",
                    Power = 180,
                    Image = "/images/HandheldCleanerPhilips.png",
                    CategoryId = 2,
                    Category = _categories.Find(c => c.NormalizedName.Equals("handheld")),
                },
                new Cleaner
                {
                    Id = 5,
                    Name = "Scarlett",
                    Description = "Ручной пылесос SC-VC80H23",
                    Power = 230,
                    Image = "/images/HandheldCleanerScarlett.png",
                    CategoryId = 2,
                    Category = _categories.Find(c => c.NormalizedName.Equals("handheld")),
                },
                new Cleaner
                {
                    Id = 6,
                    Name = "Xiaomi",
                    Description = "Ручной пылесос BHR8195EU",
                    Power = 215,
                    Image = "/images/HandheldCleanerXiaomi.png",
                    CategoryId = 2,
                    Category = _categories.Find(c => c.NormalizedName.Equals("handheld")),
                },
                new Cleaner
                {
                    Id = 7,
                    Name = "Dream",
                    Description = "Робот-пылесос D9 Max Gen 2",
                    Power = 75,
                    Image = "/images/RobotCleanerDream.png",
                    CategoryId = 3,
                    Category = _categories.Find(c => c.NormalizedName.Equals("robot")),
                },
                new Cleaner
                {
                    Id = 8,
                    Name = "Karcher",
                    Description = "Робот-пылесос RCV 3",
                    Power = 60,
                    Image = "/images/RobotCleanerKarcher.png",
                    CategoryId = 3,
                    Category = _categories.Find(c => c.NormalizedName.Equals("robot")),
                },
                new Cleaner
                {
                    Id = 9,
                    Name = "Xiaomi",
                    Description = "Робот-пылесос Vacuum S20+",
                    Power = 55,
                    Image = "/images/RobotCleanerXiaomi.png",
                    CategoryId = 3,
                    Category = _categories.Find(c => c.NormalizedName.Equals("robot")),
                },
                //new Cleaner
                //{
                //    Id = 10,
                //    Name = "Dream",
                //    Description = "Робот-пылесос D9 Max Gen 2",
                //    Power = 75,
                //    Image = "/images/RobotCleanerDream.png",
                //    CategoryId = 1,
                //    Category = _categories.Find(c => c.NormalizedName.Equals("floor")),
                //},
                //new Cleaner
                //{
                //    Id = 11,
                //    Name = "Karcher",
                //    Description = "Робот-пылесос RCV 3",
                //    Power = 60,
                //    Image = "/images/RobotCleanerKarcher.png",
                //    CategoryId = 2,
                //    Category = _categories.Find(c => c.NormalizedName.Equals("handheld")),
                //},
                //new Cleaner
                //{
                //    Id = 12,
                //    Name = "Xiaomi",
                //    Description = "Робот-пылесос Vacuum S20+",
                //    Power = 55,
                //    Image = "/images/RobotCleanerXiaomi.png",
                //    CategoryId = 3,
                //    Category = _categories.Find(c => c.NormalizedName.Equals("robot")),
                //},
            };
        }

        public Task<ResponseData<CleanerListModel<Cleaner>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            // Создать объект результата
            var result = new ResponseData<CleanerListModel<Cleaner>>();
            // Id категории для фильрации
            int? categoryId = null;
            // если требуется фильтрация, то найти Id категории
            // с заданным categoryNormalizedName
            if (categoryNormalizedName != null)
                categoryId = _categories.Find(c => c.NormalizedName.Equals(categoryNormalizedName))?.Id;
            // Выбрать объекты, отфильтрованные по ID категории,
            // если этот ID имеется
            var data = _cleaners.Where(d => categoryId == null || d.Category.Id.Equals(categoryId))?.ToList();

            // получить размер страницы из конфигурации
            int pageSize = _config.GetSection("ItemsPerPage").Get<int>();
            // получить общее количество страниц
            int totalPages = (int)Math.Ceiling(data.Count / (double)pageSize);
            // получить данные страницы
            var listData = new CleanerListModel<Cleaner>()
            {
                Items = data.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList(),
                //Items = data.ToList(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };
            // поместить данные в объект результата
            result.Data = listData;

            // Если список пустой
            if (data.Count == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбраннной категории";
            }
            // Вернуть результат
            return Task.FromResult(result);
        }
    }
}


