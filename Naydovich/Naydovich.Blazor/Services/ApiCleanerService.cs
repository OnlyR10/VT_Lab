using Naydovich.Domain.Entities;
using Naydovich.Domain.Models;

namespace Naydovich.Blazor.Services
{
    public class ApiCleanerService(HttpClient http) : ICleanerService<Cleaner>
    {
        List<Cleaner> _cleaners;
        int _currentPage = 1;
        int _totalPages = 1;
        public IEnumerable<Cleaner> Cleaners => _cleaners;
        public int CurrentPage => _currentPage;
        public int TotalPages => _totalPages;
        public event Action ListChanged;
        public async Task GetCleaners(int pageNo, int pageSize)
        {
            // Url сервиса API
            var uri = http.BaseAddress.AbsoluteUri;

            // данные для Query запроса
            var queryData = new Dictionary<string, string> {
                { "pageNo", pageNo.ToString() },
                { "pageSize", pageSize.ToString() }
            };
            var query = QueryString.Create(queryData);
            // Отправить запрос http
            var result = await http.GetAsync(uri + query.Value);
            // В случае успешного ответа
            if (result.IsSuccessStatusCode)
            {
                // получить данные из ответа
                var responseData = await result.Content.ReadFromJsonAsync<ResponseData<CleanerListModel<Cleaner>>>();
                // обновить параметры
                _currentPage = responseData.Data.CurrentPage;
                _totalPages = responseData.Data.TotalPages;
                _cleaners = responseData.Data.Items;
                ListChanged?.Invoke();
            } // В случае ошибки
            else
            {
                _cleaners = null;
                _currentPage = 1;
                _totalPages = 1;
            }
        }
    }
}