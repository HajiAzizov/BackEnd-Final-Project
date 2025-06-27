using Final_Project.ViewModels.Admin.Blog;

namespace Final_Project.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogVM>> GetAllAsync();
        Task<BlogVM> GetByIdAsync(int id);
        Task CreateAsync(BlogCreateVM model);
        Task<BlogEditVM> GetEditByIdAsync(int id);
        Task EditAsync(BlogEditVM model);
        Task DeleteAsync(int id);
    }
}
