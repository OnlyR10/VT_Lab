using Naydovich.Domain.Entities;

namespace Naydovich.Domain.Models
{

    public class CleanerListModel<T>
    {
        public List<T> Items { get; set; } = new();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;

        public static implicit operator CleanerListModel<T>(CleanerListModel<Cleaner> v)
        {
            throw new NotImplementedException();
        }
    }
}
