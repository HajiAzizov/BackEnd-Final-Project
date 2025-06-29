// Services/WishlistService.cs
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Product;
using Final_Project.ViewModels.User.Wishlist;
using Microsoft.EntityFrameworkCore;

public class WishlistService : IWishlistService
{
    private readonly AppDbContext _context;

    public WishlistService(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(string userId, int productId)
    {
        var wishlist = await _context.Wishlists
            .Include(w => w.WishlistItems)
            .FirstOrDefaultAsync(w => w.AppUserId == userId);

        if (wishlist == null)
        {
            wishlist = new Wishlist { AppUserId = userId };
            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();
        }

        bool exists = wishlist.WishlistItems.Any(wi => wi.ProductId == productId);
        if (exists) return;

        wishlist.WishlistItems.Add(new WishlistItem
        {
            ProductId = productId,
            WishlistId = wishlist.Id
        });

        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(string userId, int productId)
    {
        var wishlist = await _context.Wishlists
            .Include(w => w.WishlistItems)
            .FirstOrDefaultAsync(w => w.AppUserId == userId);

        if (wishlist == null) return;

        var item = wishlist.WishlistItems.FirstOrDefault(wi => wi.ProductId == productId);
        if (item != null)
        {
            _context.WishlistItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<WishlistVM>> GetAllAsync(string userId)
    {
        return await _context.WishlistItems
            .Where(wi => wi.Wishlist.AppUserId == userId)
            .Include(wi => wi.Product)
            .Select(wi => new WishlistVM
            {
                ProductId = wi.ProductId,
                ProductName = wi.Product.Name,
                ProductImage = wi.Product.Img,
                Price = wi.Product.Price
            })
            .ToListAsync();
    }
}
