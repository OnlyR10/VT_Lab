using Microsoft.AspNetCore.Mvc.Rendering;

namespace Naydovich.UI.ViewModels
{
    public class IndexViewModel
    {
        public int SelectedId { get; set; }
        //public List<SelectListItem> Items { get; set; }
        public SelectList Items { get; set; }
    }
}
