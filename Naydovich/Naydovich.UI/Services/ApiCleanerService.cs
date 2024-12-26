using Naydovich.Domain.Entities;
using Naydovich.Domain.Models;
using System.Text.Json;

namespace Naydovich.UI.Services
{
    public class ApiCleanerService(HttpClient httpClient) : ICleanerService
    {

        public async Task<ResponseData<List<Cleaner>>> GetCleanerListAsync()
        {
            var result = await httpClient.GetAsync(httpClient.BaseAddress);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<ResponseData<List<Cleaner>>>();
            };

            var response = new ResponseData<List<Cleaner>> { Success = false, ErrorMessage = "Ошибка чтения API" };

            return response;
        }

        public async Task<ResponseData<Cleaner>> CreateCleanerAsync(Cleaner product, IFormFile? formFile)
        {
            var serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // Подготовить объект, возвращаемый методом
            var responseData = new ResponseData<Cleaner>();

            // Послать запрос к API для сохранения объекта
            var response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress, product);

            if (!response.IsSuccessStatusCode)
            {
                responseData.Success = false;
                responseData.ErrorMessage = $"Не удалось создать объект:{response.StatusCode}";

                return responseData;
            }

            // Если файл изображения передан клиентом
            if (formFile != null)
            {
                // получить созданный объект из ответа Api-сервиса
                var cleaner = await response.Content.ReadFromJsonAsync<Cleaner>();

                // создать объект запроса
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}/{cleaner.Id}")
                };

                // Создать контент типа multipart form-data
                var content = new MultipartFormDataContent();

                // создать потоковый контент из переданного файла
                var streamContent = new StreamContent(formFile.OpenReadStream());

                // добавить потоковый контент в общий контент по именем "image"
                content.Add(streamContent, "image", formFile.FileName);

                // поместить контент в запрос
                request.Content = content;

                // послать запрос к Api-сервису
                response = await httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    responseData.Success = false;
                    responseData.ErrorMessage = $"Не удалось сохранить изображение:{response.StatusCode}";
                }
            }

            return responseData;
        }

        public async Task<ResponseData<CleanerListModel<Cleaner>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var uri = httpClient.BaseAddress;
            var queryData = new Dictionary<string, string>();

            queryData.Add("pageNo", pageNo.ToString());

            if (!string.IsNullOrEmpty(categoryNormalizedName))
            {
                queryData.Add("category", categoryNormalizedName);
            }

            var query = QueryString.Create(queryData);
            var result = await httpClient.GetAsync(uri + query.Value);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<ResponseData<CleanerListModel<Cleaner>>>();
            };

            var response = new ResponseData<CleanerListModel<Cleaner>> { Success = false, ErrorMessage = "Ошибка чтения API" };

            return response;
        }
    }
}
