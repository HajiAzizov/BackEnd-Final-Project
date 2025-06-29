using Final_Project.ViewModels.Admin.Product;
using Final_Project.ViewModels.User.UIProduct;

namespace Final_Project.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetAllAsync();
        Task<ProductVM> GetByIdAsync(int id);
        Task CreateAsync(ProductCreateVM model);
        Task EditAsync(int id, ProductEditVM model);
        Task DeleteAsync(int id);
        Task<int> GetCountAsync();
        Task<List<ProductVM>> GetPagedAsync(int page, int pageSize);
        Task<List<ProductSearchVM>> LiveSearchAsync(string query);


    }
}
