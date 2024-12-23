namespace Naydovich.Domain.Models
{

    public class CleanerListModel<T>
    {
        public List<T> Items { get; set; } = new();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
    }
}
