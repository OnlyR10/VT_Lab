using Naydovich.Domain.Entities;
using Naydovich.Domain.Models;

namespace Naydovich.UI.Services
{
    public interface ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
