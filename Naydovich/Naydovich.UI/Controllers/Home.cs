using Microsoft.AspNetCore.Mvc;

namespace Naydovich.UI.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
