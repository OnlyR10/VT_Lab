using Naydovich.Domain.Entities;
using Naydovich.Domain.Models;

namespace Naydovich.Services
{
    public class MemoryCategoryService : ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = new List<Category>
            {
                new Category {Id=1, Name="Напольные",
                NormalizedName="floor"},
                new Category {Id=2, Name="Ручные",
                NormalizedName="handheld"},
                new Category {Id=3, Name="Роботы",
                NormalizedName="robot"},
            };

            var result = new ResponseData<List<Category>>();
            result.Data = categories;

            return Task.FromResult(result);
        }
    }
}
