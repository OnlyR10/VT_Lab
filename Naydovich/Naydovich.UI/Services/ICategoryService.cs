using Naydovich.Domain.Entities;
using Naydovich.Domain.Models;

namespace Naydovich.Services
{
    public interface ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
