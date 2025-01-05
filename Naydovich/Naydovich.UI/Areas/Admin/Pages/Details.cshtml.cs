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
    public class DetailsModel : PageModel
    {
        private readonly Naydovich.Api.Data.AppDbContext _context;

        public DetailsModel(Naydovich.Api.Data.AppDbContext context)
        {
            _context = context;
        }

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
    }
}
