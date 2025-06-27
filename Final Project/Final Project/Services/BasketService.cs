using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.User.Basket;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
    public class BasketService : IBasketService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketService(AppDbContext context, UserManager<AppUser> userManager,
    IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddToBasketAsync(string userId, int productId, int quantity = 1)
        {
            var basket = await _context.Baskets
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (basket == null)
            {
                basket = new Basket
                {
                    UserId = userId,
                    BasketItems = new List<BasketItem>()
                };
                await _context.Baskets.AddAsync(basket);
            }

            var existingItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                basket.BasketItems.Add(new BasketItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<BasketItemVM>> GetUserBasketItemsAsync(string userId)
        {
            var basket = await _context.Baskets
                .Include(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (basket == null) return new();

            return basket.BasketItems.Select(item => new BasketItemVM
            {
                ProductId = item.ProductId,
                ProductName = item.Product.Name,
                Img = item.Product.Img,
                Price = item.Product.Price,
                Quantity = item.Quantity
            }).ToList();
        }

        public async Task RemoveItemAsync(string userId, int productId)
        {
            var basket = await _context.Baskets
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (basket == null) return;

            var item = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                basket.BasketItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ChangeQuantityAsync(string userId, int productId, int newQuantity)
        {
            var basket = await _context.Baskets
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.UserId == userId);

            var item = basket?.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                item.Quantity = newQuantity;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<BasketItemVM>> GetBasketItemsAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null)
                return new List<BasketItemVM>();

            var basket = await _context.Baskets
                .Include(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                    .ThenInclude(p => p.ProductGenres)
                        .ThenInclude(pg => pg.Genre)
                .FirstOrDefaultAsync(b => b.UserId == user.Id);

            if (basket == null)
                return new List<BasketItemVM>();

            return basket.BasketItems.Select(item => new BasketItemVM
            {
                ProductId = item.ProductId,
                ProductName = item.Product.Name,
                Img = item.Product.Img,
                Quantity = item.Quantity,
                Price = item.Product.Price,
                Genres = item.Product.ProductGenres.Select(pg => pg.Genre.Name).ToList()
            }).ToList();
        }




    }
}
