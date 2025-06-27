using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.ViewComponents
{
    public class BasketViewComponent : ViewComponent
    {
        private readonly IBasketService _basketService;

        public BasketViewComponent(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var basketItems = await _basketService.GetBasketItemsAsync();
            return View(basketItems);
        }
    }
}
