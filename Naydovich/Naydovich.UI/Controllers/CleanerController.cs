using Microsoft.AspNetCore.Mvc;
using Naydovich.Services;

namespace Naydovich.UI.Controllers
{
    public class CleanerController(ICleanerService cleanerService, ICategoryService categoryService) : Controller
    {
        [Route("Cleaner")]
        [Route("Cleaner/{category}")]
        public async Task<IActionResult> Index(string? category, int pageNo = 1)
        {
            // получить список категорий
            var categoriesResponse = await categoryService.GetCategoryListAsync();

            // если список не получен, вернуть код 404
            if (!categoriesResponse.Success)
                return NotFound(categoriesResponse.ErrorMessage);

            // передать список категорий во ViewData
            ViewData["categories"] = categoriesResponse.Data;

            // передать во ViewData имя текущей категории
            var currentCategory = category == "Все" ? "Все" : categoriesResponse.Data.FirstOrDefault(c => c.NormalizedName == category)?.Name;

            if (currentCategory == null) currentCategory = "Все";

            ViewData["currentCategory"] = currentCategory;

            var productResponse = await cleanerService.GetProductListAsync(category, pageNo);

            if (!productResponse.Success)
                ViewData["Error"] = productResponse.ErrorMessage;
            return View(productResponse.Data);
        }
    }
}
