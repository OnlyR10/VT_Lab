using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Naydovich.Api.Data;
using Naydovich.Domain.Entities;
using Naydovich.Domain.Models;

namespace Naydovich.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CleanersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CleanersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cleaners
        [HttpGet]
        [HttpGet]
        public async Task<ActionResult<ResponseData<CleanerListModel<Cleaner>>>> GetCleaners(string? category, int pageNo = 1, int pageSize = 3)
        {
            // Создать объект результата
            var result = new ResponseData<CleanerListModel<Cleaner>>();

            // Фильтрация по категории загрузка данных категории
            var data = _context.Cleaners.Include(d => d.Category).Where(d => String.IsNullOrEmpty(category) || d.Category.NormalizedName.Equals(category));

            // Подсчет общего количества страниц
            int totalPages = (int)Math.Ceiling(data.Count() / (double)pageSize);

            if (pageNo > totalPages)
                pageNo = totalPages;

            // Создание объекта ProductListModel с нужной страницей данных
            var listData = new CleanerListModel<Cleaner>()
            {
                Items = await data.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };

            // поместить данные в объект результата
            result.Data = listData;

            // Если список пустой
            if (data.Count() == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбранной категории";
            }

            return result;
        }

        // GET: api/Cleaners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cleaner>> GetCleaner(int id)
        {
            var cleaner = await _context.Cleaners.FindAsync(id);

            if (cleaner == null)
            {
                return NotFound();
            }

            return cleaner;
        }

        // PUT: api/Cleaners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCleaner(int id, Cleaner cleaner)
        {
            if (id != cleaner.Id)
            {
                return BadRequest();
            }

            _context.Entry(cleaner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CleanerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cleaners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cleaner>> PostCleaner(Cleaner cleaner)
        {
            _context.Cleaners.Add(cleaner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCleaner", new { id = cleaner.Id }, cleaner);
        }

        // DELETE: api/Cleaners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCleaner(int id)
        {
            var cleaner = await _context.Cleaners.FindAsync(id);
            if (cleaner == null)
            {
                return NotFound();
            }

            _context.Cleaners.Remove(cleaner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CleanerExists(int id)
        {
            return _context.Cleaners.Any(e => e.Id == id);
        }
    }
}
