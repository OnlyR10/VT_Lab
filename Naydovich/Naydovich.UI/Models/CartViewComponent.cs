using Microsoft.AspNetCore.Mvc;

namespace Naydovich.UI.Models
{
    public class CartViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Получение информации о корзине (например, из базы данных или сессии)
            // Здесь можно добавить код для получения информации о товарах, сумме и количестве товаров
            var cartInfo = new CartViewModel
            {
                TotalAmount = "00,0 руб",
                ItemCount = 0
            };

            // Возвращаем частичное представление с моделью cartInfo
            return View(cartInfo);
        }
    }

    // Модель для передачи данных о корзине
    public class CartViewModel
    {
        public string TotalAmount { get; set; }
        public int ItemCount { get; set; }
    }
}
