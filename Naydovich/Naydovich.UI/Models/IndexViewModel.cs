using Microsoft.AspNetCore.Mvc.Rendering;

namespace Naydovich.UI.Models
{
    public class IndexViewModel
    {
        public int SelectedId { get; set; }
        public List<SelectListItem> Items { get; set; }
    }
}
