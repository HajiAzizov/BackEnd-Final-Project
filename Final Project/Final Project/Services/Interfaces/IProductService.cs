using Final_Project.ViewModels.Admin.Product;

namespace Final_Project.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetAllAsync();
        Task<ProductVM> GetByIdAsync(int id);
        Task CreateAsync(ProductCreateVM model);
        Task EditAsync(int id, ProductEditVM model);
        Task DeleteAsync(int id);
    }
}
