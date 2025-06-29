using Final_Project.ViewModels.Admin.Product;
using Final_Project.ViewModels.User.Wishlist;

namespace Final_Project.Services.Interfaces
{
    public interface IWishlistService
    {
        Task AddAsync(string userId, int productId);
        Task RemoveAsync(string userId, int productId);
        Task<List<WishlistVM>> GetAllAsync(string userId);
    }
}
