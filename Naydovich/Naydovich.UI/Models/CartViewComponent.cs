using Microsoft.AspNetCore.Mvc;

namespace Naydovich.UI.Models
{
    public class CartViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cartInfo = new CartViewModel
            {
                TotalAmount = "00,0 руб",
                ItemCount = 0
            };

            return View(cartInfo);
        }
    }

    public class CartViewModel
    {
        public string TotalAmount { get; set; }
        public int ItemCount { get; set; }
    }
}
