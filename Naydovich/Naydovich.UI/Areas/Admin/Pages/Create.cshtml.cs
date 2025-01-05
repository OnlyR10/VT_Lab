using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Naydovich.Domain.Entities;
using Naydovich.UI.Services;

namespace Naydovich.UI.Areas_Admin_Pages
{
    public class CreateModel(ICategoryService categoryService, ICleanerService cleanerService) : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            var categoryListData = await categoryService.GetCategoryListAsync();

            ViewData["CategoryId"] = new SelectList(categoryListData.Data, "Id", "Name");

            return Page();
        }

        [BindProperty]
        public Cleaner Cleaner { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await cleanerService.CreateCleanerAsync(Cleaner, Image);

            return RedirectToPage("./Index");
        }
    }
}
