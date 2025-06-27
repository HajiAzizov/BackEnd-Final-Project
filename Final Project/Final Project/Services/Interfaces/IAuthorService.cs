using Final_Project.ViewModels.Admin.Author;

namespace Final_Project.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorVM>> GetAllAsync();
        Task<AuthorVM> GetByIdAsync(int id);
        Task CreateAsync(AuthorCreateVM model);
        Task EditAsync(int id, AuthorEditVM model);
        Task DeleteAsync(int id);
    }
}
