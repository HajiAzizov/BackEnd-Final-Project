using Final_Project.ViewModels.Admin.BestSellingBook;

namespace Final_Project.Services.Interfaces
{
    public interface IBestSellingBookService
    {
        Task<List<BestSellingBookVM>> GetAllAsync();
        Task<BestSellingBookVM> GetByIdAsync(int id);
        Task CreateAsync(BestSellingBookCreateVM model);
        Task UpdateAsync(BestSellingBookEditVM model);
        Task DeleteAsync(int id);
    }
}
