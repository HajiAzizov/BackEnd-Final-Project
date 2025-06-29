using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class WishlistController : Controller
{
    private readonly IWishlistService _wishlistService;
    private readonly UserManager<AppUser> _userManager;

    public WishlistController(IWishlistService wishlistService, UserManager<AppUser> userManager)
    {
        _wishlistService = wishlistService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var wishlist = await _wishlistService.GetAllAsync(user.Id);
        return View(wishlist);
    }

    public async Task<IActionResult> Add(int productId)
    {
        var userId = _userManager.GetUserId(User);
        await _wishlistService.AddAsync(userId, productId);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int productId)
    {
        var user = await _userManager.GetUserAsync(User);
        await _wishlistService.RemoveAsync(user.Id, productId);
        return RedirectToAction("Index");
    }
}
