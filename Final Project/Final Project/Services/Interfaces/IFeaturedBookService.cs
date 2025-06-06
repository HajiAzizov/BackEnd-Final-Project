using Final_Project.ViewModels.Admin.FuturedBook;

namespace Final_Project.Services.Interfaces
{
    public interface IFeaturedBookService
    {
        Task<List<FeaturedBookVM>> GetAllAsync();
        Task<FeaturedBookVM> GetByIdAsync(int id);
        Task CreateAsync(FeaturedBookCreateVM model);
        Task UpdateAsync(FeaturedBookEditVM model);
        Task DeleteAsync(int id);
    }
}
