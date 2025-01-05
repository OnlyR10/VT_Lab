namespace Naydovich.Blazor.Services
{
    public interface ICleanerService<T> where T : class
    {
        event Action ListChanged;

        // Список объектов
        IEnumerable<T> Cleaners { get; }
        // Номер текущей страницы
        int CurrentPage { get; }
        // Общее количество страниц
        int TotalPages { get; }
        // Получение списка объектов
        Task GetCleaners(int pageNo = 1, int pageSize = 3);
    }
}
