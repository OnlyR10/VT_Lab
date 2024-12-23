using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Naydovich.UI.Models;
using Naydovich.UI.ViewModels;

namespace Naydovich.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var items = new List<ListDemo>
            {
                new ListDemo { Id = 1, Name = "Item 1" },
                new ListDemo { Id = 2, Name = "Item 2" },
                new ListDemo { Id = 3, Name = "Item 3" },
            };

            //var viewModel = new IndexViewModel
            //{
            //    Items = items.ConvertAll(item => new SelectListItem
            //    {
            //        Value = item.Id.ToString(),
            //        Text = item.Name
            //    })
            //};

            //return View(viewModel);

            var selectList = new SelectList(items, "Id", "Name");

            var model = new IndexViewModel
            {
                Items = selectList
            };
            return View(model);
        }

        [Authorize(Policy = "admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }
    }
}
