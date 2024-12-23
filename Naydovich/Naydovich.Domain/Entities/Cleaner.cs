namespace Naydovich.Domain.Entities
{
    public class Cleaner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Power { get; set; }
        public string? Image { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
