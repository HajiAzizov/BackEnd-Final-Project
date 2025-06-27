using Final_Project.ViewModels.User.Basket;

namespace Final_Project.Services.Interfaces
{
    public interface IBasketService
    {
        Task AddToBasketAsync(string userId, int productId, int quantity = 1);
        Task<List<BasketItemVM>> GetUserBasketItemsAsync(string userId);
        Task RemoveItemAsync(string userId, int productId);
        Task ChangeQuantityAsync(string userId, int productId, int newQuantity);
        Task<List<BasketItemVM>> GetBasketItemsAsync();
    }
}
