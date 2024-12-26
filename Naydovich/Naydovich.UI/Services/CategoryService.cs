using Naydovich.Domain.Entities;
using Naydovich.Domain.Models;

namespace Naydovich.UI.Services
{
    public class CategoryService : ICategoryService
    {
        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly HttpClient _httpClient;

        public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var url = _httpClient.BaseAddress + "/categories";

            var data = await _httpClient.GetFromJsonAsync<ResponseData<List<Category>>>(url);

            return data;
        }
    }
}
