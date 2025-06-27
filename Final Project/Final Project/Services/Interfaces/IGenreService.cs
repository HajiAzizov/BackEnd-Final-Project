using Final_Project.ViewModels.Admin.Genre;

namespace Final_Project.Services.Interfaces
{
    public interface IGenreService
    {
        Task<List<GenreVM>> GetAllAsync();
        Task<GenreVM> GetByIdAsync(int id);
        Task CreateAsync(GenreCreateVM model);
        Task EditAsync(int id, GenreEditVM model);
        Task DeleteAsync(int id);
    }
}
