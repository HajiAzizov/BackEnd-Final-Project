using Final_Project.ViewModels.Admin.About;

namespace Final_Project.Services.Interfaces
{
    public interface IAboutService
    {
        Task<List<AboutVM>> GetAllAsync();
        Task<AboutVM> GetByIdAsync(int id);
        Task CreateAsync(AboutCreateVM model);
        Task EditAsync(AboutEditVM model);
        Task DeleteAsync(int id);
    }
}
