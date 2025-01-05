using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Naydovich.Domain.Entities;
using Naydovich.UI.Services;

namespace Naydovich.UI.Areas_Admin_Pages
{
    [Authorize(Policy = "admin")]
    public class IndexModel : PageModel
    {
        private readonly ICleanerService _cleanerService;
        public IndexModel(ICleanerService cleanerService)
        {
            //_context = context;
            _cleanerService = cleanerService;
        }
        public List<Cleaner> Cleaner { get; set; } = default!;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public async Task OnGetAsync(int? pageNo = 1)
        {
            var response = await _cleanerService.GetProductListAsync(null, pageNo.Value);
            if (response.Success)
            {
                Cleaner = response.Data.Items;
                CurrentPage = response.Data.CurrentPage;
                TotalPages = response.Data.TotalPages;
            }
        }
    }
}