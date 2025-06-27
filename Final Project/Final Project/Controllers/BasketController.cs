using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly UserManager<AppUser> _userManager;
        public BasketController(IBasketService basketService, UserManager<AppUser> userManager)
        {
            _basketService = basketService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var items = await _basketService.GetUserBasketItemsAsync(userId);
            return View(items); // burda view olacaq
        }
        public async Task<IActionResult> Add(int productId)
        {
            var userId = _userManager.GetUserId(User);
            await _basketService.AddToBasketAsync(userId, productId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(int productId)
        {
            var userId = _userManager.GetUserId(User);
            await _basketService.RemoveItemAsync(userId, productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeQuantity(int productId, int quantity)
        {
            var userId = _userManager.GetUserId(User);
            await _basketService.ChangeQuantityAsync(userId, productId, quantity);
            return Ok();
        }
    }
}
