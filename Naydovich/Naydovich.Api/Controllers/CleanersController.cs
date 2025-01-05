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
        private readonly IWebHostEnvironment _env;
        public CleanersController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SaveImage(int id, IFormFile image)
        {
            // Найти объект по Id
            var asset = await _context.Cleaners.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }
            // Путь к папке wwwroot/Images
            var imagesPath = Path.Combine(_env.WebRootPath, "Images");
            // получить случайное имя файла
            var randomName = Path.GetRandomFileName();
            // получить расширение в исходном файле
            var extension = Path.GetExtension(image.FileName);
            // задать в новом имени расширение как в исходном файле
            var fileName = Path.ChangeExtension(randomName, extension);
            // полный путь к файлу
            var filePath = Path.Combine(imagesPath, fileName);
            // создать файл и открыть поток для записи
            using var stream = System.IO.File.OpenWrite(filePath);
            // скопировать файл в поток
            await image.CopyToAsync(stream);
            // получить Url хоста
            var host = "https://" + Request.Host;
            // Url файла изображения
            var url = $"{host}/Images/{fileName}";
            // Сохранить url файла в объекте
            asset.Image = url;
            await _context.SaveChangesAsync();
            return Ok();
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

            // Создание объекта CleanerListModel с нужной страницей данных
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
