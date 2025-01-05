using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Naydovich.Domain.Entities;

namespace Naydovich.UI.Areas_Admin_Pages
{
    public class EditModel : PageModel
    {
        private readonly Naydovich.Api.Data.AppDbContext _context;

        public EditModel(Naydovich.Api.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cleaner Cleaner { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cleaner = await _context.Cleaners.FirstOrDefaultAsync(m => m.Id == id);
            if (cleaner == null)
            {
                return NotFound();
            }
            Cleaner = cleaner;
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Cleaner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CleanerExists(Cleaner.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CleanerExists(int id)
        {
            return _context.Cleaners.Any(e => e.Id == id);
        }
    }
}
