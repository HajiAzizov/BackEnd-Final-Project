using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.User.Wishlist;

public class WishlistViewComponent : ViewComponent
{
    private readonly IWishlistService _wishlistService;
    private readonly UserManager<AppUser> _userManager;

    public WishlistViewComponent(IWishlistService wishlistService, UserManager<AppUser> userManager)
    {
        _wishlistService = wishlistService;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (!User.Identity.IsAuthenticated)
        {
            // İstifadəçi login deyilsə boş siyahı qaytar
            return View(new List<WishlistVM>());
        }

        var user = await _userManager.GetUserAsync(HttpContext.User);
        var wishlist = await _wishlistService.GetAllAsync(user.Id);

        return View(wishlist);
    }
}
