using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Naydovich.Api.Data;
using Naydovich.Domain.Entities;

namespace Naydovich.UI.Areas_Admin_Pages
{
    public class DeleteModel : PageModel
    {
        private readonly Naydovich.Api.Data.AppDbContext _context;

        public DeleteModel(Naydovich.Api.Data.AppDbContext context)
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
            else
            {
                Cleaner = cleaner;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cleaner = await _context.Cleaners.FindAsync(id);
            if (cleaner != null)
            {
                Cleaner = cleaner;
                _context.Cleaners.Remove(Cleaner);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
